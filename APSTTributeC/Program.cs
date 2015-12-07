using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulas;
using WordProcessing;
using System.IO;

namespace APSTTributeC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dati fisierul:");
            string fl = Console.ReadLine();
            string input = File.ReadAllText(fl);
            WordProcessor pc = new WordProcessor(input, WordProcessor.Order.None);
            Bayes bs = new Bayes(pc.Parts);
            Console.Clear();
            Console.WriteLine("Dati cuvantul:");
            string searchw = Console.ReadLine();
            Console.WriteLine("Probabilitate");
            Console.WriteLine("Parte:\tProbabilitate");
            for (int i = 0; i < bs.ProbabilitiesByParts.Count; i++)
            {
                Console.WriteLine("{0}\t{1:0.0000000}", i + 1, bs.ProbabilitiesByParts[i + 1].Find(a => a.Word == searchw).Probability);
            }
            Console.WriteLine("Aposterioara");
            Console.WriteLine("Parte:\tProbabilitate");
            for (int i = 0; i < bs.ProbabilitiesByParts.Count; i++)
            {
                Console.WriteLine("{0}\t{1:0.000000}", i + 1, bs.GetAProbability(searchw, i + 1, pc.Parts));
            }

            Console.WriteLine("Aparitia dubla");
            Console.WriteLine("Parte:\tProbabilitate");
            for (int i = 0; i < bs.ProbabilitiesByParts.Count; i++)
            {
                Console.WriteLine("{0}\t{1:0.000000}", i + 1, bs.GetADoubleProbability(searchw, i + 1, pc.Parts));
            }
            Console.ReadKey();
        }
    }
}
