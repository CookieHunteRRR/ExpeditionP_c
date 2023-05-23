using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    /*
     * Себе на будущее
     * чтобы получить нужный енум по порядковому номеру: MyEnum myEnum = (MyEnum)myInt;
     * чтобы получить нужный енум по названию: MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
     */

    internal enum Tag
    {
        // Rarity tags
        Common,
        Uncommon,
        Epic,
        Legendary,
        Relic,
        // Expedition tags
        Other,
        Standart,
        Mythological,
        // Entity tags
        Bloodless,
        Fleshless,
        // Damage type tags
        Physical,
        Magical,
        Pure,
        // Item tags
        Melee,
        Ranged,
        Offensive,
        Defensive,
        Critical,
        Health,
        Defense,
        Evasion,
        CritChance,
        CritDamage,
        Mitigation,
        Annulment,
        Amplification,
        Heal,
        Vampiric
    }
}
