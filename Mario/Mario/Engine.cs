using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Mario
{
    public class Engine
    {
        private Control[][] controls;
        private int pointer, gameWidth;
        private double border;
        List<Control> gameControls;
        private Control.ControlCollection controlCollection;
        private System.Windows.Forms.Timer timer;

        public Engine(Control[][] controls, Control.ControlCollection controlCollection)
        {
            gameControls = new List<Control>();
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Start();
            this.controls = controls;
            this.controlCollection = controlCollection;
            pointer = 0;
            gameWidth = Screen.PrimaryScreen.WorkingArea.Width / Settings.width;
            border = gameWidth * Settings.width * Settings.borderFactor;
            DisplayBackground();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoveBackgroundLeft();
            MoveBackgroundRight();
        }

        private void DisplayBackground()
        {
            for (int row = pointer; row < pointer + gameWidth && row < pointer + controls.Length; row++)
            {
                //TODO get length
                for (int column = 0; column < Settings.gamehight && column < controls[0].Length; column++)
                {
                    if (controls[row][column] != null)
                    {
                        gameControls.Add(controls[row][column]);
                        controls[row][column].Location = new Point((row - pointer) * Settings.width, column * Settings.height);
                        controlCollection.Add(controls[row][column]);
                        if (controls[row][column].Tag.Equals("players"))
                        {
                            gameControls.Remove(controls[row][column]);
                            controls[row][column].Location = new Point((row - pointer) * Settings.width, column * Settings.height - Settings.height);
                            controls[row][column] = null;
                        }
                    }
                }
            }

        }
        public Control GetPlayer()
        {
            foreach (Control control in controlCollection)
            {
                if (control.GetType().Equals((typeof(Players))))
                {
                    return control;
                }
            }
            return null;
        }
        private void MoveBackgroundLeft()
        {
            if (pointer <= 1) return;
            Point help = GetPlayer().Location;
            if (help.X < border)
            {
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer + gameWidth-1][f] != null)
                    {
                        int index = gameControls.IndexOf(controls[pointer + gameWidth-1][f]);
                        if (index != -1)
                        {
                            controlCollection.Remove(gameControls[index]);
                            gameControls.RemoveAt(index);
                        }
                    }
                }
                foreach (Control control in gameControls)
                {

                    Point location = control.Location;
                    location.Offset(Settings.width, 0);
                    control.Location = location;
                }
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer-1][f] != null)
                    {
                        controls[pointer-1][f].Location = new Point(0, f * Settings.height);
                        gameControls.Add(controls[pointer-1][f]);
                        controlCollection.Add(controls[pointer-1][f]);
                    }
                }
                pointer--;
                Point temp = GetPlayer().Location;
                temp.Offset(Settings.width, 0);
                GetPlayer().Location = temp;
            }
        }
        private void MoveBackgroundRight()
        {
            if (pointer >= controls.Length-gameWidth) return;
            Point help = GetPlayer().Location;
            if (help.X > gameWidth * Settings.width - border)
            {
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer][f] != null)
                    {
                        int index = gameControls.IndexOf(controls[pointer][f]);
                        if (index != -1)
                        {
                            controlCollection.Remove(gameControls[index]);
                            gameControls.RemoveAt(index);
                        }
                    }
                }
                foreach (Control control in gameControls)
                {
                    Point location = control.Location;
                    location.Offset(-Settings.width, 0);
                    control.Location = location;
                }
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer + gameWidth][f] != null)
                    {
                        controls[pointer + gameWidth][f].Location = new Point((gameWidth-1) * Settings.width, f * Settings.height);
                        gameControls.Add(controls[pointer + gameWidth][f]);
                        controlCollection.Add(controls[pointer + gameWidth][f]);
                    }
                }
                pointer++;
                Point temp = GetPlayer().Location;
                temp.Offset(-Settings.width, 0);
                GetPlayer().Location = temp;
                }
        }
    }
}

