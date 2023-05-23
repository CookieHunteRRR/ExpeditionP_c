using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps
{
    internal class MapInfo : Info
    {
        internal MapInfo() : base() { }

        internal MapInfo(string internalName, string name, string? description, Tag expeditionTag) : base(internalName, name, description, expeditionTag)   
        {
            InternalName = internalName;
            Name = name;
            Description = description;
            ExpeditionTag = expeditionTag;
        }
    }
}
