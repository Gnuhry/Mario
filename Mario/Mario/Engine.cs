using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    class Engine
    {
       private Control[] controls;
        private Control[] hindernis;

        public Engine(Control[] controls)
        {
            this.controls = controls;

        }
        public bool Kollisionsüberprufung(Point Position,Size size)
        {
            for(int f = 0; f < hindernis.Length; f++)
            {
                if (Position.X < hindernis[f].Location.X + hindernis[f].Size.Height && //oben links, unten rechts
                    Position.Y < hindernis[f].Location.Y + hindernis[f].Size.Width &&
                    Position.X + size.Height > hindernis[f].Location.X &&
                    Position.Y + size.Width > hindernis[f].Location.Y) return false;
                if (Position.X < hindernis[f].Location.X + hindernis[f].Size.Height && //oben rechts, unten links
                    Position.Y + size.Width > hindernis[f].Location.Y &&
                    Position.X + size.Height > hindernis[f].Location.X &&
                    Position.Y < hindernis[f].Location.Y+hindernis[f].Size.Width) return false;
            }
            return true;
        }
        public void HintergrundAnzeigen(ref Control.ControlCollection controlCollection)
        {
            //TODO abfrage nach bildschirmgröße und spielpixel anzahl
            //anzeigen der möglichen
            //die anderen nicht hinzufügen
        }
        public void HintergrundBewegenLinks(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach rechts bewegen
            //ganz rechts die Controls löschen
            //links neue Controls hinzufügen
        }
        public void HintergrundBewegenRechts(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach links bewegen
            //ganz links die Controls löschen
            //rechts neue Controls hinzufügen
        }
        public void HintergrundBewegenOben(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach unten bewegen
            //ganz unten die Controls löschen
            //oben neue Controls hinzufügen
        }
        public void HintergrundBewegenUnten(ref Control.ControlCollection controlCollection)
        {
            //TODO alles nach oben bewegen
            //ganz oben die Controls löschen
            //unten neue Controls hinzufügen
        }
    }
}
