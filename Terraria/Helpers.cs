using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Helpers
    {
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
