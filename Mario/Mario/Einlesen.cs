using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mario
{
    class Einlesen
    {
        string Pfad;
        public Einlesen(int Level)
        {
<<<<<<< HEAD
            Pfad = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9) + "Level\\" + Level + ".txt";
=======
            this.Pfad = Pfad;
            //test 
>>>>>>> 19c56b59ee62df4132153910b80d13fd924b23bd
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
        public Control[] DateiInterpretieren()
        {
            List<Control> controls = new List<Control>();
            string[] Datei = TextDateiEinlesen();
            for (int Reihe = 0; Reihe < Datei.Length; Reihe++)
            {
                if (Datei[Reihe].Length != Datei[0].Length) return null;
                for (int Spalte = 0; Spalte < Datei[0].Length; Spalte++)
                {
                    switch (Datei[Reihe].ToCharArray()[Spalte])
                    {
                        case ' ': controls.Add(NewControl(Spalte, Reihe, "", "")); break;
                    }
                }
            }
            return controls.ToArray();
        }
        private PictureBox NewControl(int Spalte, int Reihe, string ImagePfad, string Tag)
        {
            PictureBox temp = new PictureBox();
            temp.Size = new Size(Settings.Weite, Settings.Lange);
            temp.Location = new Point(Settings.Lange * (Spalte + 1), Settings.Weite * (Reihe + 1));
            temp.Image = Image.FromFile(ImagePfad);
            temp.Tag = Tag;
            return temp;
        }
    }
}
