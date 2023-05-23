using ExpeditionP.GameLogic.Items;
using ExpeditionP.GameLogic.Items.Instances.Consumables;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Holders
{
    public static class ItemHolder
    {
        internal static Dictionary<string, Item> RegisteredItems { get; } = new Dictionary<string, Item>();
        internal static DataTable ItemTable { get; } = new DataTable();
        static int ColumnsBeforeTags { get; set; }

        internal static void RegisterItems()
        {
            Program.SendToLog("[ItemHolder] Началась загрузка предметов");
            DataColumn column;

            // Создаем колонку айдишников предметов
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;
            ItemTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "itemtype";
            column.ReadOnly = true;
            ItemTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rarity";
            column.ReadOnly = true;
            ItemTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "expedition";
            column.ReadOnly = true;
            ItemTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "isappearingrandomly";
            column.ReadOnly = true;
            ItemTable.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[] { ItemTable.Columns["id"] };
            ItemTable.PrimaryKey = PrimaryKeyColumns;

            ColumnsBeforeTags = ItemTable.Columns.Count;

            RegisterItemsN();
        }

        // по хорошему бы переименовать метод...
        static void RegisterItemsN()
        {
            // Регистрируем оружия
            string itemsDir = "ExpeditionP.GameLogic.Items.Instances";

            // Регистрируем предметы из каждой экспедиции
            foreach (var expeditionName in Constants.expeditionTags)
            {
                string expNameAsStr = expeditionName.ToString();
                // Проверяем, что папки с названием этой экспедиции действительно существуют
                string weaponExpDir = String.Format("{0}.{1}.{2}", itemsDir, "Weapons", expNameAsStr);
                var weaponTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == weaponExpDir);
                int weaponCounter = 0;
                foreach (var item in weaponTypes)
                {
                    if (RegisterItem(item))
                        weaponCounter++;
                }
                int accCounter = 0;
                string accExpDir = String.Format("{0}.{1}.{2}", itemsDir, "Accessories", expNameAsStr);
                var accTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == accExpDir);
                foreach (var item in accTypes)
                {
                    if (RegisterItem(item))
                        accCounter++;
                }
                int consumableCounter = 0;
                string consumableExpDir = String.Format("{0}.{1}.{2}", itemsDir, "Consumables", expNameAsStr);
                var consumableTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == consumableExpDir);
                foreach (var item in consumableTypes)
                {
                    if (RegisterItem(item))
                        consumableCounter++;
                }
                Program.SendToLog(String.Format("[ItemHolder] Зарегистрировано {0} предметов экспедиции {1} " +
                    "({2} оружий, {3} аксессуаров, {4} расходников)", weaponCounter + accCounter, expNameAsStr, weaponCounter, accCounter, consumableCounter));
            }
            Program.SendToLog("[ItemHolder] Успешно зарегистрировано " + RegisteredItems.Count + " предметов");
        }

        // ура победа
        static bool RegisterItem(Type type)
        {
            object item = Activator.CreateInstance(type, true);
            // Так как какая нибудь атака у ножа-бабочки тоже находится в неймспейсе с оружками,
            // то нужно делать проверку является ли регистрируемый предмет вообще предметом
            Item? toRegister = null;
            try
            {
                toRegister = (Item)item;
            }
            catch (InvalidCastException)
            {
                Program.SendToLog("[ItemHolder] Алгоритм попытался зарегистрировать " + item.GetType() + " как предмет");
                return false;
            }

            if (toRegister is not null)
            {
                RegisteredItems.Add(toRegister.Info.InternalName, toRegister);
                // Проверяем на проблемы в предметах
                if (toRegister as Weapon is not null)
                {
                    var check = (Weapon)toRegister;
                    if (check.Attack.MaxDamage < check.Attack.MinDamage)
                        Program.SendToLog("[ItemHolder] Оружие " + toRegister.Info.InternalName + " имеет макс. урон меньше минимального");
                    if (check.Tags.Count < 1)
                        Program.SendToLog("[ItemHolder] Оружие " + toRegister.Info.InternalName + " не имеет тегов");
                }
                DataRow row = ItemTable.NewRow();
                row["id"] = toRegister.Info.InternalName;
                row["itemtype"] = GetItemType(toRegister);
                row["rarity"] = toRegister.RarityTag.ToString();
                row["expedition"] = toRegister.Info.ExpeditionTag.ToString();
                row["isappearingrandomly"] = toRegister.Info.IsAppearingRandomly;
                // Регистрируем отсутствующие в таблице теги
                foreach (Tag tag in toRegister.Tags)
                {
                    if (!ItemTable.Columns.Contains(tag.ToString()))
                    {
                        DataColumn column = new DataColumn();
                        column.DataType = System.Type.GetType("System.Boolean");
                        column.ColumnName = tag.ToString();
                        column.ReadOnly = false;
                        ItemTable.Columns.Add(column);
                        // Для каждого DataRow уже добавленного в таблицу без этого тега, установить этот тег как false
                        // (Ну потому что очевидно что если предыдущие предметы не добавили этот тег в таблицу, значит у них этого тега и нет)
                        foreach (DataRow addedRow in ItemTable.Rows)
                            addedRow[column] = false;
                    }
                }
                // Добавляем предмет в таблицу
                // Если помимо id, rarity, exp есть колонки с тегами
                if (ItemTable.Columns.Count > ColumnsBeforeTags)
                {
                    for (int i = ColumnsBeforeTags; i < ItemTable.Columns.Count; i++)
                    {
                        if (toRegister.Tags.Contains((Tag)Enum.Parse(typeof(Tag), ItemTable.Columns[i].ColumnName)))
                            row[ItemTable.Columns[i]] = true;
                        else row[ItemTable.Columns[i]] = false;
                    }
                }

                ItemTable.Rows.Add(row);
                return true;
            }
            return false;
        }

        static string GetItemType(Item item)
        {
            try
            {
                if ((Weapon)item != null)
                    return "weapon";
            } catch { }
            try
            {
                if ((Accessory)item != null)
                    return "accessory";
            } catch { }
            try
            {
                if ((Consumable)item != null)
                    return "consumable";
            } catch { }

            throw new Exception($"Неизвестный тип при регистрации предмета: {item.GetType()}");
        }
    }
}
