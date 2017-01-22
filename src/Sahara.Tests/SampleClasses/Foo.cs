using System;

namespace Sahara.UnitTests.SampleClasses
{
    class Foo : IComparable<Foo>
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
}