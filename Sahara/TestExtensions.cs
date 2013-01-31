using System;
using System.Collections;
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

        public static string ShouldBeEmpty(this string actual)
        {
            Assert.IsEmpty(actual);
            return actual;
        }

        public static string ShouldNotBeEmpty(this string actual)
        {
            Assert.IsNotEmpty(actual);
            return actual;
        }

        public static T ShouldBeEmpty<T>(this T actual) where T : IEnumerable
        {
            Assert.IsEmpty(actual);
            return actual;
        }

        public static T ShouldNotBeEmpty<T>(this T actual) where T : IEnumerable
        {
            Assert.IsNotEmpty(actual);
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

        public static IComparable<T> ShouldBeGreaterThan<T>(this IComparable<T> actual, T expected)
        {
            Assert.Greater(actual.CompareTo(expected),0);
            return actual;
        }

        public static IComparable<T> ShouldBeAtLeast<T>(this IComparable<T> actual, T expected)
        {
            Assert.GreaterOrEqual(actual.CompareTo(expected), 0);
            return actual;
        }

        public static IComparable<T> ShouldBeLessThan<T>(this IComparable<T> actual, T expected)
        {
            Assert.Less(actual.CompareTo(expected), 0);
            return actual;
        }

        public static IComparable<T> ShouldBeAtMost<T>(this IComparable<T> actual, T expected)
        {
            Assert.LessOrEqual(actual.CompareTo(expected),0);
            return actual;
        }

        public static string ShouldContain(this string actual, string match)
        {
            Assert.IsTrue(actual.Contains(match));
            return actual;
        }

        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> actual, T match)
        {
            Assert.IsTrue(actual.Any(x => x.Equals(match)));
            return actual;
        }

        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> actual, Func<T, bool> predicate)
        {
            Assert.IsTrue(actual.Any(predicate));
            return actual;
        }

        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> actual, T match, int count)
        {
            Assert.IsTrue(actual.Count(x => x.Equals(match)) == count);
            return actual;
        }

        public static IEnumerable<T> ShouldNotContain<T>(this IEnumerable<T> actual, T match)
        {
            Assert.IsFalse(actual.Any(x => x.Equals(match)));
            return actual;
        }

        public static IEnumerable<T> ShouldNotContain<T>(this IEnumerable<T> actual, Func<T, bool> predicate)
        {
            Assert.IsFalse(actual.Any(predicate));
            return actual;
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