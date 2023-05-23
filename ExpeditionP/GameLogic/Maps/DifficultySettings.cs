using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Maps
{
    internal class DifficultySettings
    {
        internal double DefaultDifficultyMultiplier { get; private set; }
        internal double DifficultyStep { get; private set; }
        internal double MultiplierCap { get; private set; }
        
        internal DifficultySettings()
        {
            SetDifficulty(1, 0.1, 2);
        }

        internal DifficultySettings(double dfltDiffMp, double diffStep, double mpCap)
        {
            SetDifficulty(dfltDiffMp, diffStep, mpCap);
        }

        internal void SetDifficulty(double dfltDiffMp, double diffStep, double mpCap)
        {
            DefaultDifficultyMultiplier = dfltDiffMp;
            DifficultyStep = diffStep;
            MultiplierCap = mpCap;
        }

        internal void SetDifficulty(DifficultySettings settings)
        {
            DefaultDifficultyMultiplier = settings.DefaultDifficultyMultiplier;
            DifficultyStep = settings.DifficultyStep;
            MultiplierCap = settings.MultiplierCap;
        }
    }
}
