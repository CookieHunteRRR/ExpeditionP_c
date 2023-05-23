using ExpeditionP.GameLogic.BattleLogic.Effects;
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
    internal class HeadhunterAcc : Accessory
    {
        internal HeadhunterAcc() : base("acc_headhunter")
        {
            IsReactingToEvents = true;

            Info.Name = "Охотник за головами";
            SpecialDescription = "Вы крадете положительный эффект у противника при победе над ним на 20 ходов";
            Stats.Health = 20;

            SetRarity(Tag.Legendary);
            //Weight = 20000;
            Info.SetRandomAppearance(false);

            Info.ExpeditionTag = Tag.Standart;
            Tags.Add(Tag.Offensive);
            Tags.Add(Tag.Health);
        }

        internal override void RegisterEvents()
        {
            EventManager.BattleEndEvent.UserEvent += onBattleEnd;
        }

        internal override void UnregisterEvents()
        {
            EventManager.BattleEndEvent.UserEvent -= onBattleEnd;
        }

        void onBattleEnd(ExpeditionManager? manager, BattleEndEventArgs? args)
        {
            // Должно срабатывать только когда моба убили
            if (args.BattleFinishReason != BattleLogic.BattleFinishReason.EnemyDied) return;

            var battle = manager.BattleManager;
            var enemyEffects = battle.Enemy.BattleStats.CurrentEffects;
            if (enemyEffects.Count > 0)
            {
                List<Effect> stealableEffects = new List<Effect>();
                foreach (var effect in enemyEffects)
                {
                    if (effect.EffectType == EffectType.Buff && !effect.IsHidden)
                        stealableEffects.Add(effect);
                }
                if (stealableEffects.Count < 1) return;
                Effect toSteal = stealableEffects[Program.Random.Next(stealableEffects.Count)];
                enemyEffects.Remove(toSteal);
                toSteal.SetInitialDuration(20);
                toSteal.IsRemovedOnBattleEnd = false;
                battle.Player.BattleStats.ApplyEffect(toSteal);
                Program.SendToLog("Headhunter украл эффект " + toSteal.Name);
            }
        }
    }
}
