using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    internal class GenerationRules
    {
        internal GenerationPriority Priority { get; set; }
        internal Tag? ExpeditionTag { get; set; }
        internal Dictionary<Tag, int> PlayerTagsDict { get; set; }
        internal GenerationMethod GenerationMethod { get; set; }

        internal GenerationRules(GenerationPriority priority, Tag? expeditionTag, GenerationMethod generationMethod)
        {
            Priority = priority;
            ExpeditionTag = expeditionTag;
            PlayerTagsDict = new Dictionary<Tag, int>();
            GenerationMethod = generationMethod;
        }
    }
}
