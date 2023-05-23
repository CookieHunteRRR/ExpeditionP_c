using ExpeditionP.GameLogic.BattleLogic;
using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpeditionP.GameLogic.BattleLogic.Effects.EffectList;

namespace ExpeditionP.GameLogic.Items.Instances.Accessories.Standart
{
    internal class VampiricFangAcc : UsableAccessory
    {
        internal VampiricFangAcc()
        {
            Info.InternalName = "acc_vampiricfang";
            Info.Name = "Клыки вампира";

            Ability = new Ability_ActivateBloodlust();
            HiddenStats.Vampirism = 12;
            SpecialDescription = $"Увеличивает вампиризм на {HiddenStats.Vampirism}%";

            Info.ExpeditionTag = Tag.Standart;
            SetRarity(Tag.Uncommon);
            Info.SetRandomAppearance(false);

            Tags.Add(Tag.Offensive);
            Tags.Add(Tag.Health);
            Tags.Add(Tag.Vampiric);
        }

        class Ability_ActivateBloodlust : Ability
        {
            protected static readonly int effectPower = 60;

            internal Ability_ActivateBloodlust() : base(8, 0, 0, false)
            {
                Info.InternalName = "ability_bloodlust";
                Info.Name = "Жажда крови";
                Info.Description = $"В вас пробуждается неестественная жажда крови, атаки под этим эффектом имеют {effectPower}% вампиризма";

                CooldownResetOnBattleEnd = false;
            }

            internal override void Activate(ExpeditionManager manager, Entity caster)
            {
                base.Activate(manager, caster);

                Player player = manager.GameInstance.Player;
                player.BattleStats.ApplyEffect(new Effect_Buff_Bloodlust(effectPower));
                manager.SendToLog("Вы ощущаете жажду крови");
            }
        }
    }
}
