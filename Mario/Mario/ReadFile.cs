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
                                erg[column][row] = NewControl(Properties.Resources.dirt, "obstacle");
                                break;
                            case 'c':
                                erg[column][row] = NewControl(Properties.Resources.dirt, "obstacle_coin");
                                break;
                            case 'Z':
                                erg[column][row - 1] = NewControl(Properties.Resources.pepper, "end");
                                erg[column-1][row - 1] = NewControl(Properties.Resources.pepper, "end");
                                erg[column-1][row] = NewControl(Properties.Resources.pepper, "end");
                                erg[column][row] = NewControl(Properties.Resources.pepper, "end");
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
                            case 'W':
                                erg[column][row] = NewControl(Properties.Resources.water, "water");
                                break;
                            case 'C':
                                erg[column][row] = NewControl(Properties.Resources.golden_rice_grain, "coin");
                                break;
                            case '1':
                                erg[column][row] = NewControl(Properties.Resources.ricecoin, "star_1");
                                break;
                            case '2':
                                erg[column][row] = NewControl(Properties.Resources.ricecoin, "star_2");
                                break;
                            case '3':
                                erg[column][row] = NewControl(Properties.Resources.ricecoin, "star_3");
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
        public static void UnlockLevel(string level)
        {
           string path = Settings.textFilePath + level + ".txt";
            List<string> file = new List<string>();
            if (!File.Exists(path))
            {
                return;
            }
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                file.Add(reader.ReadLine());
            }
            reader.Dispose();
            for(int f=0;f<file.Count;f++)
            {
                if (file[f].StartsWith("#"))
                {
                    string[] erg = file[f].Split('|');
                    erg[4] = "1";
                    file[f] = "";
                    for(int g=0;g<erg.Length-1;g++)
                    {
                        file[f] += erg[g] + "|";
                    }
                    File.Delete(path);
                    File.WriteAllLines(path,file);
                    return;
                }
            }
        }
    }
}
