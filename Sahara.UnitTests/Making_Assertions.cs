using NUnit.Framework;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Making_Assertions
    {
        interface IInterface1 { }
        class BaseClass { }
        class DerivedClass : BaseClass { }
        class InterfaceImpl : IInterface1 { }
        class DerivedImpl : BaseClass, IInterface1 { }

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
            false.ShouldBeFalse();
            "Monkey".ShouldBeOfType<string>();

            new DerivedClass().ShouldBeOfType<BaseClass>();
            new DerivedClass().ShouldBeOfType<DerivedClass>();
            new InterfaceImpl().ShouldBeOfType<IInterface1>();
            new DerivedImpl().ShouldBeOfType<BaseClass>();
            new DerivedImpl().ShouldBeOfType<IInterface1>();
        }

        [Test]
        public void Setting_up_conditions_should_be_easy()
        {
            //Some nice syntax for G/W/T...

        }

    }
}