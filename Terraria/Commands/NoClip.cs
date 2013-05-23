using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class NoClip : ModCommand
    {
        public NoClip(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //toggle the global flag
            Mod.NoClip = !Mod.NoClip;

            //display result to user
            if (Mod.NoClip)
            {
                Helpers.Result("NoClip enabled", true);
            }
            else
            {
                Helpers.Result("NoClip disabled", true);
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
