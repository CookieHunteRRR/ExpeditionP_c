using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;
using static ExpeditionP.GameLogic.BattleLogic.Effects.EffectList;

namespace ExpeditionP.GameLogic.Entities.Instances.Mobs.Standart
{
    internal class ZombieMob : Mob
    {
        internal ZombieMob() : base("Зомби", Items.Tag.Standart)
        {
            AI.AddAttackToPool(new Attack_MobGeneric(5, 8, DamageType.Physical, "Зомби кусает вас, нанося {0} урона"));
            AI.AddAbilityToPool(new Ability_Zombie_InfectedScratch());

            Stats.Health = 75;
            Stats.Defense = 5;

            LootTable.AddItem(ItemHolder.RegisteredItems["acc_zombieheart"]);
            LootTable.AddItem(ItemHolder.RegisteredItems["acc_zombiejaw"]);
        }

        class Ability_Zombie_InfectedScratch : Ability
        {
            internal Ability_Zombie_InfectedScratch() : base(3, 0) { }

            internal override void Activate(ExpeditionManager manager, Entity caster)
            {
                base.Activate(manager, caster);
                Player player = manager.BattleManager.Player;
                manager.SendToLog("Зомби заражает вас");
                //manager.BattleManager.MakeAttack(new Attack_MobGeneric(5, 8, DamageType.Physical,
                //    "Зомби кусает вас, нанося {0} урона и отравляя вас"), false);
                player.BattleStats.ApplyEffect(new Effect_Debuff_Poison(5));
            }
        }
    }
}
