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
    internal class LeisurelyClockAcc : Accessory
    {
        internal LeisurelyClockAcc() : base("acc_leisurelyclock")
        {
            Info.Name = "Неторопливые часы";
            SpecialDescription = "Стрелка часов поворачивается только когда игрок наносит критический удар. " +
                "Каждый некритический удар делает игрока более нетерпеливым, в силу чего его физический урон и шанс критического удара увеличиваются. " +
                "Успешный критический удар поворачивает стрелку часов и успокаивает игрока.";

            Stats.CritChance = -10;
            Stats.CritDamage = 30;

            IsReactingToEvents = true;

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Legendary);

            Tags.Add(Tag.CritChance);
            Tags.Add(Tag.Critical);
        }

        internal override void RegisterEvents()
        {
            EventManager.PlayerAttackEvent.UserEvent += this.onPlayerAttack;
        }

        internal override void UnregisterEvents()
        {
            EventManager.PlayerAttackEvent.UserEvent -= this.onPlayerAttack;
        }

        void onPlayerAttack(ExpeditionManager manager, PlayerAttackEventArgs? args)
        {
            Player player = manager.BattleManager.Player;
            if (!args.IsNotEvaded) return;
            if (args.IsCrit)
            {
                var effectToRemove = player.BattleStats.FindEffectOfType(new Effect_Buff_Impatience().GetType());
                player.BattleStats.UndoEffect(effectToRemove);
                manager.SendToLog("Стрелка часов сдвинулась");
            }
            else
            {
                player.BattleStats.ApplyEffect(new Effect_Buff_Impatience());
            }
        }
    }
}
