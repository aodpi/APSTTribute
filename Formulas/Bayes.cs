using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Formulas
{
    public class Bayes
    {
        public Bayes(Dictionary<int, List<string>> parts)
        {
            CalculateProbabilitiesForParts(parts);
            CalculateProbabilitiesForParts(parts);
        }

        #region Fields
        private List<ProbabilityItem> probabilities = new List<ProbabilityItem>();
        private Dictionary<int, List<ProbabilityItem>> probabilitiesbyparts = new Dictionary<int, List<ProbabilityItem>>();
        #endregion

        #region Properties
        public List<ProbabilityItem> Probabilities
        {
            get
            {
                return probabilities;
            }

            set
            {
                probabilities = value;
            }
        }

        public Dictionary<int, List<ProbabilityItem>> ProbabilitiesByParts
        {
            get
            {
                return probabilitiesbyparts;
            }

            set
            {
                probabilitiesbyparts = value;
            }
        }
        #endregion

        #region Methods
        public void CalculateProbabilitiesForParts(Dictionary<int, List<string>> parts)
        {
            int count = 1;
            foreach (List<string> item in parts.Values)
            {
                List<ProbabilityItem> tmp = new List<ProbabilityItem>();
                foreach (string str in item)
                {
                    tmp.Add(new ProbabilityItem() { Word = str, Probability = (double)AparitionsInPart(str, item) / item.Count });
                    probabilities.Add(new ProbabilityItem() { Word = str, Probability = (double)AparitionsInPart(str, item) / item.Count });
                }
                ProbabilitiesByParts.Add(count, tmp);
                count++;
            }
        }

        public int AparitionsInPart(string word, List<string> part)
        {
            return part.Count(a => a == word);
        }

        public double GetTotalProbabilityOfWord(string word, Dictionary<int, List<string>> parts,bool squared)
        {
            double result = 0;
            foreach (int key in ProbabilitiesByParts.Keys)
            {
                for (int i = 0; i < ProbabilitiesByParts[key].Count; i++)
                {
                    if (ProbabilitiesByParts[key][i].Word == word)
                    {
                        switch (squared)
                        {
                            case true:
                                result += GetTextRatio(key, parts) * Math.Pow(ProbabilitiesByParts[key][i].Probability, 2);
                                break;
                            case false:
                                result += GetTextRatio(key, parts) * ProbabilitiesByParts[key][i].Probability;
                                break;
                            default:
                                break;
                        }
                        
                    }
                }
            }
            return result;
        }


        public double GetAProbability(string word, int part, Dictionary<int, List<string>> parts)
        {
            double result = 0;
            if (ProbabilitiesByParts[part].Contains(new ProbabilityItem() { Word = word }))
            {
                result = (GetTextRatio(part, parts) * ProbabilitiesByParts[part].Find(a => a.Word == word).Probability) / (GetTotalProbabilityOfWord(word, parts,false));
                return result;
            }
            else
            {
                return double.NaN;
            }
        }

        public double GetADoubleProbability(string word, int part, Dictionary<int, List<string>> parts)
        {
            double result = 0;
            if (ProbabilitiesByParts[part].Contains(new ProbabilityItem() { Word = word }))
            {
                result = (GetTextRatio(part, parts) * (Math.Pow(ProbabilitiesByParts[part].Find(a => a.Word == word).Probability, 2))) / (GetTotalProbabilityOfWord(word, parts,true));
                return result;
            }
            else
            {
                return double.NaN;
            }
        }

        public double GetTextRatio(int part,Dictionary<int,List<string>> parts)
        {
            int sum = 0;
            foreach (List<string> item in parts.Values)
            {
                sum += item.Count;
            }
            return (double)parts[part].Count / sum;
        }
        #endregion
    }
}
