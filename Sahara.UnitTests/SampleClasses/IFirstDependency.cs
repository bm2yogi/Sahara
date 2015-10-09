namespace Sahara.UnitTests.SampleClasses
{
    public interface IFirstDependency
    {
        string StringValue { get; set; }
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
}