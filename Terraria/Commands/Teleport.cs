using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Teleport : ModCommand
    {
        public Teleport(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //if there is an argument
            if (args.Count > 0)
            {
                //locate player with name args[0]
                int player = Helpers.FindPlayer(args[0]);
                if (player >= 0)
                {
                    //set location to that of the other player
                    Main.player[Main.myPlayer].position = Main.player[player].position;

                    //notify user of success
                    Helpers.Result("Teleported to " + args[0], true);
                    return;
                }

                //notify user of no results
                Helpers.Result("No player named: " + args[0]);
                return;
            }

            //notify user of fail
            Helpers.Result("Syntax: .tp <playername>");
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
