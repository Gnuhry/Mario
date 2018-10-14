﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{
    public class Settings
    {
        public static int
            speedX = 10,
            speedY = 10,
            jumpspeed = 20,
            width = 100,
            hight = 100,
            timerlenght = 50,
            gamehight = 20;
        public static string imgStone = "img\\stone.jpg", imgDirt = "img\\dirt.jpg", levelPath = "Level\\", imgFireballThrow = "img//firball_throw.jpg",
            imgMushroom = "img\\mushroom.jpg", imgDoublejump = "img\\doublejump.jpg", imgFireflower = "img\\fireflower.jpg", imgInvincible = "img\\invincible.jpg";
        private char up_, left_, right_, item_;
        private bool music_, sounds_;
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

        public Settings()
        {
            Default();
        }
        public void Default()
        {
            up_ = 'W';
            left_ = 'A';
            right_ = 'D';
            item_ = 'Q';
            music_ = sounds_ = true;
        }
    }
}
