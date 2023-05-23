using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities
{
    /// <summary>
    /// Класс, отвечающий за то, какие именно атаки и абилки из общего пула способен использовать моб в данный момент
    /// Например, хп триггеры могут поменять поведение моба и атаки с абилками станут другими
    /// </summary>
    internal class EntityBehavior
    {
        internal List<int> AttackIndexes { get; init; }
        internal List<int> AbilityIndexes { get; init; }

        internal EntityBehavior() { AttackIndexes = new List<int>(); AbilityIndexes = new List<int>(); }
    }
}
