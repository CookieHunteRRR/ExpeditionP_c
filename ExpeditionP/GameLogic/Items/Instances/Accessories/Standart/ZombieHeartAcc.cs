using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class ZombieHeartAcc : UsableAccessory
    {
        static readonly string thisAccId = "acc_zombieheart";

        internal ZombieHeartAcc()
        {
            Info.InternalName = thisAccId;
            Info.Name = "Сердце зомби";

            Ability = new Ability_ConsumeHeart();
            HiddenStats.Heal = 5;
            SpecialDescription = $"Вы регенерируете {HiddenStats.Heal} здоровья в конце хода";

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Uncommon);
            Info.SetRandomAppearance(false);

            Tags.Add(Tag.Defensive);
            Tags.Add(Tag.Health);
            Tags.Add(Tag.Heal);
        }

        class Ability_ConsumeHeart : Ability
        {
            protected static readonly double healPercentage = 0.5;

            internal Ability_ConsumeHeart() : base(0, 0) 
            {
                Info.InternalName = "ability_consumeheart";
                Info.Name = "Съесть сердце";
                Info.Description = $"Вы съедаете сердце зомби, восстанавливая {healPercentage * 100}% своего максимального здоровья";

                UsableOutOfBattle = true;
            }

            internal override void Activate(ExpeditionManager manager, Entity caster)
            {
                base.Activate(manager, caster);

                Player player = manager.GameInstance.Player;
                double playerHealth = player.BattleStats.CurrentEntityStats.Health * healPercentage;
                caster.ChangeCurrentHealth(Utils.Round(playerHealth));

                player.RemoveAccessoryById(thisAccId);
            }
        }
    }
}
