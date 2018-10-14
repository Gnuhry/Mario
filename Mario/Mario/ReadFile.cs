using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mario
{
    class ReadFile
    {
        private static string defaultPath = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9);
        private string path;

        public ReadFile(int Level)
        {
            path = defaultPath + Settings.levelPath + Level + ".txt";
        }
        private string[] ReadTextFile()
        {
            List<string> file = new List<string>();
            if (!File.Exists(path))
            {
                return null;
            }
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                file.Add(reader.ReadLine());
            }
            reader.Dispose();
            return file.ToArray();
        }
        public Control[,] InterpretFile()
        {

            string[] file = ReadTextFile();
            Control[,] erg = new Control[file[0].Length, file.Length];
            Console.WriteLine(file.Length + "," + file[0].Length);
            for (int row = 0; row < file.Length; row++)
            {
                if (file[row].Length != file[0].Length)
                {
                    return null;
                }
                for (int column = 0; column < file[0].Length; column++)
                {
                    switch (file[row].ToCharArray()[column])
                    {
                        case '1':
                            erg[column, row] = NewControl(defaultPath + Settings.imgStone, "obstacle");
                            break;
                        case '2':
                            erg[column, row] = NewControl(defaultPath + Settings.imgDirt, "");
                            break;
                    }
                }
            }

            return erg;
        }
        private PictureBox NewControl(string Imagepath, string Tag)
        {
            PictureBox temp = new PictureBox();
            temp.Size = new Size(Settings.width, Settings.hight);
            temp.Image = Image.FromFile(Imagepath);
            temp.Tag = Tag;
            return temp;
        }
    }
}
