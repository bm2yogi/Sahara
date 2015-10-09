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

    internal class SingleConcreteDependencyClass
    {
        private ConcreteFirstDependency _depenency;

        public SingleConcreteDependencyClass(ConcreteFirstDependency dependency)
        {
            _depenency = dependency;
        }

        public string Value
        {
            get { return _depenency.StringValue; }
        }
    }
}