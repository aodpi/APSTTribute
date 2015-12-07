using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formulas;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using WordProcessing;
namespace APSTTribute
{
    public partial class Form1 : Form
    {
        string filename = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text File|*.txt";
            ofd.ShowDialog();
            filename = ofd.FileName;
            toolStripStatusLabel1.Text = "Ready!!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputstr = File.ReadAllText(filename).Replace(".", " ");
            WordProcessor ps = new WordProcessor(inputstr, WordProcessor.Order.None);
            Bayes bs = new Bayes(ps.Parts);
            string firstword = textBox1.Text;
            List<prob> lst = new List<prob>();
            List<prob> aposterioare = new List<prob>();
            List<prob> aposterioareduble = new List<prob>();
            foreach (int key in bs.ProbabilitiesByParts.Keys)
            {
                if (bs.ProbabilitiesByParts[key].Contains(new ProbabilityItem() { Word = firstword }))
                {
                    aposterioareduble.Add(new prob() { Part = key, Probability = bs.GetADoubleProbability(firstword, key, ps.Parts) });
                    aposterioare.Add(new prob() { Part = key, Probability = bs.GetAProbability(firstword, key, ps.Parts) });
                    lst.Add(new prob() { Part = key, Probability = bs.ProbabilitiesByParts[key].Find(a=>a.Word==firstword).Probability });
                }
                else
                {
                    aposterioareduble.Add(new prob() { Part = key, Probability = 0.00 });
                    aposterioare.Add(new prob() { Part = key, Probability = 0.00 });
                    lst.Add(new prob() { Part = key, Probability = 0.00 });
                }
            }
            dataGridView1.DataSource = lst;
            dataGridView2.DataSource = aposterioare;
            dataGridView3.DataSource = aposterioareduble;
            MessageBox.Show(bs.AparitionsInPart("the", ps.Parts[1]).ToString());
        }
        
        public class prob
        {
            int part;
            double probability;

            public int Part
            {
                get
                {
                    return part;
                }

                set
                {
                    part = value;
                }
            }

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
        }
    }
}
