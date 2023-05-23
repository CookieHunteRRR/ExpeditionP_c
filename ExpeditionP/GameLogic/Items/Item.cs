using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.Items.Instances.Accessories;
using ExpeditionP.GameLogic.Items.Instances.Consumables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    internal class Item : EventSubject
    {
        internal SpawnableInfo Info { get; set; }
        internal string? SpecialDescription { get; set; }
        internal Tag RarityTag { get; set; }
        internal List<Tag> Tags { get; }

        internal Item(string internalName, string? name = null, string? description = null)
        {
            Info = new SpawnableInfo();
            Tags = new List<Tag>();

            Info.InternalName = internalName;
            Info.Name = name;
            Info.Description = description;
            Info.SetRandomAppearance(true);
        }

        internal void SetRarity(Tag rarity)
        {
            if (!Constants.rarityTags.Contains(rarity))
            {
                RarityTag = Tag.Common;
                Info.SetWeight(0);
                return;
            }
            RarityTag = rarity;
            int weight = GetDefaultWeight();
            Info.SetWeight((Info.ExpeditionTag == Tag.Standart) ? weight / 2 : weight);
        }

        internal void AddTags(Tag[] tags) { Tags.AddRange(tags); }

        internal string GetTagsAsString()
        {
            string toReturn = string.Empty;
            foreach (Tag tag in Tags)
            {
                toReturn += tag.ToString() + ", ";
            }
            if (toReturn.Length > 0) toReturn.Remove(toReturn.Length - 2, 2);
            return toReturn;
        }

        internal string GetItemStatsAsString()
        {
            StringBuilder sb = new StringBuilder();
            string toReturn = string.Empty;
            sb.Append($"{((this.Info.Name is null) ? this.Info.InternalName : this.Info.Name)}\r\n");
            sb.Append($"Редкость: {this.RarityTag.ToString()}\r\n");
            sb.Append($"Экспедиция: {this.Info.ExpeditionTag.ToString()}\r\n");
            sb.Append($"Теги: {this.GetTagsAsString()}");

            if (this.Info.Description != null)
            {
                sb.Append($"\r\n\n{this.Info.Description}");
            }

            if (this is Weapon)
            {
                Weapon item = (Weapon)this;
                sb.Append($"\r\n\nУрон: {item.Attack.MinDamage}-{item.Attack.MaxDamage}\r\n");
                sb.Append($"Тип урона: {item.Attack.DamageType}");

                if (item.Stats != null)
                    sb.Append($"\r\n\n{item.Stats.GetStatsAsString()}");
            }
            else if (this is Accessory)
            {
                Accessory item = (Accessory)this;
                if (item is UsableAccessory)
                {
                    sb.Append($"\r\n\n{((UsableAccessory)item).Ability.GetAbilityInfo()}");
                }
                sb.Append($"\r\n\n{item.Stats.GetStatsAsString()}");
            }
            else if (this is Consumable)
            {
                sb.Clear();
                sb.Append($"{this.Info.Name}\r\n\n");
                sb.Append(this.Info.Description);
            }
            else { throw new Exception($"Попытка вывода информации о неизвестном типе {this.GetType()}"); }

            if (this.SpecialDescription != null)
                sb.Append($"\n{this.SpecialDescription}");

            return sb.ToString();
        }

        int GetDefaultWeight()
        {
            Tag rarityTag = this.RarityTag;
            if (!Constants.rarityTags.Contains(rarityTag)) return 0;
            switch (rarityTag)
            {
                case Tag.Common:
                    return Constants.rarityWeight[0];
                case Tag.Uncommon:
                    return Constants.rarityWeight[1];
                case Tag.Epic:
                    return Constants.rarityWeight[2];
                case Tag.Legendary:
                    return Constants.rarityWeight[3];
                case Tag.Relic:
                    return Constants.rarityWeight[4];
                default:
                    return 0;
            }
        }
    }
}