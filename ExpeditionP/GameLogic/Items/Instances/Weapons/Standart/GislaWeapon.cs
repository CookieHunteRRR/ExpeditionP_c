using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.EventHandling.Events;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons.Standart
{
    internal class GislaWeapon : GenericMeleeWeapon
    {
        static readonly double minPower = 0.0;
        static readonly double maxPower = 2.0;
        internal double Power;

        internal GislaWeapon() : base()
        {
            Power = minPower;
            IsReactingToEvents = true;

            Info.InternalName = "weapon_gisla";
            Info.Name = "Гисла";
            SpecialDescription = $"Увеличивает атаку вплоть до {maxPower * 100}% в зависимости от актуального процента оставшегося здоровья";

            Attack.MinDamage = 16;
            Attack.MaxDamage = 20;
            Attack.DamageType = DamageType.Physical;
            Attack.Message = "Вы протыкаете противника, нанося {0} урона";

            UpdateStats();

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Legendary);

            Tags.Add(Tag.Melee);
            Tags.Add(Tag.Physical);
            Tags.Add(Tag.Offensive);
        }

        internal override void RegisterEvents()
        {
            EventManager.EntityHealthChangeEvent.UserEvent += onEntityHealthChange;
        }

        internal override void UnregisterEvents()
        {
            EventManager.EntityHealthChangeEvent.UserEvent -= onEntityHealthChange;
        }

        void onEntityHealthChange(ExpeditionManager? manager, EntityHealthChangeEventArgs? args)
        {
            ExpeditionManager? expManager = Program.Expedition.ExpeditionManager;
            if (expManager is null) return;
            if (args is null) return;
            if (!args.IsEntityAPlayer) return; // работает только если игроку хп поменяли

            // Обновляем силу эффекта
            double hpRatio = expManager.GameInstance.Player.GetHpRatio();
            Power = maxPower * (1 - hpRatio);
            UpdateStats();
            // очень не уверен что стоит в этом ивенте рекалькулейт юзать но хз куда еще его пихнуть
            expManager.GameInstance.Player.RecalculateStats();
        }

        void UpdateStats()
        {
            HiddenStats.AllDmgMultiplier = Power;
        }
    }
}
