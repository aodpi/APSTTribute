using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulas
{
    public class ProbabilityItem:IEquatable<ProbabilityItem>
    {
        string word;
        double probability;

        public double Probability
        {
            get
            {
                return probability;
            }

            set
            {
                probability = value;
            }
        }

        public string Word
        {
            get
            {
                return word;
            }

            set
            {
                word = value;
            }
        }

        public bool Equals(ProbabilityItem other)
        {
            if (other == null)
            {
                return false;
            }
            return (Word.Equals(other.Word));
        }
    }
}
