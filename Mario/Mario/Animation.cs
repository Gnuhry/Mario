﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{

    class Animation
    {
        private List<Image> pictures;
        private int pointer;
        public Animation()
        {
            pictures = new List<Image>();
            pointer = 0;
        }
        public void Add(Image picture)
        {
            pictures.Add(picture);
        }
        public Image Get()
        {
            if (++pointer >= pictures.Count)
            {
                pointer = 0;
            }
            return pictures[pointer];
        }
    }
}