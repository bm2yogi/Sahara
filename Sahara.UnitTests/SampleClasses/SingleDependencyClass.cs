namespace Sahara.UnitTests.SampleClasses
{
    internal class SingleDependencyClass
    {
        private readonly IFirstDependency _first;


        public SingleDependencyClass(IFirstDependency first)
        {
            _first = first;
        }

        public string TheValue
        {
            get { return _first.StringValue; }
        }
    }
}