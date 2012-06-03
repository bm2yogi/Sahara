using NUnit.Framework;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Making_Assertions
    {
        [Test]
        public void Assertions_should_check_values()
        {
            (new object() as string).ShouldBeNull();
            "Monkey".ShouldNotBeNull();
            "Monkey".ShouldEqual("Monkey");
            "Monkey".ShouldNotEqual("Ape");
            3.ShouldBeGreaterThan(2);
            3.ShouldBeAtLeast(2);
            2.ShouldBeLessThan(3);
            2.ShouldBeAtMost(3);
            3.ShouldEqual(3);
            3.ShouldBeAtLeast(3);
            3.ShouldBeAtMost(3);
            true.ShouldBeTrue();
            false.ShouleBeFalse();
            "Monkey".ShouldBeOfType<string>();
            1.ShouldBeOfType<int>();
        }

        [Test]
        public void Setting_up_conditions_should_be_easy()
        {
            //Some nice syntax for G/W/T...
            
        }
        
    }
}