using Xunit.Abstractions;
using Xunit.Sdk;

namespace Accredetation.Application.IntegrationTests;

public class TestCaseOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        // Order the test cases by the OrderAttribute value
        var orderedTestCases = testCases.OrderBy(tc =>
        {
            var orderAttr = tc.TestMethod.Method.GetCustomAttributes(typeof(OrderAttribute).AssemblyQualifiedName).FirstOrDefault();
            return orderAttr == null ? 0 : orderAttr.GetNamedArgument<int>("Order");
        });

        return orderedTestCases;
    }
}
