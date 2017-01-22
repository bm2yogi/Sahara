namespace Sahara.UnitTests.SampleClasses
{
    class BaseClass { }

    class DerivedClass : BaseClass { }

    interface IInterface1 { }

    class InterfaceImpl : IInterface1 { }

    class DerivedImpl : BaseClass, IInterface1 { }
}