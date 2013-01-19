using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Sahara
{
    public static class TestExtensions
    {
        public static T ShouldBeNull<T>(this T actual)
        {
            Assert.IsNull(actual);
            return actual;
        }

        public static T ShouldNotBeNull<T>(this T actual)
        {
            Assert.IsNotNull(actual);
            return actual;
        }

        public static T ShouldEqual<T>(this T actual, T expected)
        {
            Assert.AreEqual(expected, actual);
            return actual;
        }

        public static T ShouldNotEqual<T>(this T actual, T expected)
        {
            Assert.AreNotEqual(expected, actual);
            return actual;
        }

        public static IComparable ShouldBeGreaterThan(this IComparable actual, IComparable expected)
        {
            Assert.Greater(actual, expected);
            return actual;
        }

        public static IComparable ShouldBeAtLeast(this IComparable actual, IComparable expected)
        {
            Assert.GreaterOrEqual(actual, expected);
            return actual;
        }

        public static IComparable ShouldBeLessThan(this IComparable actual, IComparable expected)
        {
            Assert.Less(actual, expected);
            return actual;
        }

        public static IComparable ShouldBeAtMost(this IComparable actual, IComparable expected)
        {
            Assert.LessOrEqual(actual, expected);
            return actual;
        }

        public static void ShouldContain(this string actual, string match)
        {
            Assert.IsTrue(actual.Contains(match));
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T match)
        {
            Assert.IsTrue(actual.Any(x => x.Equals(match)));
        }

        public static void ShouldContain<T>(this IEnumerable<T> actual, T match, int count)
        {
            Assert.IsTrue(actual.Count(x => x.Equals(match)) == count);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T match)
        {
            Assert.IsFalse(actual.Any(x => x.Equals(match)));
        }

        public static bool ShouldBeTrue(this bool actual)
        {
            Assert.IsTrue(actual);
            return actual;
        }

        public static bool ShouldBeFalse(this bool actual)
        {
            Assert.IsFalse(actual);
            return actual;
        }

        public static T ShouldBeOfType<T>(this T actual)
        {
            Assert.IsInstanceOf<T>(actual);
            return actual;
        }
    }
}