using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.EventHandling.Events;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class SneakyBlowerAcc : Accessory
    {
        static readonly int minCrit = 0;
        static readonly int step = 2;
        static readonly int maxCrit = 30;

        int currentCrit;

        internal SneakyBlowerAcc() : base("acc_sneakyblower")
        {
            IsReactingToEvents = true;

            Info.Name = "Подлая воздуходувка";
            Info.Description = "Ударить ниже пояса может лишь подлый ублюдок";

            Stats.Evasion = 15;
            Stats.CritDamage = 10;

            currentCrit = minCrit;
            UpdateStats();
            SpecialDescription = $"При каждом успешном увороте увеличивает шанс критического удара, вплоть до {maxCrit}%";

            SetRarity(Tag.Legendary);

            Info.ExpeditionTag = Tag.Standart;
            Tags.Add(Tag.Defensive);
            Tags.Add(Tag.Evasion);
            Tags.Add(Tag.Critical);
            Tags.Add(Tag.CritDamage);
            Tags.Add(Tag.CritChance);
        }

        internal override void RegisterEvents()
        {
            EventManager.MobAttackEvent.UserEvent += onMobAttack;
        }

        internal override void UnregisterEvents()
        {
            EventManager.MobAttackEvent.UserEvent -= onMobAttack;
            // т.к. происходит при анеквипе сбрасываем до 0
            currentCrit = minCrit;
            UpdateStats();
        }

        void UpdateStats()
        {
            Stats.CritChance = currentCrit;
        }

        void onMobAttack(ExpeditionManager? manager, MobAttackEventArgs? args)
        {
            if (args.IsNotEvaded) return;
            if (currentCrit >= maxCrit) return;

            currentCrit += step;
            UpdateStats();
            manager.GameInstance.Player.RecalculateStats();
        }
    }
}
