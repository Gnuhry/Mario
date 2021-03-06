﻿using System;
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
        private ReadFile readFile;
        public Players(Settings settings, ReadFile readFile)
        {
            InitializeComponent();
            Init(settings, readFile);
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
        private void Init(Settings settings, ReadFile readFile)
        {
            this.settings = settings;
            this.readFile = readFile;
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
            if (key.Equals(settings.Left))
            {
                left = false;
            }
            else if (key.Equals(settings.Right))
            {
                right = false;
            }
            else if (key.Equals(settings.Up))
            {
                up = false;
            }
            else if (key.Equals(settings.Item))
            {
                previtem = false;
            }
        }

        private void Players_KeyDown(object sender, KeyEventArgs e)
        {
            char key = Convert.ToChar(e.KeyValue);
            if (key.Equals(settings.Left))
            {
                left = true;
                lastpressRight = false;
            }
            else if (key.Equals(settings.Right))
            {
                right = true;
                lastpressRight = true;
            }
            else if (key.Equals(settings.Up))
            {
                up = true;
            }
            else if (key.Equals(settings.Item))
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
        private bool jump, fulldown;
        private int jumpCounter;
        private void InitPlayerTimer()
        {
            fulldown = true;
            player_timer = new Timer
            {
                Interval = 25//40Hz
            };
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
            if (deadanimationcounter > -1)
            {
                if (--deadanimationcounter < 0)
                {
                    (Parent as Play).Restart();
                }
            }
            else
            {
                Focus();
                BringToFront();
                ChangeTexture();
                if (counter++ != 1)
                {
                    return;
                }
                counter = 0;
                Point offset = new Point
                {
                    Y = CheckY()
                };
                offset.X = CheckX(offset.Y);
                CollisionDetect(Bounds, false, false, true, false, false, false);
                ItemTimer();
                EnemyCheckTimer();
                Moving(offset);
                prevup = up;
            }
        }

        private void EnemyCheckTimer()
        {
            try
            {
                for (int f = 0; f < enemies.Count; f++)
                {
                    if (enemies[f].IsActive)
                    {
                        enemies[f].Visible = true;
                        if (enemies[f].Location.X < 0 || enemies[f].Location.X > Parent.Size.Width)
                        {
                            enemies[f].Stop();
                        }
                        if (enemies[f].Location.Y > Parent.Size.Height)
                        {
                            enemies[f].Stop();
                            GameControlRemove(enemies[f]);
                            enemies.RemoveAt(f);
                        }
                    }
                    else
                    {
                        if (Parent != null)
                        {
                            if (enemies[f].Location.X > 0 && enemies[f].Location.X < Parent.Size.Width)
                            {
                                enemies[f].Start(this);
                                enemies[f].Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
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
                if (CollisionDetect(help, false, false, true, false, false, false))
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
                if (CollisionDetect(help, false, false, true, false, false, false))
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
                if (CollisionDetect(help, true, false, true, false, false, false))
                {
                    if (!prevup)
                    {
                        Sound_music.DoubleJumpSound(settings);
                    }
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
                if (CollisionDetect(help, false, true, true, false, false, false))
                {
                    if (CollisionDetect(help, false, false, false, false, false, true))
                    {
                        fulldown = !fulldown;
                        return Settings.speedY / 2;
                    }
                    else if (!fulldown)
                    {
                        fulldown = true;
                        return Settings.speedY / 2;
                    }
                    else
                    {
                        return Settings.speedY;
                    }
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
        public bool CollisionDetect(Rectangle location, bool up, bool down, bool player, bool destroyEnemyItem, bool pickCoinItem, bool cloud)
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
                        if (cloud)
                        {
                            if (control.Tag.ToString().Equals("cloud"))
                            {
                                if (player)
                                {
                                    Sound_music.CloudSound(settings);
                                }
                                return true;
                            }
                        }
                        else
                        {

                            if (control.Tag.ToString().Split('_').Length > 1 && ((player && up) || pickCoinItem))
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
                                    coinVisibleCounter = 3;
                                    Sound_music.RiceSound(settings);
                                    return false;
                                }
                                else if (control.Tag.ToString().Split('_')[1].Equals("destroy"))
                                {
                                    Sound_music.DestroyBlockSound(settings);
                                    gameControls.Remove(control);
                                    Parent.Controls.Remove(control);
                                    return false;
                                }

                            }
                            else if (control.Tag.ToString().Split('_')[0].Equals("obstacle"))
                            {
                                if (player && up)
                                {
                                    Sound_music.HitBlockSound(settings);
                                }
                                return false;
                            }
                            else if (control.Tag.Equals("coin") && (player || pickCoinItem))
                            {
                                Sound_music.RiceSound(settings);
                                gameControls.Remove(control);
                                Parent.Controls.Remove(control);
                                (Parent as Play).SetCoin(++coinCounter);
                                coinVisibleCounter = 3;
                                return true;
                            }
                            else if (control.Tag.ToString().Split('_')[0].Equals("star") && (player || pickCoinItem))
                            {
                                Sound_music.RiceCoinSound(settings);
                                gameControls.Remove(control);
                                Parent.Controls.Remove(control);
                                star[Convert.ToInt32(control.Tag.ToString().Split('_')[1]) - 1] = true;
                                (Parent as Play).GetEngine().ClearCoin(Convert.ToInt32(control.Tag.ToString().Split('_')[1]));
                                return true;
                            }
                            else if (control.Tag.Equals("end") && player)
                            {
                                (Parent as Play).CheckHighScoore();
                                score sc = new score((Parent as Play).GetPlayTime(), (Parent as Play).GetHigscoore(), coinCounter, star, (Parent as Play).GetWorlds(), (Parent as Play).GetRiceCoin());
                                (Parent as Play).SetRiceCoin(star);
                                sc.Show();
                                (Parent as Play).Close();
                                return true;
                            }
                            else if (control is Itembox)
                            {
                                if (up||pickCoinItem)
                                {
                                    Control c = (control as Itembox).Activate(!mushroom);
                                    if (c != null)
                                    {
                                        gameControls.Add(c);
                                        Sound_music.ItemBoxSound(settings);
                                    }
                                    else
                                    {
                                        Sound_music.HitBlockSound(settings);
                                    }
                                }
                                return false;
                            }
                            else if (control.Tag.ToString().Split('_')[0].Equals("Item") && (player||pickCoinItem))
                            {
                                PickItem(control);
                            }
                            else if (control.Tag.ToString().Equals("water"))
                            {
                                if (invincible && itemCounter > 0)
                                {
                                    Sound_music.WaterSound(settings);
                                    return true;
                                }
                                else if (player)
                                {
                                    Sound_music.WaterSound(settings);
                                    jumpCounter = 0;
                                    Hit();
                                }
                            }
                            else if (control is Enemy)
                            {
                                if ((invincible && itemCounter > 0) || destroyEnemyItem)
                                {
                                    (control as Enemy).Hit(settings);
                                }
                                else if ((down && location.Bottom - control.Top < 20))
                                {
                                    if (control.Tag.ToString().Split('_').Length > 1)
                                    {
                                        Hit();
                                    }
                                    else
                                    {
                                        Rectangle rectangle = Bounds;
                                        rectangle.Offset(0, -50);
                                        if (CollisionDetect(rectangle, true, false, true, false, true, false))
                                        {
                                            Point x = Location;
                                            x.Offset(0, -50);
                                            Location = x;
                                        }
                                        (control as Enemy).Hit(settings);
                                        return false;
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

            }

            if (Parent == null)
            {
                return false;
            }
            if (location.Y + location.Height > Parent.Height)
            {
                Dead();
            }
            if (cloud)
            {
                return false;
            }
            return true;
        }



        //-------------------------------------------Item--------------------------------------------------------------
        private bool fireBall, doubleJump, mushroom, bumerang, invincible, lastRight, doubleJumping, prevup, previtem, backbumerang, lastpressRight;
        private int itemCounter, hitCounter, coinCounter, coinVisibleCounter, deadanimationcounter;
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
            deadanimationcounter = -1;
            string[] help = readFile.GetData().Split('|')[3].Split(',');
            star = new bool[] { help[0] == "1", help[1] == "1", help[2] == "1" };
            fireBall = doubleJump = bumerang = invincible = backbumerang = false;
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
           /* if (invincible)
            {
                Sound_music.StarSound(settings);
            }*/
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
                    if (bumerang)
                    {
                        if(!CollisionDetect(new Rectangle(help, Settings.size), false, false, false, false, true, false))
                        {
                            itemCounter = 1;
                            backbumerang = true;
                        }
                    }
                    if (CollisionDetect(new Rectangle(help, Settings.size), false, false, false, false, false, false))
                    {
                       
                        if (fireBall)
                        {
                            Sound_music.FireballSound(settings);
                            help.Offset(0, Settings.height);
                            if (!CollisionDetect(new Rectangle(help, Settings.size), false, false, false, true, false, false))
                            {
                                help.Offset(0, -Settings.height);
                                CollisionDetect(new Rectangle(help, Settings.size), false, false, false, true, false, false);
                            }
                        }
                        itemThrow.Location = help;
                    }
                    else
                    {
                        itemCounter = 1;
                        backbumerang = true;
                    }
                }
                
                if (--itemCounter == 0)
                {
                    if (fireBall || backbumerang)
                    {
                        Parent.Controls.Remove(itemThrow);
                    }
                    if (bumerang && !backbumerang)
                    {
                        backbumerang = true;
                        lastRight = !lastRight;
                        itemCounter = Settings.itemThrowBlockLength;
                        Point help = itemThrow.Location;
                        if (lastRight)
                        {
                            help.Offset(Settings.width, 0);
                        }
                        else
                        {
                            help.Offset(-Settings.width, 0);
                        }
                        itemThrow.Location = help;
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
                backbumerang = false;
                lastRight = lastpressRight;
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
                if (bumerang)
                {
                    Sound_music.BumerangSound(settings);
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
                Sound_music.HitSound(settings);
                return;
            }
            if (mushroom)
            {
                Size = Settings.size;
                mushroom = false;
                hitCounter = 30;
                Sound_music.HitSound(settings);
                return;
            }
            Dead();
        }
        private void Dead()
        {
            Sound_music.DieSound(settings);
            Point x = Location;
            PictureBox pcB = new PictureBox()
            {
                Image = Properties.Resources.player_dead,
                Location = x
            };
            (Parent as Play).Controls.Add(pcB);
            pcB.BringToFront();
            deadanimationcounter = 50;
            Visible = false;
            StopEnemies();
        }
        private void PickItem(Control control)
        {
            int help = Convert.ToInt32(control.Tag.ToString().Split('_')[1]);
            if (help == 0)
            {
                Sound_music.GrowSound(settings);
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
                Sound_music.PickItemSound(settings);
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
                        Sound_music.StarSound(settings);
                        break;
                    case 3:
                        doubleJump = true;
                        break;
                    case 4:
                        itemThrow.Image = Properties.Resources.bumerang_throw;
                        bumerang = true;
                        break;
                }
            }
            ChangeTexture();
        }

        //-----------------------------------------Texture/Bounds--------------------------------------------------------
        private Animation normal_stay_left, normal_stay_right, pepper_stay_left, pepper_stay_right, pepper_walk_left, pepper_walk_right,
            player_small_walk_right, player_small_walk_left, player_small_stay_left, player_small_stay_right, player_normal_walk_left, player_normal_walk_right,
            stay_bumerang_left, stay_bumerang_right, run_bumerang_left, run_bumerang_right, stay_star_left, stay_star_right, run_star_left, run_star_right,
        stay_jump_left, stay_jump_right, run_jump_left, run_jump_right;
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
            player_normal_walk_left = new Animation();
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_0);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_1);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_2);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_3);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_4);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_5);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_6);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_7);
            player_normal_walk_left.Add(Properties.Resources.player_normal_walk_left_8);
            player_normal_walk_right = new Animation();
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_0);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_1);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_2);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_3);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_4);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_5);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_6);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_7);
            player_normal_walk_right.Add(Properties.Resources.player_normal_walk_right_8);
            stay_bumerang_left = new Animation();
            stay_bumerang_left.Add(Properties.Resources.player_normal_bumerang_left_0);
            stay_bumerang_left.Add(Properties.Resources.player_normal_bumerang_left_0);
            stay_bumerang_left.Add(Properties.Resources.player_normal_bumerang_left_1);
            stay_bumerang_left.Add(Properties.Resources.player_normal_bumerang_left_1);
            stay_bumerang_left.Add(Properties.Resources.player_normal_bumerang_left_2);
            stay_bumerang_left.Add(Properties.Resources.player_normal_bumerang_left_2);
            stay_bumerang_right = new Animation();
            stay_bumerang_right.Add(Properties.Resources.player_normal_bumerang_right_0);
            stay_bumerang_right.Add(Properties.Resources.player_normal_bumerang_right_0);
            stay_bumerang_right.Add(Properties.Resources.player_normal_bumerang_right_1);
            stay_bumerang_right.Add(Properties.Resources.player_normal_bumerang_right_1);
            stay_bumerang_right.Add(Properties.Resources.player_normal_bumerang_right_2);
            stay_bumerang_right.Add(Properties.Resources.player_normal_bumerang_right_2);
            run_bumerang_left = new Animation();
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_0);
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_1);
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_2);
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_3);
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_4);
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_5);
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_6);
            run_bumerang_left.Add(Properties.Resources.player_normal_run_bumerang_left_7);
            run_bumerang_right = new Animation();
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_0);
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_1);
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_2);
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_3);
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_4);
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_5);
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_6);
            run_bumerang_right.Add(Properties.Resources.player_normal_run_bumerang_right_7);
            stay_star_left = new Animation();
            stay_star_left.Add(Properties.Resources.player_normal_stay_star_left_0);
            stay_star_left.Add(Properties.Resources.player_normal_stay_star_left_0);
            stay_star_left.Add(Properties.Resources.player_normal_stay_star_left_1);
            stay_star_left.Add(Properties.Resources.player_normal_stay_star_left_1);
            stay_star_left.Add(Properties.Resources.player_normal_stay_star_left_2);
            stay_star_left.Add(Properties.Resources.player_normal_stay_star_left_2);
            stay_star_right = new Animation();
            stay_star_right.Add(Properties.Resources.player_normal_stay_star_right_0);
            stay_star_right.Add(Properties.Resources.player_normal_stay_star_right_0);
            stay_star_right.Add(Properties.Resources.player_normal_stay_star_right_1);
            stay_star_right.Add(Properties.Resources.player_normal_stay_star_right_1);
            stay_star_right.Add(Properties.Resources.player_normal_stay_star_right_2);
            stay_star_right.Add(Properties.Resources.player_normal_stay_star_right_2);
            run_star_left = new Animation();
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_0);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_1);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_2);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_3);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_4);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_5);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_6);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_7);
            run_star_left.Add(Properties.Resources.player_normal_run_star_left_8);
            run_star_right = new Animation();
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_0);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_1);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_2);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_3);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_4);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_5);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_6);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_7);
            run_star_right.Add(Properties.Resources.player_normal_run_star_right_8);
            stay_jump_left = new Animation();
            stay_jump_left.Add(Properties.Resources.player_normal_stay_doublejump_left_0);
            stay_jump_left.Add(Properties.Resources.player_normal_stay_doublejump_left_0);
            stay_jump_left.Add(Properties.Resources.player_normal_stay_doublejump_left_1);
            stay_jump_left.Add(Properties.Resources.player_normal_stay_doublejump_left_1);
            stay_jump_left.Add(Properties.Resources.player_normal_stay_doublejump_left_2);
            stay_jump_left.Add(Properties.Resources.player_normal_stay_doublejump_left_2);
            stay_jump_right = new Animation();
            stay_jump_right.Add(Properties.Resources.player_normal_stay_doublejump_right_0);
            stay_jump_right.Add(Properties.Resources.player_normal_stay_doublejump_right_0);
            stay_jump_right.Add(Properties.Resources.player_normal_stay_doublejump_right_1);
            stay_jump_right.Add(Properties.Resources.player_normal_stay_doublejump_right_1);
            stay_jump_right.Add(Properties.Resources.player_normal_stay_doublejump_right_2);
            stay_jump_right.Add(Properties.Resources.player_normal_stay_doublejump_right_2);
            run_jump_left = new Animation();
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_0);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_1);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_2);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_3);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_4);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_5);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_6);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_7);
            run_jump_left.Add(Properties.Resources.player_normal_run_doublejump_left_8);
            run_jump_right = new Animation();
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_0);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_1);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_2);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_3);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_4);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_5);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_6);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_7);
            run_jump_right.Add(Properties.Resources.player_normal_run_doublejump_right_8);
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
                else if (bumerang)
                {
                    if (left)
                    {
                        pcBPlayer.Image = run_bumerang_left.Get();
                    }
                    else if (right)
                    {
                        pcBPlayer.Image = run_bumerang_right.Get();
                    }
                    else
                    {
                        if (last_button_right)
                        {
                            pcBPlayer.Image = stay_bumerang_right.Get();
                        }
                        else
                        {
                            pcBPlayer.Image = stay_bumerang_left.Get();
                        }
                    }
                }
                else if (invincible)
                {
                    if (left)
                    {
                        pcBPlayer.Image = run_star_left.Get();
                    }
                    else if (right)
                    {
                        pcBPlayer.Image = run_star_right.Get();
                    }
                    else
                    {
                        if (last_button_right)
                        {
                            pcBPlayer.Image = stay_star_right.Get();
                        }
                        else
                        {
                            pcBPlayer.Image = stay_star_left.Get();
                        }
                    }
                }
                else if (doubleJump)
                {
                    if (left)
                    {
                        pcBPlayer.Image = run_jump_left.Get();
                    }
                    else if (right)
                    {
                        pcBPlayer.Image = run_jump_right.Get();
                    }
                    else
                    {
                        if (last_button_right)
                        {
                            pcBPlayer.Image = stay_jump_right.Get();
                        }
                        else
                        {
                            pcBPlayer.Image = stay_jump_left.Get();
                        }
                    }
                }
                else
                {
                    if (left)
                    {
                        pcBPlayer.Image = player_normal_walk_left.Get();
                    }
                    else if (right)
                    {
                        pcBPlayer.Image = player_normal_walk_right.Get();
                    }
                    else
                    {
                        if (last_button_right)
                        {
                            pcBPlayer.Image = normal_stay_right.Get();
                        }
                        else
                        {
                            pcBPlayer.Image = normal_stay_left.Get();
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
        public bool IsGameControl(Control control)
        {
            try
            {
                return gameControls.BinarySearch(control) > -1;
            }
            catch (Exception)
            {
                return false;
            }
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
                        enemy.Visible = false;
                        return;
                    }
                }
            }
            enemy.Start(this);
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
