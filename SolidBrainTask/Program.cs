using System;
using System.Threading;

namespace SolidBrainTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var phraseSearch = new PhraseSearch()
            {
                Phrase = "Yumi"
            };

            if(phraseSearch.Search())
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }

            Thread.Sleep(3000);
        }
    }
}
