using NUnit.Framework;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Tests
{
    public class DebugHelperTests
    {
        [Test]
        public void Test()
        {
            typeof(DebugHelperTests).F(nameof(Test), "somthing", "hehe");
            Assert.Pass();
        }
    }
}