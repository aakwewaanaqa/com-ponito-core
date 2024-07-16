using google_storage_upload_tool.Models;
using NUnit.Framework;

namespace Ponito.Core.Tests
{
    public class PathNameInfoTests
    {
        [Test]
        public void ForRange()
        {
            var name  = new PathNameInfo("A/B/C/D");
            
            var range = name[..];
            Assert.That(range.ToString(), Is.EqualTo("A/B/C/D"));
            
            range = name[1..];
            Assert.That(range.ToString(), Is.EqualTo("B/C/D"));
            range = name[2..];
            Assert.That(range.ToString(), Is.EqualTo("C/D"));
            range = name[3..];
            Assert.That(range.ToString(), Is.EqualTo("D"));

            range = name[..^1];
            Assert.That(range.ToString(), Is.EqualTo("A/B/C"));
            range = name[..^2];
            Assert.That(range.ToString(), Is.EqualTo("A/B"));
            range = name[..^3];
            Assert.That(range.ToString(), Is.EqualTo("A"));
        }
    }
}