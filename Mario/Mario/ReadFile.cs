using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mario
{
    class ReadFile
    {
        string Path;
        static string DefaultPath = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9);
        public ReadFile(int Level)
        {
            Path = DefaultPath + "Level\\" + Level + ".txt";
        }
        private string[] readTextFile()
        {
            List<string> file = new List<string>();
            if (!File.Exists(Path)) return null;
            StreamReader reader = new StreamReader(Path);
            while (!reader.EndOfStream)
                file.Add(reader.ReadLine());
            reader.Dispose();
            return file.ToArray();
        }
        public Control[] interpretFile()
        {
            List<Control> controls = new List<Control>();
            string[] file = readTextFile();
            for (int row = 0; row < file.Length; row++)
            {
                if (file[row].Length != file[0].Length) return null;
                for (int column = 0; column < file[0].Length; column++)
                {
                    switch (file[row].ToCharArray()[column])
                    {
                        case ' ': controls.Add(NewControl(column, row, "", "")); break;
                    }
                }
            }
            return controls.ToArray();
        }
        private PictureBox NewControl(int column, int row, string ImagePath, string Tag)
        {
            PictureBox temp = new PictureBox();
            temp.Size = new Size(Settings.Width, Settings.Hidth);
            temp.Location = new Point(Settings.Hidth * (column + 1), Settings.Width * (row + 1));
            temp.Image = Image.FromFile(ImagePath);
            temp.Tag = Tag;
            return temp;
        }
    }
}
