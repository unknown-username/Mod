using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Helpers
    {
        public static int FindPlayer(string name)
        {
            //use temp variable to prevent useless calls to ToLower()
            string t_name = name.ToLower();

            //locate player with name args[0]
            for (int i = 0; i < Main.player.Length; ++i)
            {
                //if name matches
                if (Main.player[i].name.ToLower() == t_name)
                {
                    return i;
                }
            }

            //return failed result
            return -1;
        }

        public static void Result(string text, bool good = false)
        {
            if (good)
            {
                Main.NewText(text, 128, 128, 0);
            }
            else
            {
                Main.NewText(text, 128, 0, 0);
            }
        }
    }
}
