namespace Sahara.UnitTests.SampleClasses
{
    public class MultiDependencyClass
    {
        private readonly IFirstDependency _first;
        private readonly ISecondDependency _second;

        public MultiDependencyClass(IFirstDependency first, ISecondDependency second)
        {
            _first = first;
            _second = second;
        }

        public string TheValue
        {
            get { return _first.StringValue; }
        }

        public int TheNumber
        {
            get { return _second.Calculate(); }
        }
    }
}