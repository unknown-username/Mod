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

        private bool m_Shooting = false;

        public override void Execute(List<string> args)
        {
            //toggle internal flag
            m_Shooting = !m_Shooting;

            //display result to user
            if (m_Shooting)
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
            if (m_Shooting)
            {
                int index = Projectile.NewProjectile(MouseX, MouseY, 0, 0, 2, 999999, 0.0f, 0xff);
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
