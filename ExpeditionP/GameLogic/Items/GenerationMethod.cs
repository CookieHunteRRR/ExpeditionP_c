using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    internal enum GenerationMethod
    {
        MainTag,
        PlayerRandomTag,
        PlayerWeightedTag, // неиспользуемый метод, чем больше какого-то тега, тем больше у него шанс на появление
        ExpeditionOnly
    }
}
