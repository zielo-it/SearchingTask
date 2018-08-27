using NUnit.Framework;

namespace SolidBrainTask
{
    [TestFixture]
    [Category("Functional")]
    internal class FunctionalTests
    {
        [Test]
        [TestCase("Yumi")]
        public void SearchTest(string searchPhrase)
        {
            Assert.IsTrue(new PhraseSearch().Search(searchPhrase), "Search is not working properly.");
        }
    }
}