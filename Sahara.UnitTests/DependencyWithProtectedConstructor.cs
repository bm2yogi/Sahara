namespace Sahara.UnitTests
{
    public class DependencyWithProtectedConstructor
    {
        private readonly IProtectedConstructor _protectedConstructor;

        public DependencyWithProtectedConstructor(IProtectedConstructor protectedConstructor)
        {
            _protectedConstructor = protectedConstructor;
        }

        public string Echo(string value)
        {
            return _protectedConstructor.Echo(value);
        }
    }

    public interface IProtectedConstructor
    {
        string Echo(string value);
    }

    public class ProtectedConstructor : IProtectedConstructor
    {
        protected ProtectedConstructor() { }

        public string Echo(string value)
        {
            return string.Format("Echo: {0}", value);
        }
    }
}