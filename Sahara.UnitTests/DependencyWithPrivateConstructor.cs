namespace Sahara.UnitTests
{
    public class DependencyWithPrivateConstructor
    {
        private readonly IPrivateConstructor _privateConstructor;

        public DependencyWithPrivateConstructor(IPrivateConstructor privateConstructor)
        {
            _privateConstructor = privateConstructor;
        }

        public string Echo(string value)
        {
            return _privateConstructor.Echo(value);
        }
    }

    public interface IPrivateConstructor
    {
        string Echo(string value);
    }

    public class PrivateConstructor : IPrivateConstructor
    {
        private PrivateConstructor() { }

        public string Echo(string value)
        {
            return string.Format("Echo: {0}", value);
        }
    }
}