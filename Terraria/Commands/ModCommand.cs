using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terraria
{
    public abstract class ModCommand
    {
        private string m_Name = "";
        public string Name
        {
            get { return m_Name; }
        }

        public ModCommand(string name)
        {
            m_Name = name;
        }

        public abstract void Execute(List<string> args);

        public abstract void OnLeftClick(int MouseX, int MouseY);
        public abstract void OnRightClick(int MouseX, int MouseY);
    }
}
