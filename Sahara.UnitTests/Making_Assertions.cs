using System;
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

        class Foo: IComparable<Foo>
        {
            public Foo(string value)
            {
                Value = value;
            }

            public string Value { get; set; }

            public int CompareTo(Foo other)
            {
                return this.Value.CompareTo(other.Value);
            }
        }

        [Test]
        public void Assertions_should_check_values()
        {
            (new object() as string).ShouldBeNull();
            "Monkey".ShouldNotBeNull();

            "".ShouldBeEmpty();
            "Monkey".ShouldNotBeEmpty();

            new int[] {}.ShouldBeEmpty();
            new[] {1}.ShouldNotBeEmpty();

            "Monkey".ShouldEqual("Monkey");
            "Monkey".ShouldBeAtLeast("Monkey");
            "Monkey".ShouldBeAtMost("Monkey");
            "Monkey".ShouldNotEqual("Ape");
            "Monkey".ShouldBeGreaterThan("Ape");
            "Ape".ShouldBeLessThan("Monkey");

            var apeFoo = new Foo("Ape");
            var monkeyFoo = new Foo("Monkey");

            monkeyFoo.ShouldBeGreaterThan(apeFoo);
            monkeyFoo.ShouldBeAtLeast(apeFoo);
            apeFoo.ShouldBeLessThan(monkeyFoo);
            apeFoo.ShouldBeAtMost(monkeyFoo);
            monkeyFoo.ShouldEqual(monkeyFoo);
            monkeyFoo.ShouldBeAtLeast(monkeyFoo);
            monkeyFoo.ShouldBeAtMost(monkeyFoo);

            3.ShouldBeGreaterThan(2);
            3.ShouldBeAtLeast(2);
            2.ShouldBeLessThan(3);
            2.ShouldBeAtMost(3);
            3.ShouldEqual(3);
            3.ShouldBeAtLeast(3);
            3.ShouldBeAtMost(3);

            true.ShouldBeTrue();
            false.ShouldBeFalse();
            
            new[] { "a", "b", "c", "d" }.ShouldNotContain("e");
            new[] { "a", "b", "c", "d" }.ShouldContain("c");
            new[] { "a", "b", "c", "d", "c", "c" }.ShouldContain("c", 3);

            new[] { new Foo("a"), new Foo("b"), new Foo("c"), new Foo("d") }.ShouldContain(x => x.Value == "a");
            new[] { new Foo("a"), new Foo("b"), new Foo("c"), new Foo("d") }.ShouldNotContain(x => x.Value == "e");

            "Monkey".ShouldBeOfType<string>();
            new DerivedClass().ShouldBeOfType<BaseClass>();
            new DerivedClass().ShouldBeOfType<DerivedClass>();
            new InterfaceImpl().ShouldBeOfType<IInterface1>();
            new DerivedImpl().ShouldBeOfType<BaseClass>();
            new DerivedImpl().ShouldBeOfType<IInterface1>();
        }
    }
}