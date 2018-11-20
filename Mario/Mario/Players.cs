using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Mario
{
    public partial class Players : UserControl
    {
        private System.Windows.Forms.Timer player_timer;
        private Settings settings;
        private pause pause;
        private ReadFile readFile;
        public Players(Settings settings, ReadFile readFile)
        {
            InitializeComponent();
            Init(settings);
            InitItem(readFile);
            InitGameControl();
            InitKeyPressEvents();
            InitPlayerTimer();
            InitTexture();
            SizeChanged += Players_SizeChanged;
        }

        private void Players_SizeChanged(object sender, EventArgs e)
        {
            pcBPlayer.Size = Size;
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
            Size = Settings.size;
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
            else if (key.Equals(settings.item))
            {
                previtem = false;
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
                previtem = true;
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
            player_timer = new System.Windows.Forms.Timer();
            player_timer.Interval = 25;//40Hz
            player_timer.Tick += Player_timer_Tick;
        }

        public void Stop()
        {
            player_timer.Stop();
            StopEnemies();
        }
        int counter = 0;
        private void Player_timer_Tick(object sender, EventArgs e)
        {
            Focus();
            BringToFront();
            ChangeTexture();
            if (counter++ != 1)
            {
                return;
            }
            counter = 0;
            Point offset = new Point();
            offset.Y = CheckY();
            offset.X = CheckX(offset.Y);
            ItemTimer();
            Moving(offset);
            prevup = up;
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
                Rectangle help = Bounds;
                help.Offset(Settings.speedX, offsetY);
                if (CollisionDetect(help, false, false, true, false, false))
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
                Rectangle help = Bounds;
                help.Offset(-Settings.speedX, offsetY);
                if (CollisionDetect(help, false, false, true, false, false))
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
                    CheckDoubleJump();
                }
                Rectangle help = Bounds;
                help.Offset(0, -Settings.speedY);
                if (CollisionDetect(help, true, false, true, false, false))
                {
                    return -Settings.speedY;
                }
                else
                {
                    jump = false;
                    return 0;
                }
            }
            else
            {
                CheckDoubleJump();
                Rectangle help = Bounds;
                help.Offset(0, Settings.speedY);
                if (CollisionDetect(help, false, true, true, false, false))
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
        public bool CollisionDetect(Rectangle location, bool up, bool down, bool player, bool destroyEnemyItem, bool pickCoinItem)
        {
            if (Parent == null)
            {
                return false;
            }
            foreach (Control control in Parent.Controls)
            {
                if (control.Bounds.IntersectsWith(location))
                {
                    if (control.Tag != null)
                    {
                        if (control.Tag.ToString().Split('_').Length > 1 && player && (up || pickCoinItem))
                        {
                            if (control.Tag.ToString().Split('_')[1].Equals("coin"))
                            {
                                gameControls.Remove(control);
                                Parent.Controls.Remove(control);
                                Point help = control.Location;
                                help.Offset(0, -Settings.height);
                                coin.Location = help;
                                gameControls.Add(coin);
                                Parent.Controls.Add(coin);
                                coin.BringToFront();
                                (Parent as Play).SetCoin(++coinCounter);
                                coinVisibleCounter = 3; ;
                            }
                        }
                        else if (control.Tag.ToString().Split('_')[0].Equals("obstacle"))
                        {
                            return false;
                        }
                        else if (control.Tag.Equals("coin") && (player || pickCoinItem))
                        {
                            sound_music.RiceSound(settings);
                            gameControls.Remove(control);
                            Parent.Controls.Remove(control);
                            (Parent as Play).SetCoin(++coinCounter);
                            coinVisibleCounter = 3;
                            return true;
                        }
                        else if (control.Tag.ToString().Split('_')[0].Equals("star") && (player || pickCoinItem))
                        {
                            gameControls.Remove(control);
                            Parent.Controls.Remove(control);
                            star[Convert.ToInt32(control.Tag.ToString().Split('_')[1])] = true;
                            return true;
                        }
                        else if (control.Tag.Equals("end") && player)
                        {
                            (Parent as Play).CheckHighScoore();
                            (Parent as Play).GetWorlds().Visible = true;
                            (Parent as Play).GetWorlds().ShowInTaskbar = true;
                            (Parent as Play).Close();
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
                        else if (control.Tag.ToString().Split('_')[0].Equals("Item") && player)
                        {
                            PickItem(control);
                        }
                        else if (control.Tag.ToString().Equals("water"))
                        {
                            if (invincible && itemCounter > 0)
                            {
                                return true;
                            }
                            else if (player)
                            {
                                jumpCounter = 0;
                                Hit();
                            }
                        }
                        else if (control is Enemy)
                        {
                            if ((invincible && itemCounter > 0) || destroyEnemyItem)
                            {
                                (control as Enemy).Hit();
                            }
                            else if ((down && location.Bottom - control.Top < 20))
                            {
                                if (control.Tag.ToString().Split('_').Length > 1)
                                {
                                    Hit();
                                }
                                else
                                {
                                    (control as Enemy).Hit();
                                }
                            }

                            else if (player)
                            {
                                Hit();
                            }
                        }
                    }
                }

            }
            if (Parent == null)
            {
                return false;
            }
            if (location.Y + location.Height > Parent.Height)
            {
                Dead();
            }
            return true;
        }



        //-------------------------------------------Item--------------------------------------------------------------
        private bool fireBall, doubleJump, mushroom, bumerang, invincible, lastRight, currentRight, doubleJumping, prevup, previtem;
        private int itemCounter, hitCounter, coinCounter, coinVisibleCounter;
        private PictureBox itemThrow, coin;
        private bool[] star;

        private void CheckDoubleJump()
        {
            if (doubleJumping && !prevup)
            {
                jump = true;
                jumpCounter = Settings.jumpspeed;
                doubleJumping = false;
            }
            else
            {
                jump = false;
            }
        }
        private void InitItem(ReadFile readFile)
        {
            string[] help = readFile.GetData().Split('|')[3].Split(',');
            star = new bool[] { help[0] == "1", help[1] == "1", help[2] == "1" };
            fireBall = doubleJump = bumerang = invincible = false;
            itemCounter = 0;
            itemThrow = new PictureBox()
            {
                Size = Settings.size,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            coin = new PictureBox()
            {
                Size = Settings.size,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Properties.Resources.golden_rice_grain
            };

            coinCounter = coinVisibleCounter = 0;
        }
        private void ItemTimer()
        {
            if (coinVisibleCounter-- == 0)
            {
                gameControls.Remove(coin);
                Parent.Controls.Remove(coin);
            }
            if (hitCounter > 0)
            {
                hitCounter--;
            }
            if (itemCounter > 0)
            {
                if (fireBall || bumerang)
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
                    if (CollisionDetect(new Rectangle(help, Settings.size), false, false, false, false, false))
                    {
                        if (bumerang)
                        {
                            CollisionDetect(new Rectangle(help, Settings.size), false, false, false, false, true);
                        }
                        if (fireBall)
                        {
                            help.Offset(0, Settings.height);
                            if (!CollisionDetect(new Rectangle(help, Settings.size), false, false, false, true, false))
                            {
                                help.Offset(0, -Settings.height);
                                CollisionDetect(new Rectangle(help, Settings.size), false, false, false, true, false);
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
                    if (fireBall || bumerang)
                    {
                        Parent.Controls.Remove(itemThrow);
                    }
                    invincible = false;
                }

            }
        }

        private void UseItem()
        {
            if (previtem)
            {
                return;
            }
            if ((fireBall || bumerang) && itemCounter == 0)
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
            if (hitCounter > 0)
            {
                return;
            }
            jumpCounter = 0;
            if (bumerang || fireBall || doubleJump)
            {
                Parent.Controls.Remove(itemThrow);
                bumerang = fireBall = doubleJump = false;
                hitCounter = 30;
                return;
            }
            if (mushroom)
            {
                Size = Settings.size;
                mushroom = false;
                hitCounter = 30;
                return;
            }
            Dead();
        }
        private void Dead()
        {
            //TODO set Animation Dead
            (Parent as Play).Restart();
        }
        private void PickItem(Control control)
        {
            int help = Convert.ToInt32(control.Tag.ToString().Split('_')[1]);
            if (help == 0)
            {
                mushroom = true;
                Point x = Location;
                x.Offset(0, -Settings.height);
                Location = x;
                Size = new Size(Settings.width, Settings.height * 2);
                Parent.Controls.Remove(control);
                gameControls.Remove(control);
            }
            else if (mushroom)
            {
                Parent.Controls.Remove(control);
                gameControls.Remove(control);
                fireBall = doubleJump = bumerang = invincible = false;
                itemCounter = 0;
                Parent.Controls.Remove(itemThrow);
                switch (help)
                {
                    case 1:
                        fireBall = true;
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
                        itemThrow.Image = Properties.Resources.stone;
                        bumerang = true;
                        break;
                }
            }
            ChangeTexture();
        }

        //-----------------------------------------Texture/Bounds--------------------------------------------------------
        private Animation normal_stay_left, normal_stay_right, pepper_stay_left, pepper_stay_right, pepper_walk_left, pepper_walk_right, player_small_walk_right, player_small_walk_left, player_small_stay_left, player_small_stay_right;
        private bool last_button_right;
        private void InitTexture()
        {
            normal_stay_left = new Animation();
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_0);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_0);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_0);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_1);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_1);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_1);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_2);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_2);
            normal_stay_left.Add(Properties.Resources.player_normal_stay_left_2);
            normal_stay_right = new Animation();
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_0);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_0);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_0);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_1);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_1);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_1);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_2);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_2);
            normal_stay_right.Add(Properties.Resources.player_normal_stay_right_2);
            pepper_stay_left = new Animation();
            pepper_stay_left.Add(Properties.Resources.player_normal_pepper_left_0);
            pepper_stay_left.Add(Properties.Resources.player_normal_pepper_left_1);
            pepper_stay_left.Add(Properties.Resources.player_normal_pepper_left_2);
            pepper_stay_left.Add(Properties.Resources.player_normal_pepper_left_3);
            pepper_stay_left.Add(Properties.Resources.player_normal_pepper_left_4);
            pepper_stay_left.Add(Properties.Resources.player_normal_pepper_left_5);
            pepper_stay_right = new Animation();
            pepper_stay_right.Add(Properties.Resources.player_normal_pepper_0);
            pepper_stay_right.Add(Properties.Resources.player_normal_pepper_1);
            pepper_stay_right.Add(Properties.Resources.player_normal_pepper_2);
            pepper_stay_right.Add(Properties.Resources.player_normal_pepper_3);
            pepper_stay_right.Add(Properties.Resources.player_normal_pepper_4);
            pepper_stay_right.Add(Properties.Resources.player_normal_pepper_5);
            pepper_walk_left = new Animation();
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_0);
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_1);
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_2);
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_3);
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_4);
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_5);
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_6);
            pepper_walk_left.Add(Properties.Resources.player_normal_run_pepper_left_7);
            pepper_walk_right = new Animation();
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_0);
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_1);
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_2);
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_3);
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_4);
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_5);
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_6);
            pepper_walk_right.Add(Properties.Resources.player_run_pepper_right_7);
            player_small_walk_right = new Animation();
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_00);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_01);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_02);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_03);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_04);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_05);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_06);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_07);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_08);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_09);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_10);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_11);
            player_small_walk_right.Add(Properties.Resources.player_small_walking_right_12);
            player_small_walk_left = new Animation();
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_00);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_01);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_02);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_03);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_04);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_05);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_06);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_07);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_08);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_09);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_10);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_11);
            player_small_walk_left.Add(Properties.Resources.player_small_walking_left_12);
            player_small_stay_left = new Animation();
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_0);
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_0);
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_0);
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_0);
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_1);
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_1);
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_1);
            player_small_stay_left.Add(Properties.Resources.player_small_stay_left_1);
            player_small_stay_right = new Animation();
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_0);
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_0);
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_0);
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_0);
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_1);
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_1);
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_1);
            player_small_stay_right.Add(Properties.Resources.player_small_stay_right_1);
        }
        private void ChangeTexture()
        {
            if (!mushroom)
            {
                if (left)
                {
                    pcBPlayer.Image = player_small_walk_left.Get();
                }
                else if (right)
                {
                    pcBPlayer.Image = player_small_walk_right.Get();
                }
                else
                {
                    if (last_button_right)
                    {
                        pcBPlayer.Image = player_small_stay_right.Get();
                    }
                    else
                    {
                        pcBPlayer.Image = player_small_stay_left.Get();
                    }
                }
            }
            else
            {
                if (fireBall)
                {
                    if (left)
                    {
                        pcBPlayer.Image = pepper_walk_left.Get();
                    }
                    else if (right)
                    {
                        pcBPlayer.Image = pepper_walk_right.Get();
                    }
                    else
                    {
                        if (last_button_right)
                        {
                            pcBPlayer.Image = pepper_stay_right.Get();
                        }
                        else
                        {
                            pcBPlayer.Image = pepper_stay_left.Get();
                        }
                    }
                }
                else
                {
                    if (left)
                    {

                    }
                    else if (right)
                    {

                    }
                    else
                    {
                        if (last_button_right)
                        {
                            pcBPlayer.Image = normal_stay_left.Get();
                        }
                        else
                        {
                            pcBPlayer.Image = normal_stay_right.Get();
                        }
                    }
                }

            }
            if ((last_button_right || right) && !left)
            {
                last_button_right = true;
            }
            else
            {
                last_button_right = false;
            }
            return;
            throw new NotImplementedException();//TODO Set Animation
        }
        //-----------------------------------------Pause-----------------------------------------------------------------------
        private void Pause()
        {
            (Parent as Play).GetEngine().StopTime();
            pause = new pause(Parent as Play, star);
            Stop();
            up = jump = false;
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
        public void StartEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.Start(this);
                }
            }
        }
        private void StopEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.Stop();
                }
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
            if (control is Enemy)
            {
                (control as Enemy).Stop();
            }
            gameControls.Remove(control);
        }
        public void GameControlRemoveAt(int index)
        {
            if (gameControls[index] is Enemy)
            {
                (gameControls[index] as Enemy).Stop();
            }
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
            int counter = 0;
            foreach (Enemy enemy_ in enemies)
            {
                if (enemy_.IsActive)
                {
                    if (++counter == 5)
                    {
                        return;
                    }
                }
            }
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
