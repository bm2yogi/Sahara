namespace Sahara.UnitTests.SampleClasses
{
    public interface IFirstDependency
    {
        string StringValue { get; set; }
    }
    public interface ISecondDependency
    {
        int Calculate();
    }

    public class ConcreteFirstDependency : IFirstDependency
    {
        private string _value;

        public ConcreteFirstDependency()
        {
            _value = "Monkey";
        }

        public string StringValue
        {
            get { return _value; }
            set { _value = value; }
        }
    }

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