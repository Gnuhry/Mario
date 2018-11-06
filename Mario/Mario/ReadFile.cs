using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mario
{
    public class ReadFile
    {
        private string path;
        private string data;

        public ReadFile(string Level)
        {
            path = Settings.textFilePath + Level + ".txt";
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
            Control[][] erg = new Control[file[1].Length][];
            for (int f = 0; f < erg.Length; f++)
            {
                erg[f] = new Control[file.Length - 1];
            }
            int row = 0;
            Console.WriteLine(file.Length + "," + file[0].Length);
            for (int f = 0; f < file.Length; f++)
            {
                if (file[f].StartsWith("#"))
                {
                    //data from level
                    data = file[f];
                }
                else
                {
                    if (file[f].Length != file[1].Length)
                    {
                        return null;
                    }
                    for (int column = 0; column < file[1].Length; column++)
                    {
                        switch (file[f].ToCharArray()[column])
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
                                erg[column][row] = new Players(settings, this);
                                break;
                            case 'e':
                                erg[column][row] = new Enemy(false);
                                break;
                            case 'E':
                                erg[column][row - 1] = null;
                                erg[column][row] = new Enemy(true);
                                break;
                            case 'C':
                                erg[column][row] = NewControl(Properties.Resources.coin, "coin");
                                break;
                            case '1':
                                erg[column][row] = NewControl(Properties.Resources.star, "star_1");
                                break;
                            case '2':
                                erg[column][row] = NewControl(Properties.Resources.star, "star_2");
                                break;
                            case '3':
                                erg[column][row] = NewControl(Properties.Resources.star, "star_3");
                                break;
                        }

                    }
                    row++;
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
        public string GetData() => data;
        public string SearchData()
        {
            foreach (string help in ReadTextFile())
            {
                if (help.StartsWith("#"))
                {
                    return help;
                }
            }
            return "";
        }

        //-----------------------Write---------------------------------------
        public void SetData(string data)
        {
            string[] help = ReadTextFile();
            for (int f = 0; f < help.Length; f++)
            {
                if (help[f].StartsWith("#"))
                {
                    help[f] = data;
                }
            }
            File.Delete(path);
            File.WriteAllLines(path, help);
        }
    }
}
