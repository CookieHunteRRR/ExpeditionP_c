using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Entities
{
    internal class Mob : Entity
    {
        internal SpawnableInfo Info { get; init; }
        internal EntityAI AI { get; set; }
        internal LootTable LootTable { get; set; }
        internal double FleeInterruptChance { get; set; }
        internal string EncounterMessage { get; set; }

        internal Mob(string name, Tag expeditionTag) : base() 
        {
            AI = new EntityAI(); 
            LootTable = new LootTable();
            Info = new SpawnableInfo();
            FleeInterruptChance = (this is Boss) ? 10 : 0; // дефолтные значения
            EncounterMessage = Constants.defaultEnemyEncounterMessage;

            Info.Name = name;
            Info.Description = String.Empty;
            Info.ExpeditionTag = expeditionTag;
            Info.InternalName = GetMobInternalName();
            Info.SetRandomAppearance(true);
            Info.SetWeight(1000);
            Info.SetDifficultyMultiplierRequirements(0, 100);
        }

        internal override string GetName()
        {
            return Info.Name;
        }

        internal override Mob Copy()
        {
            Mob copy = new Mob(GetName(), (Tag)Info.ExpeditionTag);
            CopyData(copy);
            return copy;
        }

        protected override void CopyData(object copy)
        {
            Mob correctedCopy = (Mob)copy;
            base.CopyData(correctedCopy);
            correctedCopy.Info.Name = GetName();
            correctedCopy.AI = AI;
            correctedCopy.LootTable = LootTable;
        }

        protected string GetMobInternalName()
        {
            StringBuilder sb = new StringBuilder();
            string rawName;
            if (this as Boss is not null)
            {
                Boss boss = (Boss)this;
                sb.Append("boss_");
                sb.Append(boss.Info.ExpeditionTag.ToString().ToLower());
                sb.Append("_");
                rawName = boss.GetType().Name.ToLower();
                sb.Append(rawName.Substring(0, rawName.Length - 4)); // -4 т.к. VasyaPupkinBoss
                return sb.ToString();
            }
            sb.Append("mob_");
            sb.Append(this.Info.ExpeditionTag.ToString().ToLower());
            sb.Append("_");
            rawName = this.GetType().Name.ToLower();
            sb.Append(rawName.Substring(0, rawName.Length - 3)); // -3 т.к. ZombieMob
            return sb.ToString();
        }
    }
}
