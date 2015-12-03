using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessing
{
    public class WordClass:IEquatable<WordClass>
    {
        //Work variables init.
        private string _word = string.Empty;
        private int _count = 1;

        /// <summary>
        /// Cuvintul.
        /// </summary>
        public string Word
        {
            get { return _word; }
            set { _word = value; }
        }

        /// <summary>
        /// Numarul de aparitii a cuvintului.
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public bool Equals(WordClass other)
        {
            if (other==null)
            {
                return false;
            }
            return (Word.Equals(other.Word));
        }
    }
}
