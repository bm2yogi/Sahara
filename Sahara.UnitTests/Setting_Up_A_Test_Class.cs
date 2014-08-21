using System;
using System.Linq;
using System.Reflection;
using Moq;
using NUnit.Framework;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Setting_Up_A_Test_Class
    {
        [Test]
        public void Setup_a_type_with_zero_dependencies()
        {
            Spec.For<ZeroDependencyClass>()
                .ShouldNotBeNull()
                .ShouldBeOfType<ZeroDependencyClass>();
        }

        [Test]
        public void Setup_a_type_with_one_interface_dependency()
        {
            Spec.For<SingleDependencyClass>()
                .ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>();
        }
    }

    public interface IFirstDependency
    {
        string StringValue { get; set; }
    }

    internal class SingleDependencyClass
    {
        private readonly IFirstDependency _first;


        public SingleDependencyClass(IFirstDependency first)
        {
            _first = first;
        }

        public string TheValue
        {
            get { return _first.StringValue; }
        }
    }

    public class Spec
    {
        public static T For<T>() where T : class
        {
            var biggestCtor = GetConstructorWithMostParameters<T>();
            var parameterTypes = biggestCtor.GetParameters()
                .Select(p1 => BuildMockObject(p1.ParameterType));

            return biggestCtor.Invoke(parameterTypes.ToArray()) as T;
        }

        private static object BuildMockObject(Type type)
        {
            var mock = typeof(Mock<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
            return mock.GetType().GetProperties().Single(f => f.Name == "Object" && f.PropertyType == type).GetValue(mock, new object[] { });
        }

        private static ConstructorInfo GetConstructorWithMostParameters<T>() where T : class
        {
            return typeof(T)
                .GetConstructors()
                .First(c => c.GetParameters().Count() == typeof(T).GetConstructors().Max(c2 => c2.GetParameters().Length));
        }
    }

    internal class ZeroDependencyClass
    {

    }
}