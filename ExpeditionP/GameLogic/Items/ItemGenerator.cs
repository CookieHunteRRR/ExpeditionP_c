using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    internal class ItemGenerator
    {
        ExpeditionManager ExpeditionManager { get; set; }
        LootTable CurrentLootTable { get; set; }
        GenerationRules GenerationRules { get; init; }
        StringBuilder CurrentFilter { get; init; }

        Dictionary<Tag, int> CurrentPlayerTagsDict { get; init; }
        List<string> ItemIdBuffer { get; init; }

        internal ItemGenerator(ExpeditionManager manager)
        {
            ExpeditionManager = manager;
            CurrentLootTable = manager.CurrentMap.LootTable;
            GenerationRules = new GenerationRules(GenerationPriority.AnyNonRelic, 
                                                manager.CurrentMap.Info.ExpeditionTag, 
                                                GenerationMethod.MainTag);
            ItemIdBuffer = new List<string>();
            CurrentPlayerTagsDict = new Dictionary<Tag, int>();
            CurrentFilter = new StringBuilder();
        }

        void ResetGenerationRules()
        {
            GenerationRules.Priority = GenerationPriority.AnyNonRelic;
            SetExpeditionTag((Tag)ExpeditionManager.CurrentMap.Info.ExpeditionTag);
            GenerationRules.GenerationMethod = GenerationMethod.MainTag;
        }

        void SetLootTable(LootTable table) { CurrentLootTable = table; UpdateFilter(); }
        void SetExpeditionTag(Tag tag)
        {
            if (Constants.expeditionTags.Contains(tag))
            {
                GenerationRules.ExpeditionTag = tag;
            }
            else GenerationRules.ExpeditionTag = Tag.Standart;
        }

        /// <summary>
        /// Изменяет правила генерации и обновляет фильтр. Если передается null - эта переменная не обновляется.
        /// </summary>
        void UpdateGenerationRules(GenerationPriority? priority, Tag? expeditionTag, GenerationMethod? method)
        {
            if (priority != null)
                GenerationRules.Priority = (GenerationPriority)priority;
            if (expeditionTag != null)
                SetExpeditionTag((Tag)expeditionTag);
            if (method != null)
                GenerationRules.GenerationMethod = (GenerationMethod)method;
            UpdateFilter();
        }

        /// <summary>
        /// Обновляет фильтр генерации предметов после изменения правил генерации или луттейбла
        /// </summary>
        void UpdateFilter()
        {
            CurrentFilter.Clear();

            // Добавляем в фильтр предметы, находящиеся в луттейбле
            if (CurrentLootTable.Items.Count > 0 && GenerationRules.Priority != GenerationPriority.Consumable)
            {
                foreach (Item i in CurrentLootTable.Items) 
                    CurrentFilter.Append("id = '" + i.Info.InternalName + "' OR ");
                // отрезаем последние 4 лишних символа (пробел OR пробел)
                CurrentFilter.Remove(CurrentFilter.Length - 4, 4);
                if (CurrentLootTable.LootTableOverridesGeneration) return;
                else
                {
                    CurrentFilter.Insert(0, "(");
                    CurrentFilter.Append(") OR ");
                }
            }

            string itemTypeToGenerate = GetItemTypeForGeneration();

            StringBuilder genMethod = new StringBuilder();
            genMethod.Append($"(itemtype = '{itemTypeToGenerate}' AND " +
                $"(expedition = '{GenerationRules.ExpeditionTag.ToString()}' OR expedition = 'Standart') " +
                $"AND isappearingrandomly = true)");
            // Не добавляем фильтр по мейн тегу сразу, т.к. может быть ситуация, когда мейн тега нет
            Tag? mainTag = GetTagForGeneration();
            if (mainTag != null)
                genMethod.Insert(genMethod.Length - 1, $" AND {mainTag} = true");
            CurrentFilter.Append(genMethod.ToString());
            Program.SendToLog("Generated filter: " + CurrentFilter.ToString());
        }

        /// <summary>
        /// Метод, конвертирующий список сгенерированных айдишников предметов в список непосредственно предметов
        /// </summary>
        List<Item> GetGeneratedItems()
        {
            List<Item> items = new List<Item>();
            foreach (var id in ItemIdBuffer)
            {
                items.Add(ItemHolder.RegisteredItems[id]);
            }

            ItemIdBuffer.Clear();
            CurrentPlayerTagsDict.Clear();

            return items;
        }

        internal List<Item> GenerateLoot(bool isEnemyKilled, LootTable? lootTable = null)
        {
            if (lootTable != null)
                SetLootTable(lootTable);

            Player player = ExpeditionManager.GameInstance.Player;

            // Предварительно проверяем особые случаи
            // Если у игрока нет оружия (даже если есть аксессуары)
            if (player.EquippedWeapons.Count < 1)
            {
                UpdateGenerationRules(GenerationPriority.Weapon, null, GenerationMethod.ExpeditionOnly);
                GenerateItems(1);
                UpdateGenerationRules(GenerationPriority.Accessory, null, null);
                GenerateItems(2);
            }
            // если у игрока нет ни одного аксессуара, но есть оружие
            else if (player.EquippedAccessories.Count < 1)
            {
                UpdateGenerationRules(GenerationPriority.Accessory, null, GenerationMethod.ExpeditionOnly);
                GenerateItems(3);
            }
            else
            {
                // Так как эта часть кода - общий, а не особый случай, то генерировать будем что угодно, что не связано с реликвиями
                UpdateGenerationRules(GenerationPriority.AnyNonRelic, null, GenerationMethod.MainTag);
                GenerateItems(1);
                UpdateGenerationRules(null, null, GenerationMethod.PlayerRandomTag);
                GenerateItems(1);
                UpdateGenerationRules(null, null, GenerationMethod.ExpeditionOnly);
                GenerateItems(1);
            }

            if (isEnemyKilled)
            {
                // 50% шанс на генерацию
                if (Utils.CheckProbability(0.5))
                {
                    UpdateGenerationRules(GenerationPriority.Consumable, Tag.Other, GenerationMethod.ExpeditionOnly);
                    GenerateItems(1);
                }
            }

            return GetGeneratedItems();
        }

        void GenerateItems(int amount)
        {
            if (CurrentPlayerTagsDict.Count > 0)
                UpdatePlayerTagDictionary();

            for (int i = 0; i < amount; i++)
            {
                TryGenerateItem();
            }
        }

        /// <summary>
        /// Основной метод, отвечающий за генерацию предмета. Генерирует предмет на основе заданных правил генерации и установленного луттейбла
        /// </summary>
        void TryGenerateItem(int attempt = 0)
        {
            if (attempt >= Constants.maxGenerationAttempts) return;

            DataTable filteredTable = GetItemPoolFromFilter();
            if (filteredTable.Rows.Count < 1) // Если отфильтрованная таблица пустая
            {
                UpdateGenerationRules(GenerationPriority.AnyNonRelic, null, GenerationMethod.ExpeditionOnly);
                TryGenerateItem(attempt++);
                return;
            }

            List<int> intermediateWeight = new List<int>();
            int totalWeight = 0;

            var filteredRows = filteredTable.Rows;
            if (filteredRows.Count == 1) // попытка в оптимизацию хз, если в таблице один возможный вариант то возвращаем его
            {
                ItemIdBuffer.Add(filteredRows[0]["id"].ToString());
                return;
            }
            foreach (DataRow row in filteredRows)
            {
                Item item = ItemHolder.RegisteredItems[row["id"].ToString()];
                totalWeight += GetCorrectedWeight(item);
                intermediateWeight.Add(totalWeight);
            }
            // сгенерировали рандомный вес
            double generatedWeight = Program.Random.NextDouble() * totalWeight;
            // нашли индекс соответствующей шмотки
            for (int i = 0; i < intermediateWeight.Count; i++)
            {
                if (generatedWeight < intermediateWeight[i])
                {
                    ItemIdBuffer.Add(filteredRows[i]["id"].ToString());
                    return;
                }
            }
        }

        /// <summary>
        /// У разных предметов одной и той же редкости может быть разный вес в зависимости от обстоятельств, поэтому необходим
        /// этот метод чтобы получить точный вес конкретного предмета
        /// </summary>
        int GetCorrectedWeight(Item item)
        {
            int weight = item.Info.Weight;
            if (CurrentLootTable.Items.Contains(item)) return weight * 3; // если предмет есть в луттейбле
            if (item.Info.ExpeditionTag == Tag.Standart) return weight; // ПРИ ЭТОМ В SetRarity У ПРЕДМЕТОВ УБРАТЬ ИЗМЕНЕНИЕ ВЕСОВ!
            return weight * 2; // дефолтный вес для шмотки из не стандартной экспедиции
        }

        DataTable GetItemPoolFromFilter()
        {
            DataView filteredItems = new DataView(ItemHolder.ItemTable);

            // отфильтровали нужные предметы
            filteredItems.RowFilter = CurrentFilter.ToString();
            DataTable itemPool = filteredItems.ToTable();

            // А теперь исключаем уже сгенерированные предметы и имеющиеся у игрока предметы
            foreach (var item in ExpeditionManager.GameInstance.Player.GetAllEquipment())
            {
                string itemID = item.Info.InternalName;
                DataRow[] itemToExclude = itemPool.Select("id = '" + item.Info.InternalName + "'");
                if (itemToExclude.Length > 0)
                {
                    itemPool.Rows.Remove(itemToExclude[0]);
                    //Program.SendToLog("Found " + itemID + " and removed it from pool");
                }
            }
            foreach (var itemId in ItemIdBuffer)
            {
                DataRow[] itemToExclude = itemPool.Select($"id = '{itemId}'");
                if (itemToExclude.Length > 0)
                {
                    itemPool.Rows.Remove(itemToExclude[0]);
                    //Program.SendToLog("Found " + itemID + " and removed it from pool");
                }
            }

            return itemPool;
        }

        string GetItemTypeForGeneration()
        {
            switch (GenerationRules.Priority)
            {
                case GenerationPriority.AnyNonRelic:
                    string typeToReturn = (Program.Random.Next(2) > 0) ? "weapon" : "accessory";
                    return typeToReturn;
                case GenerationPriority.Weapon:
                case GenerationPriority.Accessory:
                case GenerationPriority.Consumable:
                    return GenerationRules.Priority.ToString().ToLower();
                default:
                    throw new Exception($"Не реализовано возвращение типа предмета для приоритета {GenerationRules.Priority}");
            }
        }

        Tag? GetTagForGeneration()
        {
            if (GenerationRules.GenerationMethod == GenerationMethod.ExpeditionOnly) return null;
            var tagDict = GenerationRules.PlayerTagsDict;
            if (tagDict.Count < 1) return null;
            switch (GenerationRules.GenerationMethod)
            {
                case GenerationMethod.MainTag:
                    // Берем количество упоминаний максимально часто упомянутого тега
                    int mainTagWeight = tagDict.Values.Max();
                    // Проверяем на наличие идентичного веса + сохраняем какие именно теги имеют такой вес
                    List<Tag> possibleMainTags = new List<Tag>();
                    for (int i = 0; i < tagDict.Values.Count; i++)
                    {
                        var pair = tagDict.ElementAt(i);
                        if (pair.Value == mainTagWeight)
                            possibleMainTags.Add(pair.Key);
                    }
                    // Определяем основной тег, по которому будем генерировать шмот
                    // Если наибольший вес только у одного тега - берется он, иначе берется рандомный
                    Tag mainTag = possibleMainTags[0];
                    if (possibleMainTags.Count > 1)
                        mainTag = possibleMainTags[Program.Random.Next(possibleMainTags.Count)];
                    return mainTag;
                case GenerationMethod.PlayerWeightedTag:
                    List<int> intermediateWeight = new List<int>();
                    int totalWeight = 0;
                    foreach (var weight in tagDict.Values)
                    {
                        totalWeight += weight;
                        intermediateWeight.Add(totalWeight);
                    }
                    double generatedWeight = Program.Random.NextDouble() * totalWeight;
                    int indexToGet = 0;
                    for (int i = 0; i < intermediateWeight.Count; i++)
                    {
                        if (generatedWeight < intermediateWeight[i])
                        {
                            indexToGet = i;
                            break;
                        }
                    }
                    return tagDict.ElementAt(indexToGet).Key;
                case GenerationMethod.PlayerRandomTag:
                    int randomIndex = Program.Random.Next(tagDict.Count);
                    return tagDict.ElementAt(randomIndex).Key;
            }
            return null;
        }

        void UpdatePlayerTagDictionary()
        {
            Player player = ExpeditionManager.GameInstance.Player;

            // Генерируем "словарь тегов" предметов, которые у нас экипированы и находим среди них самый частый тег (основной тег)
            foreach (Accessory item in player.EquippedAccessories)
                AddTagsToTagPool(item);
            if (player.CurrentEquippedWeapon != Constants.GetFistsWeapon())
                AddTagsToTagPool(player.CurrentEquippedWeapon);
        }

        void AddTagsToTagPool(Item item)
        {
            foreach (Tag tag in item.Tags)
            {
                if (CurrentPlayerTagsDict.ContainsKey(tag))
                {
                    CurrentPlayerTagsDict[tag]++;
                    continue;
                }
                CurrentPlayerTagsDict.Add(tag, 1);
            }
        }
    }
}
