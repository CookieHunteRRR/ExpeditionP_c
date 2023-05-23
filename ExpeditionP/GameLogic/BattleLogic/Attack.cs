using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.BattleLogic
{
    internal class Attack
    {
        internal double MinDamage { get; set; }
        internal double MaxDamage { get; set; }
        internal DamageType DamageType { get; set; }

        /// <summary>
        /// <para>Сообщение ОБЯЗАНО содержать {0}, в которое форматирование вставит нанесенный урон.</para>
        /// <para>Пример: "Вы замахиваетесь кулаками, нанося {0} урона"</para>
        /// </summary>
        internal string Message { get; set; }
        internal bool CanBeEvaded { get; set; }

        internal Attack()
        {
            CanBeEvaded = true;
        }

        internal int GetDamage()
        {
            return Program.Random.Next((int)MinDamage, (int)MaxDamage);
        }

        internal virtual void Hit(ExpeditionManager manager) { }
    }
}
