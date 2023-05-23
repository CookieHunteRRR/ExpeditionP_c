using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class SpearWeapon : Weapon
    {
        static readonly double armorIgnoreChance = 0.6;
        static readonly string attackMessage = "Вы протыкаете врага, нанося {0} урона";

        internal SpearWeapon() : base("weapon_spear")
        {
            Info.Name = "Копье";
            Attack = new Attack_Spear();
            SpecialDescription = String.Format("Атаки этим оружием имеют {0}% шанс проигнорировать броню противника",
                armorIgnoreChance * 100);

            Stats.CritChance = 5;
            Stats.CritDamage = 40;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Epic);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.CritChance);
            Tags.Add(Tag.CritDamage);
            Tags.Add(Tag.Critical);
        }

        class Attack_Spear : Attack
        {
            internal Attack_Spear()
            {
                MinDamage = 24;
                MaxDamage = 28;
                DamageType = DamageType.Physical;
                Message = attackMessage;
            }

            internal override void Hit(ExpeditionManager manager)
            {
                BattleManager battle = manager.BattleManager;
                if (Utils.CheckProbability(armorIgnoreChance))
                {
                    var enemyEntityStats = battle.Enemy.BattleStats.CurrentEntityStats;
                    int savedDefense = enemyEntityStats.Defense;
                    if (enemyEntityStats.Defense > 0)
                    {
                        enemyEntityStats.Defense = 0;
                        Program.SendToLog("Броня проигнорирована " + savedDefense);
                        battle.MakeAttack(this, true);
                        enemyEntityStats.Defense = savedDefense;
                        return;
                    }
                }
                battle.MakeAttack(this, true);
            }
        }
    }
}
