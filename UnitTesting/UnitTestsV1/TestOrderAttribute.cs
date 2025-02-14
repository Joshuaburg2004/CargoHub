using System.Diagnostics.CodeAnalysis;

namespace CargoHub.UnitTesting
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [ExcludeFromCodeCoverage]
    public class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }
    }
}