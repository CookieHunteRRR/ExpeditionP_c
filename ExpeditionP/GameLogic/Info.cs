using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic
{
    internal class Info
    {
        internal string InternalName { get; set; }
        internal string Name { get; set; }
        internal string? Description { get; set; }
        internal Tag? ExpeditionTag { get; set; }

        internal Info() { }

        internal Info(string internalName, string name, string? description, Tag? expeditionTag)
        {
            InternalName = internalName;
            Name = name;
            Description = description;
            ExpeditionTag = expeditionTag;
        }
    }
}
