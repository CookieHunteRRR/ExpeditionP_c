using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExpeditionP.GameLogic.Entities
{
    /// <summary>
    /// Базовый класс, отвечающий за статы всех существ в игре (игрок, моб, босс)
    /// </summary>
    public class EntityStats
    {
        public int Health { get; set; }

        /// <summary>
        /// <para>Целое число, влияющее на множитель получаемого физического урона</para>
        /// Если число больше 0, множитель будет идти от ~0.99 до <see cref="Constants.minimalDefenseMultiplier">минимального множителя защиты</see><br/>
        /// Если число меньше 0, множитель будет идти от ~1.01 до <see cref="Constants.maximumDefenseMultiplier">максимального множителя защиты</see>
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Целое число, влияющее на шанс уклонения от физического урона <br/>
        /// От 0 до 100%
        /// </summary>
        public int Evasion { get; set; }

        /// <summary>
        /// Целое число, влияющее на шанс критического урона у физической атаки <br/>
        /// От 0 до 100%
        /// </summary>
        public int CritChance { get; set; }

        /// <summary>
        /// Целое число, влияющее на множитель критического урона у физической атаки
        /// </summary>
        public int CritDamage { get; set; }

        /// <summary>
        /// Целое число, отвечающее за ресурс для каста спеллов
        /// </summary>
        public int Mana { get; set; }

        /// <summary>
        /// <para>Целое число, влияющее на множитель получаемого магического урона </para>
        /// Если число больше 0, множитель будет идти от ~0.99 до <see cref="Constants.minimalDefenseMultiplier">минимального множителя защиты</see><br/>
        /// Если число меньше 0, множитель будет идти от ~1.01 до <see cref="Constants.maximumDefenseMultiplier">максимального множителя защиты</see>
        /// <para>Замечу прям вот указываю тут на это: этот стат зависит от того же множителя, что и у защиты физической</para>
        /// </summary>
        public int Mitigation { get; set; }

        /// <summary>
        /// Целое число, влияющее на шанс уклонения от магического урона <br/>
        /// От 0 до 100%
        /// </summary>
        public int Annulment { get; set; }

        /// <summary>
        /// Целое число, влияющее на множитель исходящего магического урона
        /// </summary>
        public int Amplification { get; set; }

        public EntityStats()
        {
            var statFields = typeof(EntityStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                var fieldInfo = GetType().GetProperty(field.Name);
                fieldInfo.SetValue(this, 0);
            }
        }

        internal void SetStatsTo(EntityStats stats)
        {
            var statFields = typeof(EntityStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                var fieldInfo = GetType().GetProperty(field.Name);
                fieldInfo.SetValue(this, fieldInfo.GetValue(stats));
            }
        }

        public void SetStatsToEntityDefault()
        {
            this.SetStatsTo(Constants.defaultEntityStats);
        }

        public EntityStats Copy()
        {
            EntityStats copy = new EntityStats();
            var statFields = typeof(EntityStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                // По моей логике это должно выбрать конкретный филд в copy (например Health)
                var fieldInfo = copy.GetType().GetProperty(field.Name);
                // А это затем устанавливает Health значение Health оригинального объекта ну типа копирование
                fieldInfo.SetValue(copy, field.GetValue(this));
            }
            return copy;
        }

        /// <summary>
        /// Применяет изменения статов от эффектов или предметов, проверяя, не выходят ли статы за рамки дозволенного
        /// </summary>
        /// <param name="changesToApply"></param>
        /// <returns><see langword="true"/>, если применение статов не опустило Health или Mana ниже 1<br/><see langword="false"/> - иначе</returns>
        public void ApplyStatChanges(EntityStats changesToApply)
        {
            var statFields = typeof(EntityStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                var fieldInfo = GetType().GetProperty(field.Name);
                fieldInfo.SetValue(this, SumIntegerStats(fieldInfo, this, changesToApply));
            }
        }

        /// <summary>
        /// Суммирует стат из оригинального EntityStats и из того, что используется для суммирования. Если это стат вроде
        /// Health/Mana, который не может быть ниже определенного числа - возвращается 1.
        /// </summary>
        /// <returns></returns>
        double SumDoubleStats(PropertyInfo info, EntityStats orig, EntityStats newstat)
        {
            return (double)info.GetValue(orig) + (double)info.GetValue(newstat);
        }

        int SumIntegerStats(PropertyInfo info, EntityStats orig, EntityStats newstat)
        {
            int toReturn = (int)info.GetValue(orig) + (int)info.GetValue(newstat);
            if (info.Name == "Health")
                if (toReturn < Constants.minimumHealth) return Constants.minimumHealth;
            if (info.Name == "Mana")
                if (toReturn < Constants.minimumMana) return Constants.minimumMana;
            return toReturn;
        }

        internal string GetStatsAsString()
        {
            string toReturn = string.Empty;
            var statFields = typeof(EntityStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                var fieldInfo = GetType().GetProperty(field.Name);
                var value = (int)fieldInfo.GetValue(this);
                //if (fieldInfo.Name == "Health" || fieldInfo.Name == "Mana")
                //    value = (int)fieldInfo.GetValue(this);
                //else
                //    value = (double)fieldInfo.GetValue(this);
                if (value != 0) toReturn += fieldInfo.Name + ": " + value + "\r\n";
            }
            if (toReturn.Length > 0) toReturn.Remove(toReturn.Length - 4, 4);
            return toReturn;
        }
    }
}
