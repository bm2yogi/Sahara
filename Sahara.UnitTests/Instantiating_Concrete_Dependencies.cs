using NUnit.Framework;
using Sahara.UnitTests.SampleClasses;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Instantiating_Concrete_Dependencies
        {
        [Test]
        public void Setup_a_class_with_a_concrete_dependency()
        {
            var instance = new Spec<SingleConcreteDependencyClass>().Build();
            instance.Value.ShouldEqual("Monkey");
        }

        [Test]
        public void Setup_a_class_with_an_implemented_interface_dependency()
        {
            // SingleDependencyClass has a constructor with an interface
            // dependency which has an implementation
            var instance = new Spec<SingleDependencyClass>().Build();
            instance.TheValue.ShouldEqual("Monkey");
        }
    }
}