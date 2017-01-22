namespace Sahara.UnitTests.SampleClasses
{
    public class DeepDependencyClass
    {
        private readonly MultiDependencyClass _multiDependencyClass;

        public DeepDependencyClass(MultiDependencyClass multiDependencyClass)
        {
            _multiDependencyClass = multiDependencyClass;
        }

        public string StringValue
        {
            get { return _multiDependencyClass.TheValue; }
        }

        public int NumericValue
        {
            get { return _multiDependencyClass.TheNumber; }
        }
    }
}