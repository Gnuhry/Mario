using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Mario
{
    public class Engine
    {
        private Control[,] controls;
        private int pointer, gameWidth;
        private double border;
        List<Control> gameControls = new List<Control>();
        private Control.ControlCollection controlCollection;
        private System.Windows.Forms.Timer timer;

        public Engine(Control[,] controls, Control.ControlCollection controlCollection)
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Start();
            this.controls = controls;
            this.controlCollection = controlCollection;
            pointer = 0;
            gameWidth = Screen.PrimaryScreen.WorkingArea.Width / Settings.width;
            border = gameWidth*Settings.width * 0.1;
            DisplayBackground();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoveBackgroundLeft();
            MoveBackgroundRight();
            Console.WriteLine("<y");
        }

        private void DisplayBackground()
        {
            for (int row = pointer; row < pointer+gameWidth && row < pointer+30; row++)
            {
                //TODO get length
                for (int column = 0; column < Settings.gamehight && column < 15; column++)
                {
                    gameControls.Add(controls[row, column]);
                    controls[row, column].Location = new Point((row - pointer) * Settings.width, column * Settings.height);
                    controlCollection.Add(controls[row, column]);
                    if (controls[row, column].Tag.Equals("players"))
                    {
                        gameControls.Remove(controls[row, column]);
                        controls[row, column].Location = new Point((row - pointer) * Settings.width, column * Settings.height - Settings.height);
                        controls[row, column] = new ReadFile(-1000).NewControl(Properties.Resources.air, "");
                        gameControls.Add(controls[row, column]);
                        controls[row, column].Location = new Point((row - pointer) * Settings.width, column * Settings.height);
                        controlCollection.Add(controls[row, column]);
                    }
                }
            }

        }
        private Control GetPlayer()
        {
            foreach(Control control in controlCollection)
            {
                if (control.GetType().Equals((typeof(Players))))
                {
                    return control;
                }
            }
            throw new Exception("Kein Spieler");
        }
        private void MoveBackgroundLeft()
        {
            if (pointer <= 1) return;
            Point help = GetPlayer().Location;
            if (help.X < border)
            {
                pointer--;           
                foreach (Control control in gameControls)
                {
                    for (int f = 0; f < 15; f++)
                    {
                        if (control.Equals(controls[pointer + gameWidth, f]))
                        {
                            controlCollection.Remove(control);
                        }
                    }
                    Point location = control.Location;
                    location.Offset(Settings.width, 0);
                    control.Location = location;
                }
                for (int f = 0; f < 15; f++)
                {
                    controls[pointer - 1, f].Location = new Point(0, f * Settings.height);
                    gameControls.Add(controls[pointer - 1, f]);
                    controlCollection.Add(controls[pointer - 1, f]);
                }
                Point temp = GetPlayer().Location;
                temp.Offset(Settings.width, 0);
                GetPlayer().Location = temp;

                //TODO alles nach rechts Move
                //ganz rechts die Controls löschen
                //links neue Controls hinzufügen
            }
        }
        private void MoveBackgroundRight()
        {
            Point help = GetPlayer().Location;
            if (help.X > gameWidth*Settings.width-border)
            {
                pointer++;
                foreach (Control control in gameControls)
                {
                    for(int f = 0; f < 15; f++)
                    {
                        if (control.Equals(controls[pointer -1, f]))
                        {
                            controlCollection.Remove(control);
                        }
                    }
                    Point location = control.Location;
                    location.Offset(-Settings.width, 0);
                    control.Location = location;
                }
                for (int f = 0; f < 15; f++)
                {
                    controls[pointer + gameWidth, f].Location=new Point((gameWidth-1) * Settings.width, f * Settings.height);
                    gameControls.Add(controls[pointer + gameWidth, f]);
                    controlCollection.Add(controls[pointer+gameWidth, f]);
                }
                Point temp = GetPlayer().Location;
                temp.Offset(-Settings.width, 0);
                GetPlayer().Location = temp;
            }
            //TODO alles nach links Move
            //ganz links die Controls löschen
            //rechts neue Controls hinzufügen
        }
    }
}

