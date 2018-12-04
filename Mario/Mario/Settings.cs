﻿using System;
using System.Drawing;
using System.IO;

namespace Mario
{
    public class Settings
    {
        public static int
            speedX = 10,
            speedY = 10,
            jumpspeed = 15,
            width = 50,
            height = 50,
            timerlenght = 50,
            gamehight = 20,
            itemThrowBlockLength = 3,
            invincibleCounter = 100,
            maxEnemy = 5,
            loadingScreenLength = 3000,
            highBlocks=16;
        public static Size size = new Size(width, height);
        public static string path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 9), textFilePath = path + "Level\\";
        public static double borderFactor = 0.3;
        public static char upD = 'W', leftD = 'A', rightD = 'D', itemD = 'Q';
        private char up_, left_, right_, item_;
        private bool music_, sounds_;
        private double volume_;
        public char up
        {
            
            get => up_;
            set => up_ = value;
        }
        public bool music
        {
            get => music_;
            set => music_ = value;
        }
        public bool sounds
        {
            get => sounds_;
            set => sounds_ = value;
        }
        public char left
        {
            get => left_;
            set => left_ = value;
        }
        public char right
        {
            get => right_;
            set => right_ = value;
        }
        public char item
        {
            get => item_;
            set => item_ = value;
        }
        public double volume
        {
            get => volume_;
            set
            {
                if (value < 0 || value > 1)
                {
                    return;
                }
                volume_ = value;
            }
        }
        public Settings()
        {
            Default();
        }
        public void Default()
        {
            up_ = upD;
            left_ = leftD;
            right_ = rightD;
            item_ = itemD;
            music_ = sounds_ = true;
            volume = 0.5;
        }
    }
}
