using System;
using NUnit.Framework;
using Sahara.UnitTests.SampleClasses;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Setting_Up_Test_Subjects
    {
        [Test]
        public void Setup_a_type_using_implicit_conversion()
        {
            SingleDependencyClass sut = new Spec<SingleDependencyClass>();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>();
        }

        public void Setup_a_type_using_explicit_conversion()
        {
            var sut = (SingleDependencyClass) new Spec<SingleDependencyClass>();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>();
        }

        public void Setup_a_type_using_builder_method()
        {
            var sut = new Spec<SingleDependencyClass>().Build();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>();
        }

        [Test]
        public void Setup_a_type_with_zero_dependencies()
        {
            ZeroDependencyClass sut = new Spec<ZeroDependencyClass>();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<ZeroDependencyClass>();
        }

        [Test]
        public void Setup_a_type_with_one_interface_dependency()
        {
            SingleDependencyClass sut = new Spec<SingleDependencyClass>();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>();
        }


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