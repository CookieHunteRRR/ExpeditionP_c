using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic
{
    internal class SpawnableInfo : Info
    {
        internal bool IsAppearingRandomly { get; private set; }
        internal int Weight { get; private set; }

        /// <summary>
        /// Определяет минимальный необходимый множитель сложности, с которого начнет появляться этот объект при генерации
        /// </summary>
        internal double MinDiffAcquirable { get; private set; }

        /// <summary>
        /// Определяет максимальный возможный множитель сложности, до которого будет генерироваться данный объект
        /// </summary>
        internal double MaxDiffAcquirable { get; private set; }

        internal void SetRandomAppearance(bool state) { IsAppearingRandomly = state; }
        internal void SetWeight(int weight) { Weight = weight; }
        internal void SetDifficultyMultiplierRequirements(double min, double max) { MinDiffAcquirable = min; MaxDiffAcquirable = max; }
    }
}
