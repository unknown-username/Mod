using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Backup : ModCommand
    {
        public Backup(string name)
            : base(name)
        {

        }

        public override void Execute(List<string> args)
        {
            //backup inventory
            Mod.BackupInventory = Main.player[Main.myPlayer].inventory;

            //backup armor
            Mod.BackupArmor = Main.player[Main.myPlayer].armor;

            //notify user
            Helpers.Result("Backed up Inventory and Armor!", true);
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
