using Xunit;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceTests
{
    [ExcludeFromCodeCoverage]
    public class BaseTest
    {
        public static string RequestUri = "http://localhost:3000";
        public static string API_KEY = "a1b2c3d4e5";

        public BaseTest() { }
    }
}
