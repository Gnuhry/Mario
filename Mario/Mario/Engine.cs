using System.Collections.Generic;
using System.Drawing;
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

        public Engine(Control[,] controls, Control.ControlCollection controlCollection)
        {
            this.controls = controls;
            this.controlCollection = controlCollection;
            pointer = 0;
            gameWidth = Screen.PrimaryScreen.WorkingArea.Width / Settings.width;
            border = gameWidth * 0.20;
            DisplayBackground();
        }
        private void DisplayBackground()
        {
            for (int row = pointer; row < gameWidth && row < 20; row++)
            {
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
        private void MoveBackgroundLeft()
        {
            pointer--;
            Point help = new Point();
            help.X = 50; // Position des Spielers, die ich nicht finde 
            if (help.X >= border && pointer > 0)
            {
                for (int row = gameWidth; row < gameWidth - 1; row--)
                {
                    for (int column = 0; column <= Settings.gamehight; column++)
                    {
                        controlCollection.Remove(controls[row, column]); //Entfernen
                        gameControls.Remove(controls[row, column]);
                        for (int i = 0; controlCollection.Count >= i; i++) // Verschieben
                        {
                            controlCollection[i].Location = new Point(controlCollection[i].Location.X - Settings.width, controlCollection[i].Location.Y - Settings.height);
                        }

                        gameControls.Add(controls[row, column]);    //Neu hinzufügen
                        controls[row, column].Location = new Point((row - (pointer - 1)) * Settings.width, column * Settings.height);
                        controlCollection.Add(controls[row, column]);
                    }
                }
            }


            //TODO alles nach rechts Move
            //ganz rechts die Controls löschen
            //links neue Controls hinzufügen
        }
        private void MoveBackgroundRight()
        {
            pointer++;
            Point help = new Point();
            help.X = 50; // Position des Spielers, die ich nicht finde 
            if (help.X >= gameWidth - border)
            {
                for (int row = gameWidth - (gameWidth - (pointer - 1)); row < row + 1; row++)
                {
                    for (int column = 0; column <= Settings.gamehight; column++)
                    {
                        controlCollection.Remove(controls[row, column]);  //Entfernen
                        gameControls.Remove(controls[row, column]);

                        for (int i = 0; controlCollection.Count >= i; i++) // Verschieben
                        {
                            controlCollection[i].Location = new Point(controlCollection[i].Location.X + Settings.width, controlCollection[i].Location.Y + Settings.height);
                        }
                        gameControls.Add(controls[row, column]);    //Neue Reihe hinzufügen
                        controls[row, column].Location = new Point((row - (pointer - 1)) * Settings.width, column * Settings.height);
                        controlCollection.Add(controls[row, column]);

                    }
                }
            }

            //TODO alles nach links Move
            //ganz links die Controls löschen
            //rechts neue Controls hinzufügen
        }
    }
}

