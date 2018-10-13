using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{
    public class Items
    {
        private bool Mushroom_;
        private int Item, JumpCounter;

        public int getRandomItem()
        {
            Random rnd = new Random();
            return rnd.Next(1, 4);
        }

        public void useItem()
        {
            switch (Item)
            {
                case 0: return;//noItem
                case 1: return;//DoubleJump
                case 2: return;//FireFlower
                case 3: return;//Invincible
            }
        }
        public bool FireFlower
        {
            get => Item == 2;
        }
        public bool Invincible
        {
            get => Item == 3;
        }
        public bool DoubleJump
        {
            get => Item == 1 && (JumpCounter++) % 2 == 0;
            set => JumpCounter = 0;
        }

        public bool Mushroom
        {
            get => Mushroom_;
            set => Mushroom_ = value;
        }



        public Items()
        {
            JumpCounter = 0;
            Item = 1;
        }
    }
}
