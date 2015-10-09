using NUnit.Framework;
using Sahara.UnitTests.SampleClasses;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Setting_Up_Test_Subjects_To_Use_Reals
    {
        [Test]
        public void SUT_has_one_dependency_with_zero_child_dependencies()
        {
            var instance = new Spec<SingleConcreteDependencyClass>().Build();
            instance.Value.ShouldEqual("Monkey");
        }

        [Test]
        public void TestMethodName()
        {
            var instance = new Spec<ConcreteFirstDependency>().Build();
            instance.StringValue.ShouldEqual("Monkey");
        }

        [Test]
        public void Test2()
        {
            var instance = new Spec<SingleDependencyClass>().Build();
            instance.TheValue.ShouldEqual("Monkey");
        }

        [Test]
        public void test3()
        {
            var instance = new Spec<MultiDependencyClass>().Build();
            instance.ShouldNotBeNull()
                .TheNumber.ShouldEqual(0);
            instance.TheValue.ShouldEqual("Monkey");

        }
    }
}