using NUnit.Framework;
using Sahara.UnitTests.SampleClasses;

namespace Sahara.UnitTests
{
    public class Mocking_Interface_Dependencies
    {
        [Test]
        public void Setup_a_mocked_operation_on_a_type_with_an_interface_dependency()
        {
            var spec = new Spec<SingleDependencyClass>();

            spec.The<IFirstDependency>()
                .SetupGet(d => d.StringValue)
                .Returns("TheValue");

            var sut = spec.Build();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>()
                .TheValue
                .ShouldNotBeNull()
                .ShouldEqual("TheValue");
        }

        [Test]
        public void Setup_mocked_operations_on_a_type_with_more_than_one_interface_dependency()
        {
            var spec = new Spec<MultiDependencyClass>();

            spec.The<IFirstDependency>().SetupGet(fd => fd.StringValue).Returns("TheValue");
            spec.The<ISecondDependency>().Setup(sd => sd.Calculate()).Returns(42);

            var sut = spec.Build();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<MultiDependencyClass>()
                .TheValue.ShouldEqual("TheValue");
            sut.TheNumber.ShouldEqual(42);
        }

        [Test]
        public void Setup_multiple_mocked_operations_on_a_single_interface_dependency()
        {
            var spec = new Spec<AnotherClass>();

            spec.The<IDependency>()
                .Setup(x => x.FirstOperation())
                .Returns("Foo");

            spec.The<IDependency>()
                .Setup(x => x.SecondOperation())
                .Returns("Bar");

            var sut = spec.Build();

            sut.Write().ShouldEqual("FooBarFoo");
        }

        [Test]
        public void Setup_operations_on_a_single_interface_dependency()
        {
            var spec = new Spec<AnotherClass>();

            var sut = spec.Build();

            sut.Write().ShouldEqual("MonkeyPantsMonkey");
        }
    }

    public interface IDependency
    {
        string FirstOperation();
        string SecondOperation();
    }

    class Dependency : IDependency
    {
        public string FirstOperation()
        {
            return "Monkey";
        }

        public string SecondOperation()
        {
            return "Pants";
        }
    }

    public class AnotherClass
    {
        private readonly IDependency _dependency;

        public AnotherClass(IDependency dependency)
        {
            _dependency = dependency;
        }

        public string Write()
        {
            return _dependency.FirstOperation() + _dependency.SecondOperation() + _dependency.FirstOperation();
        }
    }
}