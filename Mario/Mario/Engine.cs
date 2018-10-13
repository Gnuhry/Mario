﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public class Engine
    {
        private Control[] controls;
        private Rectangle[] obstacles;
        private Player player;
        private Timer timer;
        private Items items;

        public Engine(Control[] controls, Player player, Items items)
        {
            obstacles = new Rectangle[] { new Rectangle(0, 400, 1000, 1) };
            this.controls = controls;
            this.player = player;
            this.items = items;
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
        private void SetObstacles(Control[] controls)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            foreach (Control control in controls)
            {
                if (control.Tag.Equals("obstacle"))
                {
                    rectangles.Add(new Rectangle(control.Location, control.Size));
                }
            }
            obstacles = rectangles.ToArray();
        }
        private void DisplayBackground(ref Control.ControlCollection controlCollection)
        {
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
        private void MoveBackgroundUp(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach unten Move
            //ganz unten die Controls löschen
            //oben neue Controls hinzufügen
        }
        private void MoveBackgroundDown(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach oben Move
            //ganz oben die Controls löschen
            //unten neue Controls hinzufügen
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
                    items.DoubleJump = false;
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
