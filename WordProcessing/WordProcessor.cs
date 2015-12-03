using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WordProcessing
{
    public class WordProcessor
    {
        /// <summary>
        /// Ordonarea cuvintelor in lista.
        /// </summary>
        public enum Order
        {
            None,
            ByCountAscending,
            ByCountDescending,
            ByWordAscending,
            ByWordDescending
        }
        /// <summary>
        /// Creati clasa ce proceseaza textul automat.
        /// </summary>
        /// <param name="inputstr">Textul pentru a fi procesat.</param>
        /// <param name="by">Ordonarea cuvintelor in lista</param>
        public WordProcessor(string inputstr,Order by)
        {
            PopulateWords(inputstr,by);
            DivideByTen();
        }

        #region Fields
        private List<WordClass> _words = new List<WordClass>();
        private List<string> _allwords = new List<string>();
        private Dictionary<int, List<string>> _parts = new Dictionary<int, List<string>>();
        #endregion

        #region Properties
        /// <summary>
        /// Contine 10 parti ale textului aproximativ egale.
        /// </summary>
        public Dictionary<int, List<string>> Parts
        {
            get { return _parts; }
            set { _parts = value; }
        }

        /// <summary>
        /// Cuvintele gasite in text impreuna cu numarul lor de aparitii in textul dat.
        /// </summary>
        public List<WordClass> Words
        {
            get
            {
                return _words;
            }
            private set
            {
                _words = value;
            }
        }

        /// <summary>
        /// Toate cuvintele gasite in text.
        /// </summary>
        public List<string> AllWords
        {
            get
            {
                return _allwords;
            }
            set
            {
                _allwords = value;
            }
        }
        #endregion

        #region Methods
        private void DivideByTen()
        {
            int lastindex = AllWords.Count - 1;
            int startindex = 0;
            int count = 0;
            while (true)
            {
                if (lastindex % 10 == 0)
                {
                    count = lastindex / 10;
                    break;
                }
                else
                {
                    lastindex -= 1;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Parts.Add(i + 1, AllWords.GetRange(startindex, AllWords.Count - startindex));
                }
                else
                {
                    Parts.Add(i + 1, AllWords.GetRange(startindex, count));
                    startindex += count;
                }
            }
        }

        /// <summary>
        /// Gasirea tuturor cuvintelor in text plus numarul lor de aparitii.
        /// </summary>
        /// <param name="inputstr">Tetxul pentru procesare.</param>
        public void PopulateWords(string inputstr,Order by)
        {

            inputstr = inputstr.ToLower();
            MatchCollection matches = Regex.Matches(inputstr, @"\b\w+\b", RegexOptions.None);
            foreach (Match item in matches)
            {
                AllWords.Add(item.Value);
                if (!Words.Contains(new WordClass() { Word = item.Value }))
                {
                    Words.Add(new WordClass() { Word = item.Value });
                }
                else
                {
                    Words[Words.FindIndex(a => a.Word == item.Value)].Count += 1;
                }
            }

            //Ordering by the given Order
            switch (by)
            {
                case Order.None:
                    break;
                case Order.ByCountAscending:
                    Words = Words.OrderBy(a => a.Count).ToList();
                    break;
                case Order.ByCountDescending:
                    Words = Words.OrderBy(a => a.Count).ToList();
                    Words.Reverse();
                    break;
                case Order.ByWordAscending:
                    Words = Words.OrderBy(a => a.Word).ToList();
                    break;
                case Order.ByWordDescending:
                    Words = Words.OrderBy(a => a.Word).ToList();
                    Words.Reverse();
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
