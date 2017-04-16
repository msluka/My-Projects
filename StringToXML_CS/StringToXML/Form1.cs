using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StringToXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;

            textBox2.Text = String.Empty;

            string result = Regex.Replace(str, @"[.]", m => string.Format(@" {0}", m.Value));
            string result2 = Regex.Replace(result, @"[^\w\'\.]", " ");
            string result3 = result2.TrimStart();
            string result4 = result3.TrimEnd();
            string result5 = Regex.Replace(result4, @"\s+", " ");
            string result6 = Regex.Replace(result5, @"[^\w\'\.]", Environment.NewLine);
            string result7 = Regex.Replace(result6, @"\.", " ");
            string result8 = result7.TrimEnd();

            textBox2.Text = result8;

            XmlTextWriter xwriter = new XmlTextWriter("XML.xml", Encoding.Unicode);
            
            xwriter.Formatting = Formatting.Indented;

            xwriter.WriteStartDocument();

            xwriter.WriteStartElement("text");

            xwriter.WriteStartElement("sentence");
            
            foreach (string item in textBox2.Lines)
            {
                string space = " ";

                if (item.Equals(space))
                {
                    xwriter.WriteEndElement();
                    xwriter.WriteStartElement("sentence");
                    continue;
                }
                
                else
                {
                    xwriter.WriteElementString("word", item);
                }
                
            }
            
            xwriter.WriteEndDocument();
            xwriter.Close();

            richTextBox1.Text = File.ReadAllText("XML.xml");
        }
    }
}
