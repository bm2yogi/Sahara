Sahara
======

Fluent Assertions for NUnit Tests

Because your tests should be ridiculously easy to read and understand. 

Sahara is an assertion library that lets you write your unit tests in a more natural language syntax.

For example instead of writing the following:

Assert.IsNotNull(testResult);
Assert.IsNotEmpty(testResult);

Why not:

TestResult.ShouldNotBeNull().ShouldNotBeEmpty();

There's support for every assertion I could think of so far, and it's a work in progress, so there's always room for more.

You can also download and add it to your VS test projects via Nuget.
