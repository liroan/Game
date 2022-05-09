using System.Collections.Generic;
using System.Drawing;
using System;

namespace Game.Model
{
    public class PlayerView:PersonView<Player>
    {
        public PlayerView(Player person, string nameImg = "pngwing"): base(person, nameImg)
        {
            this.person = person;
        }
    }
}