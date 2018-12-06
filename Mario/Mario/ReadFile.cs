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
                throw new Exception(path);
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
            data = file[0];
            Console.WriteLine(data);
            if (!data.StartsWith("#") || data.Split('|').Length < 5)
            {
                throw new Exception("No Data Line");
            }
            for (int f = 1; f < file.Length; f++)
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
                        case 'G':
                            erg[column][row] = NewControl(Properties.Resources.grass, "obstacle");
                            break;
                        case 'c':
                            erg[column][row] = NewControl(Properties.Resources.box, "obstacle_coin");
                            break;
                        case 'd':
                            erg[column][row] = NewControl(Properties.Resources.box, "obstacle_destroy");
                            break;
                        case 'Z':
                            erg[column - 1][row] = NewControl(Properties.Resources.end_2, "end");
                            erg[column - 2][row] = NewControl(Properties.Resources.end_1, "end");
                            erg[column][row] = NewControl(Properties.Resources.end_3, "end");
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
                        case 'w':
                            erg[column][row] = NewControl(Properties.Resources.water, "water");
                            break;
                        case 'W':
                            erg[column][row] = NewControl(Properties.Resources.water_full, "water");
                            break;
                        case 'C':
                            erg[column][row] = NewControl(Properties.Resources.golden_rice_grain, "coin");
                            break;
                        case '1':
                            if (data.Split('|')[3].Split(',')[0] == "0")
                            {
                                erg[column][row] = NewControl(Properties.Resources.ricecoin, "star_1");
                            }
                            else
                            {
                                erg[column][row] = NewControl(Properties.Resources.ricecoin_not, "star_1");
                            }
                            break;
                        case '2':
                            if (data.Split('|')[3].Split(',')[1] == "0")
                            {
                                erg[column][row] = NewControl(Properties.Resources.ricecoin, "star_2");
                            }
                            else
                            {
                                erg[column][row] = NewControl(Properties.Resources.ricecoin_not, "star_2");
                            }
                            break;
                        case '3':
                            if (data.Split('|')[3].Split(',')[2] == "0")
                            {
                                erg[column][row] = NewControl(Properties.Resources.ricecoin, "star_3");
                            }
                            else
                            {
                                erg[column][row] = NewControl(Properties.Resources.ricecoin_not, "star_3");
                            }
                            break;
                        case 'a':
                            erg[column][row] = NewControl(Properties.Resources.cloud, "cloud");
                            break;
                    }
                }
                row++;
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
            help[0] = data;
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
            for (int f = 0; f < file.Count; f++)
            {
                if (file[f].StartsWith("#"))
                {
                    string[] erg = file[f].Split('|');
                    erg[4] = "1";
                    file[f] = "";
                    for (int g = 0; g < erg.Length - 1; g++)
                    {
                        file[f] += erg[g] + "|";
                    }
                    File.WriteAllLines(path, file);
                    return;
                }
            }
        }

        //----------------------------------------Resets-------------------------------------
        public static void ResetAll(Settings settings)
        {
            foreach (string filename in GetFileNames())
            {
                if (filename == "1-1.txt")
                {
                    ResetLevel(new ReadFile(filename.Split('.')[0]), "1");
                }
                else if (!filename.ToCharArray()[0].Equals('.'))
                {
                    ResetLevel(new ReadFile(filename.Split('.')[0]), "0");
                }
            }
            settings.Default();
            SetSettings(settings);
            SetFirst("0");

        }
        public static void ResetLevel(ReadFile readFile, string four)
        {
            string[] help = readFile.SearchData().Split('|');
            help[1] = "-1";
            help[3] = "0,0,0";
            help[4] = four;
            string setdata = "";
            for (int f = 0; f < help.Length - 1; f++)
            {
                setdata += help[f] + "|";
            }
            setdata += help[help.Length - 1];
            readFile.SetData(setdata);
        }
        //-----------------------------------Filenames-----------------------------------
        private static string[] GetFileNames()
        {
            DirectoryInfo d = new DirectoryInfo(Settings.textFilePath);
            FileInfo[] files = d.GetFiles("*.txt");
            string[] str = new string[files.Length];
            for (int f = 0; f < files.Length; f++)
            {
                str[f] = files[f].Name;
            }
            return str;
        }
        //---------------------------------------Settings---------------------------------------
        public static Settings GetSettings()
        {
            string[] txt = File.ReadAllLines(Settings.textFilePath + ".global.txt");
            string[] setting = txt[1].Split(';');
            return new Settings
            {
                Left = Convert.ToChar(setting[0]),
                Right = Convert.ToChar(setting[1]),
                Up = Convert.ToChar(setting[2]),
                Item = Convert.ToChar(setting[3]),
                Music = setting[4] == "True",
                Sounds = setting[5] == "True",
                Volume = Convert.ToInt32(setting[6]) / 100.0
            };
        }
        public static void SetSettings(Settings settings)
        {
            string[] txt = File.ReadAllLines(Settings.textFilePath + ".global.txt");
            txt[1] = settings.Left + ";" + settings.Right + ";" + settings.Up + ";" + settings.Item + ";" + settings.Music + ";" + settings.Sounds + ";" + (settings.Volume * 100) + ";";
            File.WriteAllLines(Settings.textFilePath + ".global.txt", txt);
        }
        //------------------------------------------------Is first?-----------------------------------------
        public static void SetFirst(string first)
        {
            string[] txt = File.ReadAllLines(Settings.textFilePath + ".global.txt");
            txt[0] = first;
            File.WriteAllLines(Settings.textFilePath + ".global.txt", txt);
        }
        public static bool IsFirst()
        {
            string[] txt = File.ReadAllLines(Settings.textFilePath + ".global.txt");
            if (txt[0].Equals("0"))
            {
                return true;
            }
            return false;
        }
    }
}
