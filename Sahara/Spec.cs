using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;

namespace Sahara
{
    /// <summary>
    ///     Builds a class of type T, creating and exposing mocked instances
    ///     of any dependent classes declared as interfaces in the class' constructor method.
    /// </summary>
    /// <typeparam name="T">The type of the class to be created. Must be a class.</typeparam>
    public class Spec<T> where T : class
    {
        protected IDictionary<Type, Mock> MockDictionary;

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public Spec()
        {
            MockDictionary = new Dictionary<Type, Mock>();
        }

        /// <summary>
        ///     Provides access to the class' dependencies (as declared
        ///     in the constructor(s)) as mock objects. Allows the setting of
        ///     expectations and behaviors on those mocks.
        /// </summary>
        /// <typeparam name="TI">The interface type of the requested dependency.</typeparam>
        /// <returns>
        ///     The mocked instance that was a declared dependency of the target class.
        ///     Returns null if there is no dependency that implements <see cref="TI" />.
        /// </returns>
        public virtual Mock<TI> The<TI>() where TI : class
        {
            var t = typeof(TI);
            var mock = (Mock<TI>)BuildMockObject(t);

            MockDictionary[t] = mock;

            return mock;
        }

        /// <summary>
        ///     Invokes the constructor of the target class, passing in
        ///     the class' dependencies as mocked instances and returns
        ///     the target class.
        /// </summary>
        /// <returns>The target class (of type T).</returns>
        public virtual T Build()
        {
            return BuildInstance(typeof(T)) as T;
        }

        private object BuildInstance(Type type)
        {
            if (type.IsInterface)
            {
                return MockDictionary.ContainsKey(type)
                    ? MockDictionary[type].Object
                    : BuildFromInterface(type);
            }

            ConstructorInfo[] ctors = type.GetConstructors();
            int maxParameterCount = ctors.Max(c => c.GetParameters().Length);
            ConstructorInfo ctor = ctors
                .First(c => c.GetParameters().Length == maxParameterCount);
            List<object> parameterList = new List<object>();

            if (maxParameterCount == 0)
                return ctor.Invoke(parameterList.ToArray());

            parameterList = ctor.GetParameters()
                .Select(pi => BuildInstance(pi.ParameterType)).ToList();

            return ctor.Invoke(parameterList.ToArray());
        }

        protected static Mock BuildMockObject(Type type)
        {
            Type mockType = typeof(Mock<>).MakeGenericType(type);
            ConstructorInfo mockCtor = mockType.GetConstructor(Type.EmptyTypes);
            Mock mockObject = mockCtor.Invoke(new object[] { }) as Mock;
            return mockObject;
        }

        private object BuildFromInterface(Type type)
        {
            Type[] types = type.Assembly.GetExportedTypes();
            Type assemblyType = types
                .FirstOrDefault(t => t.GetInterface(type.Name) != null);

            return assemblyType == null
                ? BuildMockObject(type).Object
                : BuildInstance(assemblyType);
        }

        /// <summary>
        ///     Performs an implicit type conversion from Spec&lt;T&gt; to a class of type T.
        /// </summary>
        /// <param name="spec">A class of type Spec&lt;T&gt;.</param>
        /// <returns>A class of type T.</returns>
        public static implicit operator T(Spec<T> spec)
        {
            return spec.Build();
        }
    }
}