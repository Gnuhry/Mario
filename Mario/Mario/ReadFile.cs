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
            if (-1000 == Level) return;
            path = defaultPath + "Level\\" + Level + ".txt";
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
        public Control[][] InterpretFile(Settings settings)
        {

            string[] file = ReadTextFile();
            Control[][] erg = new Control[file[0].Length][];
            for(int f=0;f<erg.Length;f++)
            {
                erg[f]=new Control[file.Length];
            }
               
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
                        case 'S':
                            erg[column][row] = NewControl(Properties.Resources.stone, "obstacle");
                            break;
                        case 'D':
                            erg[column][row] = NewControl(Properties.Resources.grass, "obstacle");
                            break;
                        case 'c':
                            erg[column][row] = NewControl(Properties.Resources.stone_grass, "obstacle_coin");
                            break;
                        case 'A':
                            erg[column][row] = null;
                            break;
                        case 'I':
                            erg[column][row] = new Itembox();
                            break;
                        case 'P':
                            erg[column][row] = new Players(settings);
                            break;
                        case 'E':
                            erg[column][row] = new Enemy();
                            break;
                        case 'C':
                            erg[column][row] = NewControl(Properties.Resources.coin, "coin");
                            break;
                    }
                }
            }

            return erg;
        }
        public PictureBox NewControl(Image img, string Tag)
        {
            PictureBox temp = new PictureBox()
            {
                Size = new Size(Settings.width, Settings.height),
                Image = img,
                Tag = Tag,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            return temp;
        }
    }
}
