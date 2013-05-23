using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class GodMode : ModCommand
    {
        public GodMode(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //toggle the global flag
            Mod.GodMode = !Mod.GodMode;

            //display result to user
            if (Mod.GodMode)
            {
                Helpers.Result("GodMode enabled", true);
            }
            else
            {
                Helpers.Result("GodMode disabled", true);
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
