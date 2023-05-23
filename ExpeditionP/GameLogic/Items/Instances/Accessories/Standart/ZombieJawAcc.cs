using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.EventHandling.Events;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.Effects.EffectList;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class ZombieJawAcc : Accessory
    {
        internal ZombieJawAcc() : base("acc_zombiejaw")
        {
            Info.Name = "Челюсть зомби";
            SpecialDescription = "Накладывает эффект \"Отравление\" в случае критического удара";

            Stats.CritChance = 10;
            Stats.CritDamage = -10;

            IsReactingToEvents = true;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Epic);
            Info.SetRandomAppearance(false);

            Tags.Add(Tag.CritChance);
            Tags.Add(Tag.Critical);
        }

        internal override void RegisterEvents()
        {
            EventManager.PlayerAttackEvent.UserEvent += this.onPlayerCrit;
        }

        internal override void UnregisterEvents()
        {
            EventManager.PlayerAttackEvent.UserEvent -= this.onPlayerCrit;
        }

        void onPlayerCrit(ExpeditionManager manager, PlayerAttackEventArgs? args)
        {
            if (!args.IsNotEvaded) return;
            if (args.IsCrit)
            {
                Mob enemy = manager.BattleManager.Enemy;
                enemy.BattleStats.ApplyEffect(new Effect_Debuff_Poison(5));
            }
        }
    }
}
