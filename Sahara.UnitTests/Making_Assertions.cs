using NUnit.Framework;
using Sahara.UnitTests.SampleClasses;

namespace Sahara.UnitTests
{
    [TestFixture]
    public class Making_Assertions
    {
        [Test]
        public void Making_assertions_about_null_or_empty()
        {
            (null as object).ShouldBeNull();
            (new object()).ShouldNotBeNull();

            "".ShouldBeEmpty();
            "Monkey".ShouldNotBeNull();
            "Monkey".ShouldNotBeEmpty();

            new int[] { }.ShouldBeEmpty();
            new[] { 1 }.ShouldNotBeEmpty();
        }

        [Test]
        public void Making_assertions_about_IComparable_types()
        {
            var apeFoo = new Foo("Ape");
            var monkeyFoo = new Foo("Monkey");

            monkeyFoo.ShouldBeGreaterThan(apeFoo);
            monkeyFoo.ShouldBeAtLeast(apeFoo);
            
            apeFoo.ShouldBeLessThan(monkeyFoo);
            apeFoo.ShouldBeAtMost(monkeyFoo);
            
            monkeyFoo.ShouldEqual(monkeyFoo);
            monkeyFoo.ShouldBeAtLeast(monkeyFoo);
            monkeyFoo.ShouldBeAtMost(monkeyFoo);
        }

        [Test, Ignore]
        public void Failing_a_test()
        {
            this.ShouldFail("For some reason.");
        }

        [Test, Ignore]
        public void Inconclusive_test()
        {
            // Inspired by Brad Wilson :)
            this.ShouldBeMeh();
        }

        [Test]
        public void Making_assertions_about_native_types()
        {
            "Monkey".ShouldEqual("Monkey");
            "Monkey".ShouldBeAtLeast("Monkey");
            "Monkey".ShouldBeAtMost("Monkey");
            "Monkey".ShouldNotEqual("Ape");
            "Monkey".ShouldBeGreaterThan("Ape");
            "Ape".ShouldBeLessThan("Monkey");

            "MonkeyFist".ShouldStartWith("Monkey");
            "MonkeyFist".ShouldEndWith("Fist");
            "MonkeyFist".ShouldContain("key");

            3.ShouldBeGreaterThan(2);
            3.ShouldBeAtLeast(2);

            2.ShouldBeLessThan(3);
            2.ShouldBeAtMost(3);

            3.ShouldEqual(3);
            3.ShouldBeAtLeast(3);
            3.ShouldBeAtMost(3);

            true.ShouldBeTrue();
            false.ShouldBeFalse();
        }

        [Test]
        public void Making_assertions_about_collections()
        {
            new[] { "a", "b", "c", "d" }.ShouldNotContain("e");
            new[] { "a", "b", "c", "d" }.ShouldContain("c");
            new[] { "a", "b", "c", "d", "c", "c" }.ShouldContain("c", 3);

            new[] { 1, 2, 3, 4 }.ShouldEqual(new[] { 1, 2, 3, 4 });
            new[] { 1, 2, 3, 4 }.ShouldEqualSet(new[] { 1, 2, 4, 3 });
            new[] { 1, 2, 3, 4 }.ShouldBeASuperSetOf(new[] { 1, 4, 3 });
            new[] { 1, 2, 3, 4 }.ShouldBeASubSetOf(new[] { 1, 4, 5, 2, 3 });

            new[] { new Foo("a"), new Foo("b"), new Foo("c"), new Foo("d") }.ShouldContain(x => x.Value == "a");
            new[] { new Foo("a"), new Foo("b"), new Foo("c"), new Foo("d") }.ShouldNotContain(x => x.Value == "e");
        }

        [Test]
        public void Making_assetions_about_types()
        {
            "Monkey".ShouldBeOfType<string>();
            new DerivedClass().ShouldBeOfType<BaseClass>();
            new DerivedClass().ShouldBeOfType<DerivedClass>();
            new InterfaceImpl().ShouldBeOfType<IInterface1>();
            new DerivedImpl().ShouldBeOfType<BaseClass>();
            new DerivedImpl().ShouldBeOfType<IInterface1>();
        }
    }
}