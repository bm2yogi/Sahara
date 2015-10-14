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
    }
}