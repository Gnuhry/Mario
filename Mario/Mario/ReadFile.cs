using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mario
{
    class ReadFile
    {
        static string DefaultPath = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9);
        string Path,
        imgStone = DefaultPath + "img\\stone.jpg",
        imgDirt = DefaultPath + "img\\dirt.jpg";

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
        public Control[,] interpretFile()
        {

            string[] file = readTextFile();
            Control[,] erg = new Control[file[0].Length, file.Length];
            Console.WriteLine(file.Length + "," + file[0].Length);
            for (int row = 0; row < file.Length; row++)
            {
                if (file[row].Length != file[0].Length) return null;
                for (int column = 0; column < file[0].Length; column++)
                {
                    switch (file[row].ToCharArray()[column])
                    {
                        case '1': erg[column, row] = NewControl(imgStone, "obstacle"); break;
                        case '2': erg[column, row] = NewControl(imgDirt, ""); break;
                    }
                }
            }

            return erg;
        }
        private PictureBox NewControl(string ImagePath, string Tag)
        {
            PictureBox temp = new PictureBox();
            temp.Size = new Size(Settings.width, Settings.hight);
            temp.Image = Image.FromFile(ImagePath);
            temp.Tag = Tag;

            return temp;
        }
    }
}
