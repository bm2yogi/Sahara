#Sahara
##Fluent Assertions and Auto-Mocking for NUnit Tests

Because your tests should be ridiculously easy to read and understand. 

--------

Sahara is an assertion library that lets you write your unit tests in a more natural language syntax.

Sahara is also an auto-mocking framework that makes it easier to test and refactor legacy code by helping you isolate dependencies away from the classes you want to test.

Sahara also supports auto-mocking for ASP.NET MVC4 ([Sahara.Mvc4](https://www.nuget.org/packages/Sahara.Mvc4)) and MVC5 ([Sahara.Mvc](https://www.nuget.org/packages/Sahara.Mvc)) apps.

You can find Sahara 1.5.0 on NuGet [here](https://www.nuget.org/packages/Sahara/). You can also download and add Sahara to your Visual Studio projects via the NuGet Package Manager.

I hope you like it, that you find it makes your unit tests easier to read and write, and that it makes working with legacy code easier. Let me know what you think. I'm [@bm2yogi](https://www.twitter.com/bm2yogi) on Twitter.

Assertions
-------------

For example, instead of writing the following:

```
Assert.IsNotNull(testResult);
Assert.IsNotEmpty(testResult);
```

Why not:

```
testResult
    .ShouldNotBeNull()
    .ShouldNotBeEmpty();
```

There's support for every assertion I could think of so far, and it's a work in progress, so there's always room for more.

Auto-Mocking
-----------------
Writing automated unit tests for classes that are tightly coupled to dependent classes (both external and internal) can be a royal pain in the assertions.

Sahara's auto-mocking feature encourages you to take advantage of constructor injection (using interface types) to declare your dependencies, so that they can be mocked and you can test (verify) your classes' behaviors in isolation, without the yak-shaving test setup that usually comes with deep inheritance chains and tightly coupled dependencies.

More to come on this later, but in the meantime check out the test project to see how it works...

