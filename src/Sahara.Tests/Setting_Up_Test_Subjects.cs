using System;
using NUnit.Framework;
using Sahara.UnitTests.SampleClasses;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Setting_Up_Test_Subjects
    {
        private readonly Mocking_Interface_Dependencies _mockingInterfaceDependencies = new Mocking_Interface_Dependencies();

        [Test]
        public void Setup_a_type_using_implicit_conversion()
        {
            SingleDependencyClass sut = new Spec<SingleDependencyClass>();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>();
        }

        [Test]
        public void Setup_a_type_using_explicit_conversion()
        {
            var sut = (SingleDependencyClass) new Spec<SingleDependencyClass>();

            sut.ShouldNotBeNull()
                .ShouldBeOfType<SingleDependencyClass>();
        }

        [Test]
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
    }
}