using System;

namespace SolidBrainTask
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine(new PhraseSearch().Search("Yumi") ? "Yes" : "No");
            Console.ReadLine();
        }
    }
}