using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic
{
    internal class Constants
    {
        // Weapons
        internal static readonly string defaultWeaponName = "Unnamed Weapon";
        internal static readonly double defaultWeaponDamage = 0;
        internal static readonly DamageType defaultDamageType = DamageType.Physical;
        internal static readonly double defaultWeaponCC = 1;
        internal static readonly double defaultWeaponCD = 1;

        internal static Weapon GetFistsWeapon() { return (Weapon)ItemHolder.RegisteredItems["weapon_fists"]; }
        // Stats
        internal static readonly double minimalDefenseMultiplier = 0.1;
        internal static readonly double maximumDefenseMultiplier = 2;
        internal static readonly int minimumHealth = 1;
        internal static readonly int minimumMana = 1;
        // Default Stats
        internal static readonly EntityStats defaultEntityStats = new EntityStats()
        {
            Health = 100,
            Defense = 0,
            Evasion = 0,
            CritChance = 5,
            CritDamage = 75,
            Mana = 100,
            Mitigation = 0,
            Annulment = 0,
            Amplification = 0
        };
        internal static readonly double defaultFleeSuccessChance = 0.5;

        // Stat Names
        internal static readonly string statHealthName = "Здоровье";
        internal static readonly string statDefenseName = "Защита";
        internal static readonly string statEvasionName = "Уклонение";
        internal static readonly string statCritChanceName = "Крит. шанс";
        internal static readonly string statCritDamageName = "Крит. урон";
        internal static readonly string statManaName = "Мана";
        internal static readonly string statMitigationName = "Подавление";
        internal static readonly string statAnnulmentName = "Аннулирование";
        internal static readonly string statAmplificationName = "Усиление";
        internal static readonly string statHealName = "Регенерация";
        // Button Texts
        internal static readonly string unavailableText = "-";
        internal static readonly string expeditionBtnEquipWeaponText = "Экипировать оружие";
        internal static readonly string expeditionBtnUnequipWeaponText = "Убрать оружие из рук";
        internal static readonly string expeditionBtnSwapWeaponText = "Заменить оружие";
        internal static readonly string expeditionBtnInteractText = "Взаимодействовать";
        internal static readonly string expeditionBtnMoveText = "Идти вперед";
        internal static readonly string expeditionBtnUseAccText = "Использовать способность";
        internal static readonly string expeditionBtnUseAccOutOfBattleText = "Нельзя использовать вне боя";
        internal static readonly string expeditionBtnAttackText = "Атаковать";
        internal static readonly string expeditionBtnFleeText = "Сбежать";

        internal static readonly string replaceitemBtnCancel = "Не брать";
        internal static readonly string replaceitemBtnReplace = "Заменить";

        internal static readonly string itempickBtnSkip = "Пропустить";
        internal static readonly string itempickBtnChoose = "Выбрать";
        // Other
        internal static readonly string noEquippedItemInHideoutText = "Ничего";
        internal static readonly string evasionSuccessfulMessage = "УКЛОНЕНИЕ";
        internal static readonly string critSuccessfulMessage = "КРИТИЧЕСКИЙ УДАР";
        internal static readonly string defaultInternalMapName = "newMap";
        internal static readonly string defaultEnemyEncounterMessage = "Вы обнаружили противника";
        internal static readonly int maximumEquippedWeapons = 4;
        internal static readonly int maximumEquippedAccessories = 5;
        internal static readonly HashSet<Tag> rarityTags = new HashSet<Tag>() {
            Tag.Common, Tag.Uncommon, Tag.Epic, Tag.Legendary, Tag.Relic };
        internal static readonly int[] rarityWeight = new int[] { 2000, 1000, 500, 250, 0 };
        internal static readonly HashSet<Tag> expeditionTags = new HashSet<Tag>() {
            Tag.Other, Tag.Standart, Tag.Mythological };
        internal static readonly int defaultItemsToGenerate = 3;
        internal static readonly int maxGenerationAttempts = 2;
    }
}
