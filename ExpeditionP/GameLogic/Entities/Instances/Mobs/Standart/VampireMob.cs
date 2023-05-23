using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Entities.Instances.Mobs.Standart
{
    internal class VampireMob : Mob
    {
        internal VampireMob() : base("Вампир", Items.Tag.Standart)
        {
            AI.AddAttackToPool(new Attack_MobGeneric(8, 10, DamageType.Physical, "Вампир кусает вас, нанося {0} урона"));
            AI.AddAbilityToPool(new Ability_Vampire_Devourment());

            Stats.Defense = 10;

            LootTable.AddItem(ItemHolder.RegisteredItems["acc_vampiricfang"]);
        }

        class Ability_Vampire_Devourment : Ability
        {
            internal Ability_Vampire_Devourment() : base(4, 0) { }

            internal override void Activate(ExpeditionManager manager, Entity caster)
            {
                base.Activate(manager, caster);
                Player player = manager.BattleManager.Player;
                Mob mob = (Mob)caster;

                mob.BattleStats.HiddenEntityStats.Vampirism = 100;
                manager.BattleManager.MakeAttack(new Attack_MobGeneric(10, 15, DamageType.Physical,
                    "Вампир высасывает из вас кровь, нанося {0} урона"), false);
                mob.BattleStats.HiddenEntityStats.Vampirism = 0;
            }
        }
    }
}
