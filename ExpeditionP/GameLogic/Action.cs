using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic
{
    internal class Action
    {
        internal ActionType Type { get; init; }
        internal int Value { get; init; }

        /// <summary>
        /// Вполне возможно что ошибка прописывать все здесь
        /// Возможные ключи:
        /// doesntUseTurn - 0..1 (0 - действие тратит ход, 1 - действие не тратит ход)
        /// </summary>
        internal Dictionary<string, int> SpecialValues { get; init; }

        internal Action(ActionType type, int value)
        {
            Type = type;
            Value = value;
            SpecialValues = new Dictionary<string, int>();
        }

        internal Action(ActionType type, int value, Dictionary<string, int> specialValues)
        {
            Type = type;
            Value = value;
            SpecialValues = specialValues;
        }
    }
}
