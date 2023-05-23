using ExpeditionP.GameLogic.Entities;
using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Items
{
    internal class LootTable
    {
        internal List<Item> Items { get; set; }
        internal bool LootTableOverridesGeneration { get; set; }
        internal LootTable() { Items = new List<Item>(); LootTableOverridesGeneration = false; }

        internal void AddItem(Item item) { Items.Add(item); }
        internal void RemoveItem(Item item) { Items.Remove(item); }
    }
}