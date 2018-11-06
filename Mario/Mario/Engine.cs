﻿using System;
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
        private Players players;
        private Control.ControlCollection controlCollection;
        private System.Windows.Forms.Timer timer;

        public Engine(Control[][] controls, Control.ControlCollection controlCollection)
        {
            this.controls = controls;
            this.controlCollection = controlCollection;
            Init();
            FindPlayer();
            DisplayBackground();
            InitTime();
            InitTimer();

        }
        public void Start()
        {
            players.Start();
            StartTime();
        }
        private void Init()
        {
            gameWidth = Screen.PrimaryScreen.WorkingArea.Width / Settings.width;
            border = gameWidth * Settings.width * Settings.borderFactor;
        }


        //-------------------------Timer-----------------------------------------------
        private void InitTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            MoveBackgroundLeft();
            MoveBackgroundRight();
            TimerTime();
        }

        //------------------------Player-----------------------------------------
        private void FindPlayer()
        {
            for (int f = 0; f < controls.Length; f++)
            {
                for (int g = 0; g < controls[0].Length; g++)
                {
                    if (controls[f][g] is Players)
                    {
                        players = controls[f][g] as Players;
                        if (f > gameWidth / 2)
                        {
                            pointer = f - (gameWidth / 2);
                        }
                        else
                        {
                            pointer = 0;
                        }
                        return;
                    }
                }

            }
            if (players == null)
            {
                throw new Exception();
            }
        }
        public void PlayerStart()
        {
            players.Start();
        }

        //-------------------------Background-------------------------------------
        private void DisplayBackground()
        {
            for (int row = pointer; row < pointer + gameWidth && row < pointer + controls.Length; row++)
            {
                for (int column = 0; column < Settings.gamehight && column < controls[0].Length; column++)
                {
                    if (controls[row][column] != null)
                    {
                        players.GameControlAdd(controls[row][column]);
                        controls[row][column].Location = new Point((row - pointer) * Settings.width, column * Settings.height);
                        controlCollection.Add(controls[row][column]);
                        if (controls[row][column] == players)
                        {
                            players.GameControlRemove(controls[row][column]);
                            // controls[row][column].Location = new Point((row - pointer) * Settings.width, column * Settings.height - Settings.height);
                            controls[row][column] = null;
                        }
                        if (controls[row][column] is Enemy)
                        {
                            if (controls[row][column].Tag.ToString().Split('_').Length > 1)
                            {
                                controls[row][column].Location = new Point((row - pointer) * Settings.width, column * Settings.height - Settings.height);
                            }
                            players.GameControlRemove(controls[row][column]);
                            players.EnemyAdd(controls[row][column] as Enemy);
                            controls[row][column] = null;
                        }
                    }
                }
            }

        }
        private void MoveBackgroundLeft()
        {
            if (pointer < 1) return;
            Point help = players.Location;
            if (help.X < border)
            {
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer + gameWidth - 1][f] != null)
                    {
                        int index = players.GetGameControlIndexOf(controls[pointer + gameWidth - 1][f]);
                        if (index != -1)
                        {
                            controlCollection.Remove(players.GetGameControlItem(index));
                            players.GameControlRemoveAt(index);
                        }
                    }
                }
                foreach (Control control in players.GetGameControl())
                {
                    if (control != null)
                    {
                        Point location = control.Location;
                        location.Offset(Settings.width, 0);
                        control.Location = location;
                    }
                }
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer - 1][f] != null)
                    {
                        controls[pointer - 1][f].Location = new Point(0, f * Settings.height);
                        players.GameControlAdd(controls[pointer - 1][f]);
                        controlCollection.Add(controls[pointer - 1][f]);
                        if (controls[pointer - 1][f] is Enemy)
                        {
                            players.GameControlRemove(controls[pointer - 1][f]);
                            players.EnemyAdd(controls[pointer - 1][f] as Enemy);
                            players.StartEnemies();
                            controls[pointer - 1][f] = null;
                        }
                    }
                }
                for (int f = 0; f < players.GetEnemy().Count; f++)
                {
                    if (players.GetEnemy()[f].Location.Y == Settings.width * (pointer - 1))
                    {
                        controlCollection.Add(players.GetEnemy()[f]);
                        players.GetEnemy()[f].Start(players);
                    }
                }
                pointer--;
                Point temp = players.Location;
                temp.Offset(Settings.width, 0);
                players.Location = temp;
                foreach (Enemy enemy in players.GetEnemy())
                {
                    if (enemy != null)
                    {
                        Point help2 = enemy.Location;
                        help2.Offset(Settings.width, 0);
                        enemy.Location = help2;
                        if (help.X > Screen.PrimaryScreen.WorkingArea.Width)
                        {
                            players.EnemyRemove(enemy);
                        }
                    }
                }
            }
        }
        private void MoveBackgroundRight()
        {
            if (pointer >= controls.Length - gameWidth) return;
            Point help = players.Location;
            if (help.X > gameWidth * Settings.width - border)
            {
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer][f] != null)
                    {
                        int index = players.GetGameControlIndexOf(controls[pointer][f]);
                        if (index != -1)
                        {
                            controlCollection.Remove(players.GetGameControlItem(index));
                            players.GameControlRemoveAt(index);
                        }
                    }
                }
                foreach (Control control in players.GetGameControl())
                {
                    if (control != null)
                    {
                        Point location = control.Location;
                        location.Offset(-Settings.width, 0);
                        control.Location = location;
                    }
                }
                for (int f = 0; f < 15; f++)
                {
                    if (controls[pointer + gameWidth][f] != null)
                    {
                        controls[pointer + gameWidth][f].Location = new Point((gameWidth - 1) * Settings.width, f * Settings.height);
                        players.GameControlAdd(controls[pointer + gameWidth][f]);
                        controlCollection.Add(controls[pointer + gameWidth][f]);
                        if (controls[pointer + gameWidth][f] is Enemy)
                        {
                            players.GameControlRemove(controls[pointer + gameWidth][f]);
                            players.EnemyAdd(controls[pointer + gameWidth][f] as Enemy);
                            players.StartEnemies();
                            controls[pointer + gameWidth][f] = null;
                        }

                    }
                }
                pointer++;
                Point temp = players.Location;
                temp.Offset(-Settings.width, 0);
                players.Location = temp;
                foreach (Enemy enemy in players.GetEnemy())
                {
                    if (enemy != null)
                    {
                        Point help2 = enemy.Location;
                        help2.Offset(-Settings.width, 0);
                        enemy.Location = help2;
                        if (help.X < 0)
                        {
                            players.EnemyRemove(enemy);
                        }
                    }
                }
            }
        }

        //-----------------------------------------Time------------------------------------------
        private DateTime start, help;
        private Label time;
        private bool active;
        private void InitTime()
        {
            time = new Label
            {
                AutoSize = true,
                Location = new Point(0, 0),
                BackColor = Color.White
            };
            controlCollection.Add(time);
            time.BringToFront();
            active = false;
            help = new DateTime();
        }
        private void TimerTime()
        {
            if (active)
            {
                time.Text = ((DateTime.Now - start).TotalSeconds + (help - new DateTime()).TotalSeconds) + "sek";
            }
        }
        public void StartTime()
        {
            active = true;
            start = DateTime.Now;

        }
        public void StopTime()
        {
            active = false;
            help += DateTime.Now - start;
        }
        public double GetTime()
        {
            return (help - new DateTime()).TotalSeconds;
        }
    }
}

