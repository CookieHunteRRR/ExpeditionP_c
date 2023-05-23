using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.BattleLogic
{
    internal class Ability : EventSubject
    {
        internal Info Info { get; set; }
        internal int BaseCooldown { get; init; } // Базовый кулдаун
        // Актуальный базовый кулдаун (-1 - не рекастится в ЭТОМ бою, 0 - возможно использовать сколь угодно раз в этом ходу
        // >=1 - обычная абилка с кд)
        internal int Cooldown { get; set; } 
        internal int InitialAvailableIn { get; set; } // Отсчет до первой возможности активации абилки в начале боя
        internal int AvailableIn { get; set; } // Отсчет до возможности повторной активации абилки (после юза например)
        // В общем для понимания:
        // BaseCooldown - допустим 3 хода. Это константа - базовый кд у абилки всегда такой, и при сбросе актуального
        // сбрасываться он будет именно до BaseCooldown
        // Cooldown - допустим, есть шмотка которая дает -1 ход кд всем абилкам. Получится, что будет BaseCooldown 3, а
        // Cooldown 2. При юзе абилки кд у абилки будет 2 хода, а не 3.
        // InitialAvailableIn - кд в начале боя, который не дает использовать абилку с первого же хода
        // AvailableIn - сам кулдаун, собственно поле, которое код будет проверять, чтобы узнать, можно ли использовать эту абилку прямо сейчас.
        // Т.е. при активации абилки AvailableIn = Cooldown, а затем в конце хода все AvailableIn всех
        // кулдаунов снижаются на 1. Если AvailableIn = 0, то абилку снова можно использовать
        internal int BaseCost { get; init; } // Базовая стоимость в мане/хп
        internal int Cost { get; set; } // Актуальная стоимость (после модификаций от шмоток, баффов и прочего)
        internal bool UsesTurn { get; set; } // Определяет, тратится ли ход на использование этой абилки
        internal bool CooldownResetOnBattleEnd { get; set; } // Определяет, сбросится ли кд до InitialAvailableIn в конце боя
        internal bool UsableOutOfBattle { get; set; }
        internal bool UsedDirectly { get; set; } // Определяет, может ли кастер использовать абилку сам, либо же она активируется сама по себе


        internal Ability(int cooldown, int cost, int initialAvailableIn = 0, bool usesTurn = true)
        {
            Info = new Info();
            BaseCooldown = cooldown;
            BaseCost = cost;
            InitialAvailableIn = initialAvailableIn;
            AvailableIn = InitialAvailableIn;
            UsesTurn = usesTurn;
            CooldownResetOnBattleEnd = true;
            UsableOutOfBattle = false;
            UsedDirectly = true;
            ResetStats();
        }

        internal void ResetStats()
        {
            Cooldown = BaseCooldown;
            Cost = BaseCost;
        }

        internal bool IsOnCooldown() { return AvailableIn > 0; }
        internal void ReduceCooldown() { if (AvailableIn > 0) AvailableIn--; }

        internal void ResetCooldownOnBattleEnd() { if (CooldownResetOnBattleEnd) AvailableIn = InitialAvailableIn; }

        internal string GetAbilityInfo()
        {
            if (Info.Name is null) return String.Empty;
            if (Info.Description is null) return String.Empty;

            return $"\"{Info.Name}\": {Info.Description}";
        }

        // Активировать base.Activate в оверрайднутом для обычных абилок (не триггеров)
        // Кост тратится вне этого метода, т.к. в этом методе неизвестно кто активирует абилку
        internal virtual void Activate(ExpeditionManager manager, Entity caster) { AvailableIn = Cooldown; }
    }
}
