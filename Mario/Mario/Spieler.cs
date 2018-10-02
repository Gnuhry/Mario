using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mario
{
    class Spieler
    {
        private static Point Location;
        private static int bewegungX = 1, bewegungy=-1, jmpCounter;
        private Control control;
        private static Control control2;
        private static bool Jumping,Rechts,Links;
        Timer timer = new Timer();
        private Engine engine;

        public Spieler(Control control,Engine engine)
        {
            this.engine = engine;
            Links = Jumping = Rechts = false;
            timer.Tick += Timer;
            timer.Interval = 50;
            this.control = control;
            control2 = control;
            Location = control.Location;
            timer.Start();
        }

        public bool Bewegung(bool rechts,bool links)
        {
            Rechts = rechts;
            Links = links;
           /* if (rechts)
            {
                Location.Offset(bewegungX, 0);
                if (!Kollisionserkennung(-2, Location))
                {
                    Location.Offset(-bewegungX, 0);
                    return false;
                }
            }
            else
            {
                Location.Offset(-bewegungX, 0);
                if (!Kollisionserkennung(2, Location))
                {
                    Location.Offset(bewegungX, 0);
                    return false;
                }
            }
            control.Location = Location;*/
            return true;
        }

        public void Jump(bool start)
        {

            if (start)
                Jumping = true;
            else
            {
                Jumping = false;
                jmpCounter = 0;
            }

        }

        private void Timer(object sender, EventArgs e)
        {
            if (Jumping)
            {
                jmpCounter -= 50;
                if (jmpCounter <= 0) { Jumping = false; }
                else if (Rechts) {
                    if (Kollisionserkennung(1, Location))
                    {
                        if (Kollisionserkennung(-2, Location))
                            Location.Offset(bewegungX, bewegungy);
                        else
                            Location.Offset(0, bewegungy);
                    }
                    else if(Kollisionserkennung(-2, Location))
                        Location.Offset(bewegungX, 0);
                }
                else if (Links)
                {
                    if (Kollisionserkennung(1, Location))
                    {
                        if (Kollisionserkennung(2, Location))
                            Location.Offset(-bewegungX, bewegungy);
                        else
                            Location.Offset(0, bewegungy);
                    }
                    else if (Kollisionserkennung(2, Location))
                        Location.Offset(-bewegungX, 0);
                }
                else if (Kollisionserkennung(1, Location))
                            Location.Offset(0, bewegungy);
                //Jump
               /* Location.Offset(0, bewegungy);
                if (Kollisionserkennung(1, Location))
                {
                    control2.Location = Location;
                }
                else
                {
                    Location.Offset(0, -bewegungy);
                }*/
            }
            else
            {
                if (Rechts)
                {
                    if (Kollisionserkennung(-1, Location))
                    {
                        if (Kollisionserkennung(-2, Location))
                            Location.Offset(bewegungX, -bewegungy);
                        else
                            Location.Offset(0, -bewegungy);
                    }
                    else if (Kollisionserkennung(-2, Location))
                    {
                        Location.Offset(bewegungX, 0);
                        jmpCounter = 4000;//4s
                    }
                }
                else if (Links)
                {
                    if (Kollisionserkennung(-1, Location))
                    {
                        if (Kollisionserkennung(2, Location))
                            Location.Offset(-bewegungX, -bewegungy);
                        else
                            Location.Offset(0, -bewegungy);
                    }
                    else if (Kollisionserkennung(2, Location))
                    {
                        Location.Offset(-bewegungX, 0);
                        jmpCounter = 4000;//4s
                    }
                }
                else if (Kollisionserkennung(-1, Location))
                    Location.Offset(0, -bewegungy);
                else
                    jmpCounter = 4000;//4s



                //Gravity
                /*Location.Offset(0, -bewegungy);
                if (Kollisionserkennung(-1, Location))
                {
                    control2.Location = Location;
                }
                else
                {
                    Location.Offset(0, bewegungy);

                }*/
            }
            control2.Location = Location;

        }
        private bool Kollisionserkennung(int richtung,Point vor)//oben, unten, links, rechts
        {
            Point x = vor;
            //TODO Erkennung ob Blöcke in Umgebung
            switch (richtung) {
                case 1:
                    x.Offset(0,bewegungy);
                    break;
                case -1:
                    if (vor.Y-bewegungy >400) return false;
                    x.Offset(0, -bewegungy);
                    break;
                case 2:
                    x.Offset(-bewegungX,0);
                    break;
                case -2:
                    x.Offset(bewegungX, 0);
                    break;
            }
            return engine.Kollisionsüberprufung(x, control.Size);
            return true;
                throw new Exception();
        }
   
    }
}
