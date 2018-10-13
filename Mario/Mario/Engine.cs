﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public class Engine
    {
        private Control[,] controls;
        private Rectangle[] obstacles;
        private Player player;
        private Timer timer;
        private int pointer,gameWidth;

        public Engine(Control[,] controls, Player player)
        {
            obstacles = new Rectangle[] { new Rectangle(0, 400, 1000, 1) };
            this.controls = controls;
            this.player = player;
            pointer = 0;
            gameWidth = Screen.PrimaryScreen.WorkingArea.Width / Settings.width;
            timer = new Timer();
            timer.Interval = Settings.timerlenght;
            timer.Tick += Timer;
            timer.Start();
        }
        public bool collisionDetect(Rectangle rectangle)
        {
            foreach (Rectangle obstacle in obstacles)
            {
                if (rectangle.IntersectsWith(obstacle)) return false;
            }
            return true;
        }
        private void SetObstacles(Control[]controls)
        {
            List<Rectangle> rectangles = new List<Rectangle>();

            foreach (Control control in controls)
            {
                if (control.Tag.Equals("obstacle"))
                {
                    rectangles.Add(new Rectangle(control.Location, control.Size));
                }
            }
            rectangles.Add(new Rectangle(0, 400, 1000, 1));
            obstacles = rectangles.ToArray();
        }
        public void DisplayBackground(Control.ControlCollection controlCollection)
        {
            List<Control> gameControls = new List<Control>();
            for (int row = pointer;row < gameWidth&& row < 10;row++)
            {
                for(int column = 0;column < Settings.gamehight&& column < 3;column++)
                {

                    gameControls.Add(controls[row, column]);
                    controls[row, column].Location = new Point((row - pointer)*Settings.width, column*Settings.hight);
                    controlCollection.Add(controls[row, column]);
                }
            }

            SetObstacles(gameControls.ToArray());
            //TODO abfrage nach bildschirmgröße und spielpixel anzahl
            //anzeigen der möglichen
            //die anderen nicht hinzufügen
            //SetObstacles();
        }
        private void MoveBackgroundLeft(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach rechts Move
            //ganz rechts die Controls löschen
            //links neue Controls hinzufügen
        }
        private void MoveBackgroundRight(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach links Move
            //ganz links die Controls löschen
            //rechts neue Controls hinzufügen
        }
       
        
        private void Timer(object sender, EventArgs e)
        {
            int x = 0, y = 0;
            if (player.jumping && player.jump)
            {
                if (player.JumpCounter-- <= 0)
                {
                    player.jumping = false;
                    return;
                }
                if (collisionDetect(new Rectangle(player.control.Location.X, player.control.Location.Y - Settings.speedX, player.control.Size.Width, player.control.Size.Height)))
                {
                    x = -Settings.speedX;
                }
            }
            else
            {
                if (collisionDetect(new Rectangle(player.control.Location.X, player.control.Location.Y + Settings.speedX, player.control.Size.Width, player.control.Size.Height)))
                {
                    x = Settings.speedX;
                }
                else
                {
                    player.jumping = true;
                    player.JumpCounter = Settings.jumpspeed;
                }
            }
            if (player.right && player.left)
            {
                player.right = player.left = false;
            }
            else if (player.right && collisionDetect(new Rectangle(player.control.Location.X + Settings.speedX, player.control.Location.Y, player.control.Size.Width, player.control.Size.Height)))
            {
                y = Settings.speedY;
            }
            else if (player.left && collisionDetect(new Rectangle(player.control.Location.X - Settings.speedX, player.control.Location.Y, player.control.Size.Width, player.control.Size.Height)))
            {
                y = -Settings.speedY;
            }
            player.Move(y, x);
        }
    }
}
