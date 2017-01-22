using System;
using NUnit.Framework;
using Moq;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Dependencies_With_Non_Public_Constructors
    {
        [Test]
        public void Dependency_has_protected_constructor()
        {
            var spec = new Spec<DependencyWithProtectedConstructor>();
            var sut = spec.Build();

            //Should fall back to default mocked instance
            sut.Echo("foo").ShouldBeNull();
        }

        [Test]
        public void Specify_mock_for_dependency_has_protected_constructor()
        {
            var spec = new Spec<DependencyWithProtectedConstructor>();
            spec.The<IProtectedConstructor>()
                .Setup(c => c.Echo("foo"))
                .Returns("NotFoo");
            var sut = spec.Build();

            //Should fall back to specific mocked instance
            sut.Echo("foo").ShouldEqual("NotFoo");
        }

        [Test, Ignore("Throws Castle.DynamicProxy.InvalidProxyConstructorArgumentsException")]
        public void Dependency_has_private_constructor()
        {
            var spec = new Spec<DependencyWithPrivateConstructor>();
            spec.Build();
        }

        [Test]
        public void Specify_mock_for_dependency_has_private_constructor()
        {
            var spec = new Spec<DependencyWithPrivateConstructor>();
            spec.The<IPrivateConstructor>()
                .Setup(c => c.Echo("foo"))
                .Returns("NotFoo");
            var sut = spec.Build();

            //Should fall back to specific mocked instance
            sut.Echo("foo").ShouldEqual("NotFoo");
        }
    }
}