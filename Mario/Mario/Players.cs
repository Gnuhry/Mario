using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    public partial class Players : UserControl
    {
        private Timer player_timer;
        private Settings settings;
        private pause pause;
        public Players(Settings settings)
        {
            InitializeComponent();
            Init(settings);
            InitItem();
            InitGameControl();
            InitKeyPressEvents();
            InitPlayerTimer();
        }
        public void Start()
        {
            player_timer.Start();
            StartEnemies();
        }
        private void Init(Settings settings)
        {
            this.settings = settings;
            Tag = "players";
            Size = new Size(Settings.width, 2 * Settings.height);
            BackColor = Color.Blue;
        }


        //---------------------------------------------Keys-------------------------------------------------------

        private bool up, left, right;
        private void InitKeyPressEvents()
        {
            up = left = right = false;
            KeyDown += Players_KeyDown;
            KeyUp += Players_KeyUp;
        }

        private void Players_KeyUp(object sender, KeyEventArgs e)
        {
            char key = Convert.ToChar(e.KeyValue);
            if (key.Equals(settings.left))
            {
                left = false;
            }
            else if (key.Equals(settings.right))
            {
                right = false;
            }
            else if (key.Equals(settings.up))
            {
                up = false;
            }
        }

        private void Players_KeyDown(object sender, KeyEventArgs e)
        {
            char key = Convert.ToChar(e.KeyValue);
            if (key.Equals(settings.left))
            {
                left = true;
                lastRight = false;
            }
            else if (key.Equals(settings.right))
            {
                right = true;
                lastRight = true;
            }
            else if (key.Equals(settings.up))
            {
                up = true;
            }
            else if (key.Equals(settings.item))
            {
                UseItem();
            }
            else if (e.KeyData.Equals(Keys.Escape))
            {
                Pause();
            }
        }


        //---------------------------------------------Tick/Move-----------------------------------------------------
        private bool jump;
        private int jumpCounter;
        private void InitPlayerTimer()
        {
            player_timer = new Timer();
            player_timer.Interval = 50;
            player_timer.Tick += Player_timer_Tick;
        }

        private void Stop()
        {
            player_timer.Stop();
            StopEnemies();
        }

        private void Player_timer_Tick(object sender, EventArgs e)
        {
            Point offset = new Point();
            Focus();
            BringToFront();
            ChangeTexture();
            offset.Y = CheckY();
            offset.X = CheckX(offset.Y);
            ItemTimer();
            Moving(offset);
        }

        private void Moving(Point offset)
        {
            Point help = Location;
            help.Offset(offset);
            Location = help;
        }

        private int CheckX(int offsetY)
        {
            if (right && left)
            {
                return 0;
            }
            else if (right)
            {
                Rectangle help = GetBounds();
                help.Offset(Settings.speedX, offsetY);
                if (CollisionDetect(help, false, false, true, false))
                {
                    return Settings.speedY;
                }
                else
                {
                    return 0;
                }
            }
            else if (left)
            {
                Rectangle help = GetBounds();
                help.Offset(-Settings.speedX, offsetY);
                if (CollisionDetect(help, false, false, true, false))
                {
                    return -Settings.speedX;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        private int CheckY()
        {
            if (up && jump)
            {
                if (jumpCounter > 0)
                {
                    jumpCounter--;
                }
                else
                {
                    if (doubleJumping)
                    {
                        doubleJumping = false;
                        jump = true;
                    }
                    else
                    {
                        jump = false;
                    }
                }
                Rectangle help = GetBounds();
                help.Offset(0, -Settings.speedY);
                if (CollisionDetect(help, true, false, true, false))
                {
                    return -Settings.speedY;
                }
                else
                {
                    //TODO Jump=false? Wenn Kopf wand berührt
                    return 0;
                }
            }
            else
            {
                Rectangle help = GetBounds();
                help.Offset(0, Settings.speedY);
                if (CollisionDetect(help, false, true, true, false))
                {
                    return Settings.speedY;
                }
                else
                {
                    jump = true;
                    jumpCounter = Settings.jumpspeed;
                    doubleJumping = doubleJump;
                    return 0;
                }
            }
        }

        //--------------------------------------------Collision Detect--------------------------------------------------
        public bool CollisionDetect(Rectangle location, bool up, bool down, bool player, bool destroyEnemyItem)
        {
            foreach (Control control in Parent.Controls)
            {
                if (control.Bounds.IntersectsWith(location))
                {
                    if (control.Tag != null)
                    {
                        if (control.Tag.Equals("obstacle"))
                        {
                            return false;
                        }
                        else if (control.Tag.ToString().Split('_')[0].Equals("Item") && player)
                        {
                            PickItem(control);
                            return true;
                        }
                        else if (control is Itembox)
                        {
                            if (up)
                            {
                                gameControls.Add((control as Itembox).Activate(!mushroom));
                            }
                            return false;
                        }
                        else if (control is Enemy)
                        {
                            if (down || (invincible && itemCounter > 0) || destroyEnemyItem)
                            {
                                (control as Enemy).Hit();
                            }
                            else if (player)
                            {
                                Hit();
                            }
                        }
                    }
                }

            }
            return true;
        }



        //-------------------------------------------Item--------------------------------------------------------------
        private bool riceBall, doubleJump, mushroom, bumerang, invincible, lastRight, currentRight, doubleJumping;
        private int itemCounter;
        private PictureBox itemThrow;

        private void InitItem()
        {
            riceBall = doubleJump = bumerang = invincible = false;
            itemCounter = 0;
            itemThrow = new PictureBox()
            {
                Size = Settings.size,
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }
        private void ItemTimer()
        {
            if (itemCounter > 0)
            {
                if (riceBall || bumerang)
                {
                    Point help = itemThrow.Location;
                    if (lastRight)
                    {
                        help.Offset(Settings.width, 0);

                    }
                    else
                    {
                        help.Offset(-Settings.width, 0);
                    }
                    if (CollisionDetect(new Rectangle(help, Settings.size), false, false, false, false))
                    {
                        if (riceBall)
                        {
                            help.Offset(0, Settings.height);
                            if (!CollisionDetect(new Rectangle(help, Settings.size), false, false, false, true))
                            {
                                help.Offset(0, -Settings.height);
                                CollisionDetect(new Rectangle(help, Settings.size), false, false, false, true);
                            }
                        }
                        itemThrow.Location = help;
                    }
                    else
                    {
                        itemCounter = 1;
                    }
                }
                if (--itemCounter == 0)
                {
                    if (riceBall || bumerang)
                    {
                        Parent.Controls.Remove(itemThrow);
                    }
                    invincible = false;
                }
            }
        }
        private void UseItem()
        {
            if ((riceBall || bumerang) && itemCounter == 0)
            {
                currentRight = lastRight;
                itemCounter = Settings.itemThrowBlockLength;
                Point help = Location;
                if (lastRight)
                {
                    help.Offset(Settings.width, 0);
                }
                else
                {
                    help.Offset(-Settings.width, 0);
                }
                itemThrow.Location = help;
                Parent.Controls.Add(itemThrow);
            }
        }
        private void Hit()
        {
            jumpCounter = 0;
            if (bumerang || riceBall || doubleJump)
            {
                Parent.Controls.Remove(itemThrow);
                bumerang = riceBall = doubleJump = false;
                return;
            }
            if (mushroom)
            {
                mushroom = false;
                return;
            }
        }
        private void PickItem(Control control)
        {
            int help = Convert.ToInt32(control.Tag.ToString().Split('_')[1]);
            if (help == 0)
            {
                mushroom = true;
                Parent.Controls.Remove(control);
                gameControls.Remove(control);
            }
            else if (mushroom)
            {
                Parent.Controls.Remove(control);
                gameControls.Remove(control);
                InitItem();
                Parent.Controls.Remove(itemThrow);
                switch (help)
                {
                    case 1:
                        riceBall = true;
                        itemThrow.Image = Properties.Resources.fireball;
                        break;
                    case 2:
                        invincible = true;
                        itemCounter = Settings.invincibleCounter;
                        break;
                    case 3:
                        doubleJump = true;
                        break;
                    case 4:
                        itemThrow.Image = Properties.Resources.bumarang;
                        bumerang = true;
                        break;
                }
            }
        }

        //-----------------------------------------Texture/Bounds--------------------------------------------------------
        public Rectangle GetBounds()
        {
            Rectangle erg;
            if (mushroom)
            {
                erg = Bounds;
            }
            else
            {
                Point help = Location;
                help.Offset(0, Settings.height);
                erg = new Rectangle(help, Settings.size);
            }
            return erg;
        }

        private void ChangeTexture()
        {
            return;
            throw new NotImplementedException();//TODO
        }
        //-----------------------------------------Pause-----------------------------------------------------------------------
        private void Pause()
        {
            pause = new pause(Parent as Play);
            Stop();
            jump = false;
            left = false;
            right = false;
            pause.Show();
        }

        //-----------------------------------------gameControl&Enemies-----------------------------------------------------------------
        private List<Control> gameControls;
        private List<Enemy> enemies;
        private void InitGameControl()
        {
            gameControls = new List<Control>();
            enemies = new List<Enemy>();
        }
        private void StartEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Start();
            }
        }
        private void StopEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Stop();
            }
        }
        public List<Control> GetGameControl()
        {
            return gameControls;
        }
        public void GameControlAdd(Control control)
        {
            gameControls.Add(control);
        }
        public void GameControlRemove(Control control)
        {
            gameControls.Remove(control);
        }
        public void GameControlRemoveAt(int index)
        {
            gameControls.RemoveAt(index);
        }
        public Control GetGameControlItem(int index)
        {
            return gameControls[index];
        }
        public int GetGameControlIndexOf(Control control)
        {
            return gameControls.IndexOf(control);
        }
        public void EnemyAdd(Enemy enemy)
        {
            enemies.Add(enemy);
        }
        public void EnemyRemove(Enemy enemy)
        {
            enemies.Remove(enemy);
        }
        public List<Enemy> GetEnemy()
        {
            return enemies;
        }

    }
}
