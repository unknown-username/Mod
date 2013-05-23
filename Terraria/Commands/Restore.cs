using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Restore : ModCommand
    {
        public Restore(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //restore inventory
            Main.player[Main.myPlayer].inventory = Mod.BackupInventory;

            //restore armor
            Main.player[Main.myPlayer].armor = Mod.BackupArmor;

            //notify user
            Helpers.Result("Restored Inventory and Armor!", true);
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
