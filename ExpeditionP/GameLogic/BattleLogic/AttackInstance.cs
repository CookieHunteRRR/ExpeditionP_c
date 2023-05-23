using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.BattleLogic
{
    internal class AttackInstance
    {
        internal int Damage { get; private set; }
        internal string AdditionalString { get; set; }
        internal bool IsEvaded { get; private set; }
        internal bool IsCrit { get; private set; }
        internal string AttackName { get; private set; }

        internal AttackInstance(int damage, string additionalString = "")
        {
            this.Damage = damage;
            this.AdditionalString = additionalString;
        }

        internal void SetDebugInfo(bool isEvaded, bool isCrit, string attackName)
        {
            IsEvaded = isEvaded;
            IsCrit = isCrit;
            AttackName = attackName;
        }

        internal void SetDebugInfo(bool isEvaded, double isCrit, string attackName)
        {
            IsEvaded = isEvaded;
            IsCrit = (isCrit > 0) ? true : false;
            AttackName = attackName;
        }
    }
}
