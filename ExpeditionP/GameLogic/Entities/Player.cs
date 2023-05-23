using ExpeditionP.GameLogic.EventHandling;
using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities
{
    internal class Player : Entity
    {
        internal string Name { get; set; }
        internal List<Weapon> EquippedWeapons { get; init; }
        internal Weapon CurrentEquippedWeapon { get; set; }
        internal List<Accessory> EquippedAccessories { get; init; }

        internal Player()
        {
            EquippedWeapons = new List<Weapon>();
            CurrentEquippedWeapon = Constants.GetFistsWeapon();
            EquippedAccessories = new List<Accessory>();

            Name = "Игрок";
        }

        internal override string GetName()
        {
            return Name;
        }

        internal List<Item> GetAllEquipment()
        {
            List<Item> toReturn = new List<Item>();
            toReturn.AddRange(EquippedWeapons);
            toReturn.AddRange(EquippedAccessories);
            return toReturn;
        }

        // При заходе на экспедицию
        internal void SetupToExpedition()
        {
            CurrentEquippedWeapon = (EquippedWeapons.Count < 1) ? Constants.GetFistsWeapon() : EquippedWeapons[0];
            base.BattleStats.CurrentEffects.Clear();
            RecalculateStats();
            BattleStats.CurrentHealth = BattleStats.CurrentEntityStats.Health;
            BattleStats.CurrentMana = BattleStats.CurrentEntityStats.Mana;
        }

        // При выходе с экспедиции
        internal void ClearEquipment()
        {
            EquippedAccessories.Clear();
            EquippedWeapons.Clear();
            // здесь был код который теперь в SetupToExpedition, если что то в конце экспедиции поломается то наверное из-за этого?
        }

        internal void EquipWeapon(int index, ExpeditionManager? manager)
        {
            CurrentEquippedWeapon = EquippedWeapons[index];
            RecalculateStats();
            if (manager is null) return;
            if (manager.BattleManager is not null)
                EventManager.WeaponEquipEvent.Invoke(manager);
        }

        internal void UnequipWeapon(ExpeditionManager manager)
        {
            // Сначала вызывается ивент, затем меняется оружие
            if (manager.BattleManager is not null)
                EventManager.WeaponUnequipEvent.Invoke(manager);
            // Добавить ивент при анеквипе
            CurrentEquippedWeapon = Constants.GetFistsWeapon();
            RecalculateStats();
        }

        /// <summary>
        /// Добавляет оружие, если для него есть место
        /// </summary>
        internal void AddWeapon(Weapon weapon)
        {
            EquippedWeapons.Add(weapon);
            if (EquippedWeapons.Count == 1)
            {
                if (Program.Expedition.ExpeditionManager != null)
                {
                    Program.Expedition.ExpeditionManager.SendToLog($"Вы взяли в руки {EquippedWeapons[0].Info.Name}");
                }
                EquipWeapon(0, null);
            }
            weapon.RegisterEvents();
        }

        /// <summary>
        /// Заменяет имеющееся оружие в выбранном слоте на новое
        /// </summary>
        internal void AddWeapon(Weapon weapon, int index)
        {
            EquippedWeapons[index].UnregisterEvents();
            EquippedWeapons.RemoveAt(index);
            EquippedWeapons.Insert(index, weapon);
            weapon.RegisterEvents();
        }

        /// <summary>
        /// Добавляет аксессуар, если для него есть место
        /// </summary>
        internal void AddAccessory(Accessory acc) 
        { 
            EquippedAccessories.Add(acc); 
            RecalculateStats();
            acc.RegisterEvents();
        }

        /// <summary>
        /// Заменяет имеющийся аксессуар в выбранном слоте на новый
        /// </summary>
        internal void AddAccessory(Accessory acc, int index) 
        {
            EquippedAccessories[index].UnregisterEvents();
            EquippedAccessories.RemoveAt(index); 
            EquippedAccessories.Insert(index, acc); 
            RecalculateStats();
            acc.RegisterEvents();
        }

        /// <summary>
        /// Пока используется ТОЛЬКО для удаления аксессуаров при использовании абилок
        /// </summary>
        internal void RemoveAccessoryById(string id)
        {
            for (int i = 0; i < EquippedAccessories.Count; i++)
            {
                var acc = EquippedAccessories[i];
                if (acc.Info.InternalName == id)
                {
                    acc.UnregisterEvents();
                    EquippedAccessories.RemoveAt(i);
                    RecalculateStats();
                    return;
                }
            }
        }
    }
}
