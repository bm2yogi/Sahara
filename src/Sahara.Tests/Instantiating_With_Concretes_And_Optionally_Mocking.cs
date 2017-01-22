using NUnit.Framework;
using Sahara.UnitTests.SampleClasses;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Instantiating_With_Concretes_And_Optionally_Mocking
    {
        [Test]
        public void Multi_dependency_subject_with_default()
        {
            var sut = new Spec<MultiDependencyClass>().Build();
            sut.TheNumber.ShouldEqual(0);
            sut.TheValue.ShouldEqual("Monkey");
        }

        [Test]
        public void Multi_dependency_subject_with_mocks()
        {
            var sut = new Spec<MultiDependencyClass>();
            sut.The<IFirstDependency>()
                .Setup(fd => fd.StringValue).Returns("Gorilla");
            sut.The<ISecondDependency>()
                .Setup(sd => sd.Calculate()).Returns(5150);
            var instance = sut.Build();
            instance.TheNumber.ShouldEqual(5150);
            instance.TheValue.ShouldEqual("Gorilla");
        }

        [Test]
        public void Deep_level_dependency_mock()
        {
            var sut = new Spec<DeepDependencyClass>();
            sut.The<IFirstDependency>()
                .Setup(fd => fd.StringValue).Returns("Gorilla");
            sut.The<ISecondDependency>()
                .Setup(sd => sd.Calculate()).Returns(5150);
            var instance = sut.Build();
            instance.NumericValue.ShouldEqual(5150);
            instance.StringValue.ShouldEqual("Gorilla");
        }
    }
}