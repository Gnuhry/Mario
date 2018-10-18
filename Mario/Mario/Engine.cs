using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public class Engine
    {
        private Control[,] controls;
        private int pointer, gameWidth;
        List<Control> gameControls = new List<Control>();
        private Control.ControlCollection controlCollection;

        public Engine(Control[,] controls, Control.ControlCollection controlCollection)
        {
            this.controls = controls;
            this.controlCollection = controlCollection;
            pointer = 0;
            gameWidth = Screen.PrimaryScreen.WorkingArea.Width / Settings.width;
            DisplayBackground();
        }
        private void DisplayBackground()
        {
            for (int row = pointer; row < gameWidth && row < 20; row++)
            {
                for (int column = 0; column < Settings.gamehight && column < 9; column++)
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
        private void MoveBackgroundLeft()
        {
            //TODO alles nach rechts Move
            //ganz rechts die Controls löschen
            //links neue Controls hinzufügen
        }
        private void MoveBackgroundRight()
        {
            //TODO alles nach links Move
            //ganz links die Controls löschen
            //rechts neue Controls hinzufügen
        }
    }
}
