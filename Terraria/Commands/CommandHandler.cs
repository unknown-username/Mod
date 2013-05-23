using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    public class CommandHandler
    {
        private List<ModCommand> m_Commands = new List<ModCommand>();

        public void Add(ModCommand cmd)
        {
            m_Commands.Add(cmd);
        }

        public void Execute(string[] args)
        {
            //pop off the first arg
            string command_name = args[0];
            
            //search for command with matching name in the list of commands
            ModCommand found_command = null;
            foreach (ModCommand command in m_Commands)
            {
                if (command_name == command.Name)
                {
                    found_command = command;
                    break;
                }
            }

            //assuming we found a matching command
            if (found_command != null)
            {
                //assemble remaining commands into a new list
                List<string> commands = new List<string>();
                for (int i = 1; i < args.Length; ++i)
                {
                    commands.Add(args[i]);
                }

                //pass them to the command via execution
                found_command.Execute(commands);
            }
            else
            {
                //no command was found, notify the user
                Helpers.Result("Unknown Command: " + command_name);
            }
        }

        public void OnLeftClick(int MouseX, int MouseY)
        {
            //forward the click to all commands
            foreach (ModCommand command in m_Commands)
            {
                command.OnLeftClick(MouseX, MouseY);
            }
        }

        public void OnRightClick(int MouseX, int MouseY)
        {
            //forward the click to all commands
            foreach (ModCommand command in m_Commands)
            {
                command.OnRightClick(MouseX, MouseY);
            }
        }
    }
}
