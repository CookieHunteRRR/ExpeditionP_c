using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities
{
    /// <summary>
    /// Будет использоваться для всего, что не требует явного отображения в игре
    /// Если условное здоровье нужно показывать прямо в интерфейсе, то сколько всего дополнительного флат физ урона имеется у игрока
    /// показывать явно не нужно
    /// </summary>
    public class HiddenStats
    {
        // Integers
        public int PhysDmgBonus { get; set; } = 0;
        public int Vampirism { get; set; } = 0;
        public int Heal { get; set; } = 0;
        public int FleeChance { get; set; } = 0;
        public double PhysDmgMultiplier { get; set; } = 0;
        public double MagicDmgMultiplier { get; set; } = 0;
        public double AllDmgMultiplier { get; set; } = 0;
        public double FinalDmgMultiplier { get; set; } = 0;
        public double HealthMultiplier { get; set; } = 0;

        // Все остальное
        public bool GuaranteedCrit { get; set; } = false;

        // Короче расклад такой, для множителей используем Double, для остального Int32
        // В общем по дефолту устанавливается множитель 0, потому что этот множитель идет в хидденстаты
        // предметов. Если его установить на 1, то любой подобранный предмет или наложенный эффект будет прибавлять 1 к множителю
        // и там будут получаться неадекватные множители, которые вообще не должны быть. Так что при создании экземпляра HiddenStats
        // множители равны 0, а при ресете статов (который происходит только при рекалькулейте у энтити) множители устанавливаются на 1
        internal void ResetStats()
        {
            var statFields = typeof(HiddenStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                var fieldInfo = GetType().GetProperty(field.Name);
                if (fieldInfo.PropertyType.Name == "Int32")
                {
                    fieldInfo.SetValue(this, 0);
                }
                else if (fieldInfo.PropertyType.Name == "Double")
                {
                    fieldInfo.SetValue(this, 1);
                }
            }
            GuaranteedCrit = false;
        }

        internal void ApplyStatChanges(HiddenStats toApply)
        {
            var statFields = typeof(HiddenStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                var fieldInfo = GetType().GetProperty(field.Name);
                if (fieldInfo.PropertyType.Name == "Int32")
                {
                    fieldInfo.SetValue(this, SumIntegerStats(fieldInfo, this, toApply));
                }
                else if (fieldInfo.PropertyType.Name == "Double")
                {
                    fieldInfo.SetValue(this, SumDoubleStats(fieldInfo, this, toApply));
                }
            }
            if (toApply.GuaranteedCrit) this.GuaranteedCrit = true;
        }

        int SumIntegerStats(PropertyInfo info, HiddenStats orig, HiddenStats newstat)
        {
            return (int)info.GetValue(orig) + (int)info.GetValue(newstat);
        }

        double SumDoubleStats(PropertyInfo info, HiddenStats orig, HiddenStats newstat)
        {
            return (double)info.GetValue(orig) + (double)info.GetValue(newstat);
        }

        internal HiddenStats Copy()
        {
            HiddenStats copy = new HiddenStats();
            var statFields = typeof(HiddenStats).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in statFields)
            {
                var fieldInfo = GetType().GetProperty(field.Name);
                if (fieldInfo.PropertyType.Name != "Int32" &&
                    fieldInfo.PropertyType.Name != "Double") continue;
                fieldInfo.SetValue(copy, fieldInfo.GetValue(this));
            }
            return copy;
        }
    }
}
