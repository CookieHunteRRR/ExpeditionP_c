using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.AttackList;

namespace ExpeditionP.GameLogic.Items.Instances.Weapons
{
    /// <summary>
    /// Особый вид милишного оружия для коммонок и рарок. Т.к. никаких особых атак у них нет и отличаются они лишь статами
    /// будет использоваться этот класс. Другой вопрос в том как проверять имеется ли подобное оружие у игрока, потому что они все
    /// будут BaseMeleeWeapon
    /// </summary>
    internal class GenericMeleeWeapon : Weapon
    {
        internal GenericMeleeWeapon() : base("weapon_genericmelee")
        {
            Info.Name = "УБО ТЕСТОВОЕ"; //Универсальное ближнее оружие
            Attack = new Attack_PlayerGeneric(1, 1, DamageType.Physical,
                "Вы наносите {0} урона");

            SetRarity(Tag.Common);
            Info.ExpeditionTag = Tag.Other;
        }
    }
}
