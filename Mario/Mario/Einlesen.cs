using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    class Einlesen
    {
        static int Weite=1, Lange=1;
        string Pfad;
        public Einlesen(string Pfad)
        {
            this.Pfad = Pfad;
        }
        private string[] TextDateiEinlesen()
        {
            List<string> Datei = new List<string>();
            if (!File.Exists(Pfad)) return null;
            StreamReader reader = new StreamReader(Pfad);
            while (!reader.EndOfStream)
                Datei.Add(reader.ReadLine());
            reader.Dispose();
            return Datei.ToArray();
        }
         private Control[] DateiInterpretieren()
        {
            List<Control> controls = new List<Control>();
            string[] Datei = TextDateiEinlesen();
            for(int Reihe = 0; Reihe < Datei.Length; Reihe++)
            {
                if (Datei[Reihe].Length != Datei[0].Length) return null;
                for(int Spalte = 0; Spalte < Datei[0].Length; Spalte++)
                {
                    switch (Datei[Reihe].ToCharArray()[Spalte])
                    {
                        case ' ': controls.Add(NewControl(Spalte,Reihe,"","")); break;
                    }
                }
            }
            return controls.ToArray();
        }   
        private PictureBox NewControl(int Spalte, int Reihe, string ImagePfad,string Tag)
        {
            PictureBox temp = new PictureBox();
            temp.Size = new Size(Weite,Lange);
            temp.Location = new Point(Lange * (Spalte + 1), Weite * (Reihe + 1));
            temp.Image = Image.FromFile(ImagePfad);
            temp.Tag = Tag;
            return temp;
        }
    }
}
