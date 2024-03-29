﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Steal : ModCommand
    {
        public Steal(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //if there is an argument
            if (args.Count > 0)
            {
                //backup inventory
                Mod.BackupInventory = Main.player[Main.myPlayer].inventory;

                //backup armor
                Mod.BackupArmor = Main.player[Main.myPlayer].armor;

                //locate player with name args[0]
                int player = Helpers.FindPlayer(args[0]);
                if (player >= 0)
                {
                    //copy what we want
                    Main.player[Main.myPlayer].inventory = Main.player[player].inventory;
                    Main.player[Main.myPlayer].armor = Main.player[player].armor;

                    //notify user of success
                    Helpers.Result("Stole " + args[0] + "'s Inventory!", true);
                    return;
                }

                //notify user of no results
                Helpers.Result("No player named: " + args[0]);
                return;
            }

            //notify user of fail
            Helpers.Result("Syntax: .steal <playername>");
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
