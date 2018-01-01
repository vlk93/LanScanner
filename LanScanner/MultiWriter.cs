using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LanScanner
{
    class MultiWriter : TextWriter
    {
        private Control textbox;
        private string sciezka;

        public MultiWriter (Control textbox, string sciezka = @"log.txt") //domyślna ścieżka - lokalizacja programu
        {
            this.textbox = textbox;
            this.sciezka = sciezka;
            
        }

        public override void Write(string tekst)
        {
            textbox.Text += tekst;
            textbox.Text += Environment.NewLine;

            if (File.Exists(sciezka))
            {
                using (StreamWriter writetext = new StreamWriter(File.Open(sciezka, FileMode.Append)))
                {
                    writetext.WriteLine(tekst + Environment.NewLine);
                }
            }
            else
            {
                using (StreamWriter writetext = new StreamWriter(File.Open(sciezka, FileMode.CreateNew)))
                {
                    writetext.WriteLine(tekst + Environment.NewLine);
                }
            }


        }

        public override void Write(char tekst)
        {
            textbox.Text += tekst;
            textbox.Text += Environment.NewLine;

            if (File.Exists(sciezka))
            {
                using (StreamWriter writetext = new StreamWriter(File.Open(sciezka, FileMode.Append)))
                {
                    writetext.WriteLine(tekst + Environment.NewLine);
                }
            }
            else
            {
                using (StreamWriter writetext = new StreamWriter(File.Open(sciezka, FileMode.CreateNew)))
                {
                    writetext.WriteLine(tekst + Environment.NewLine);
                }
            }
        }
        
        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
