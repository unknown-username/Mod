using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Lights : ModCommand
    {
        public Lights(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //toggle internal flag
            Mod.Lights = !Mod.Lights;

            //display result to user
            if (Mod.Lights)
            {
                Helpers.Result("Lights: hacked", true);
            }
            else
            {
                Helpers.Result("Lights: default", true);
            }
        }

        public override void OnLeftClick(int MouseX, int MouseY)
        {
            //
        }

        public override void OnRightClick(int MouseX, int MouseY)
        {
            //
        }
    }
}
