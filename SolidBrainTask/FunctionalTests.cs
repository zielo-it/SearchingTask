using NUnit.Framework;

namespace SolidBrainTask
{
    [TestFixture]
    internal class FunctionalTests
    {
        [Test]
        public void SearchTest()
        {
            Assert.IsTrue(new PhraseSearch().Search("Yumi"), "Search is not working properly.");
        }
    }
}