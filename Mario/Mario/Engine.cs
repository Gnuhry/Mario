using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public class Engine
    {
        private Control[,] controls;
        private Rectangle[] obstacles, itemboxes;
        private List<Rectangle> item;
        private Player player;
        private Timer timer;
        private int pointer, gameWidth;
        private Items items;
        private Control.ControlCollection controlCollection;

        public Engine(Control[,] controls, Player player, Items items, Control.ControlCollection controlCollection)
        {
            obstacles = new Rectangle[] { new Rectangle(0, 400, 1000, 1) };
            item = new List<Rectangle>();
            this.controls = controls;
            this.player = player;
            this.controlCollection = controlCollection;
            pointer = 0;
            gameWidth = Screen.PrimaryScreen.WorkingArea.Width / Settings.width;
            this.items = items;
            DisplayBackground();
            timer = new Timer();
            timer.Interval = Settings.timerlenght;
            timer.Tick += Timer;
            timer.Start();
        }
        public bool CollisionDetect(Rectangle rectangle)
        {
            foreach (Rectangle obstacle in obstacles)
            {
                if (rectangle.IntersectsWith(obstacle))
                {
                    return false;
                }
            }
            foreach (Rectangle itembox in itemboxes)
            {
                if (rectangle.IntersectsWith(itembox))
                {
                    return false;
                }
            }
            for (int f = 0; f < item.Count; f++)
            {
                if (rectangle.IntersectsWith(item[f]))
                {
                    items.PickUpItem(f, controlCollection);
                    item.RemoveAt(f);
                    return true;
                }
            }
            return true;
        }
        private bool ItemBox(Rectangle rectangle)
        {
            foreach (Rectangle itembox in itemboxes)
            {
                if (rectangle.IntersectsWith(itembox))
                {
                    return true;
                }
            }
            return false;
        }
        private void SetObstacles(Control[] controls)
        {
            List<Rectangle> rectangles = new List<Rectangle>(),
                rectangles2 = new List<Rectangle>();

            foreach (Control control in controls)
            {
                if (control.Tag.Equals("obstacle"))
                {
                    rectangles.Add(new Rectangle(control.Location, control.Size));
                }
                else if (control.Tag.Equals("itembox"))
                {
                    rectangles2.Add(new Rectangle(control.Location, control.Size));
                }
            }
            rectangles.Add(new Rectangle(0, 400, 1000, 1));
            obstacles = rectangles.ToArray();
            itemboxes = rectangles2.ToArray();
        }
        private void DisplayBackground()
        {
            List<Control> gameControls = new List<Control>();
            for (int row = pointer; row < gameWidth && row < 10; row++)
            {
                for (int column = 0; column < Settings.gamehight && column < 3; column++)
                {

                    gameControls.Add(controls[row, column]);
                    controls[row, column].Location = new Point((row - pointer) * Settings.width, column * Settings.hight);
                    controlCollection.Add(controls[row, column]);
                }
            }

            SetObstacles(gameControls.ToArray());
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


        private void Timer(object sender, EventArgs e)
        {
            int x = 0, y = 0;
            if (player.jumping && player.jump)
            {
                if (player.jumpCounter-- <= 0)
                {
                    player.jumping = false;
                    return;
                }
                if (CollisionDetect(new Rectangle(player.control.Location.X, player.control.Location.Y - Settings.speedX, player.control.Size.Width, player.control.Size.Height)))
                {
                    x = -Settings.speedX;
                }
                else if (ItemBox(new Rectangle(player.control.Location.X, player.control.Location.Y - Settings.speedX, player.control.Size.Width, player.control.Size.Height)))
                {
                    item.Add(items.GetRandomItem(controlCollection, player.control.Location));
                }
            }
            else
            {
                if (CollisionDetect(new Rectangle(player.control.Location.X, player.control.Location.Y + Settings.speedX, player.control.Size.Width, player.control.Size.Height)))
                {
                    x = Settings.speedX;
                }
                else
                {
                    player.jumping = true;
                    player.jumpCounter = Settings.jumpspeed;
                    items.doubleJump = false;
                }
            }
            if (player.right && player.left)
            {
                player.right = player.left = false;
            }
            else if (player.right && CollisionDetect(new Rectangle(player.control.Location.X + Settings.speedX, player.control.Location.Y, player.control.Size.Width, player.control.Size.Height)))
            {
                y = Settings.speedY;
            }
            else if (player.left && CollisionDetect(new Rectangle(player.control.Location.X - Settings.speedX, player.control.Location.Y, player.control.Size.Width, player.control.Size.Height)))
            {
                y = -Settings.speedY;
            }
            player.Move(y, x);
        }
    }
}
