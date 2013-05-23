using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Shoot : ModCommand
    {
        public Shoot(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //toggle internal flag
            Mod.Shooting = !Mod.Shooting;

            //display result to user
            if (Mod.Shooting)
            {
                Helpers.Result("Shooting enabled", true);
            }
            else
            {
                Helpers.Result("Shooting disabled", true);
            }
        }

        public override void OnLeftClick(int MouseX, int MouseY)
        {
            if (Mod.Shooting)
            {
                //changed damage from 99999 to 175 to prevent TShock servers from disabling the player
                //int index = Projectile.NewProjectile(MouseX, MouseY, 0, 0, 2, 175, 0.0f, 0xff);
                int index = Projectile.NewProjectile(MouseX, MouseY, 0, 1, 92, 175, 25f, Main.myPlayer);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(0x1b, -1, -1, "", index, 0f, 0f, 0f, 0);
                }
            }
        }

        public override void OnRightClick(int MouseX, int MouseY)
        {
            //
        }
    }
}
