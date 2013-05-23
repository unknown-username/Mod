using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    class Mod
    {
        //====================================================================================================
        //  Flags
        //====================================================================================================

        //GodMode
        private static bool m_Godmode = false;
        public static bool GodMode
        {
            get { return m_Godmode; }
            set { m_Godmode = value; }
        }

        //NoClip
        private static bool m_NoClip = false;
        public static bool NoClip
        {
            get { return m_NoClip; }
            set { m_NoClip = value; }
        }

        //====================================================================================================
        //  Storage Variables
        //====================================================================================================

        private static Item[] m_BackupInventory = new Item[49];
        public static Item[] BackupInventory
        {
            get { return m_BackupInventory; }
            set { m_BackupInventory = value; }
        }

        private static Item[] m_BackupArmor = new Item[11];
        public static Item[] BackupArmor
        {
            get { return m_BackupArmor; }
            set { m_BackupArmor = value; }
        }

        //====================================================================================================

        private static CommandHandler m_Handler = null;

        public static void Init()
        {
            //create new command handler
            m_Handler = new CommandHandler();

            //add commands here
            m_Handler.Add(new GodMode("god"));
            m_Handler.Add(new NoClip("noclip"));
            m_Handler.Add(new Backup("backup"));
            m_Handler.Add(new Restore("restore"));
            m_Handler.Add(new Steal("steal"));
            m_Handler.Add(new Shoot("shoot"));
        }

        public static void DeInit()
        {
            //remove command handler
            m_Handler = null;
        }

        public static void OnLeftClick(int MouseX, int MouseY)
        {
            //forward the click event to the command handler
            m_Handler.OnLeftClick(MouseX, MouseY);
        }

        public static void OnRightClick(int MouseX, int MouseY)
        {
            //forward the click event to the command handler
            m_Handler.OnRightClick(MouseX, MouseY);
        }

        public static void OnCommand(string text)
        {
            //early out if there is no command handler
            if (m_Handler == null) return;

            //avoid null reference exception from no text
            if (text.Length > 1)
            {
                //break up string
                string[] args = text.Split(' ');

                //ensure there is more than just the command character
                if (args.Length > 0 && args[0].Length > 1)
                {
                    //break off command character at start
                    args[0] = args[0].Remove(0, 1);

                    //have the command handler execute the command
                    m_Handler.Execute(args);
                }
            }
        }
    }
}
