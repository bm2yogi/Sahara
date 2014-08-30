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
        protected readonly ConstructorInfo _biggestCtor;
        protected readonly IList<Mock> _mockDependencies;

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public Spec()
        {
            _biggestCtor = GetConstructorWithMostParameters<T>();
            _mockDependencies = GetMockDependencies(_biggestCtor);
        }

        private static ConstructorInfo GetConstructorWithMostParameters<TI>() where TI : class
        {
            var constructors = typeof(TI).GetConstructors();

            if (!constructors.Any())
            {
                return typeof(TI).GetConstructor(Type.EmptyTypes);
            }

            var maxParameterCount = constructors.Max(c => c.GetParameters().Length);
            return constructors.First(c => c.GetParameters().Count() == maxParameterCount);
        }

        private static IList<Mock> GetMockDependencies(ConstructorInfo biggestCtor)
        {
            if (biggestCtor == null) return new List<Mock>();

            return biggestCtor.GetParameters()
                .Select(parameter => BuildMockObject(parameter.ParameterType))
                .ToList();
        }

        protected static Mock BuildMockObject(Type type)
        {
            Type mockType = typeof(Mock<>).MakeGenericType(type);
            ConstructorInfo mockCtor = mockType.GetConstructor(Type.EmptyTypes);
            Mock instance = mockCtor.Invoke(new object[] { }) as Mock;
            return instance;
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
            var mock = _mockDependencies.FirstOrDefault(md => md is Mock<TI>) as Mock<TI>;
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
            var sut = _biggestCtor.Invoke(_mockDependencies.Select(m => m.Object).ToArray()) as T;
            return sut;
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