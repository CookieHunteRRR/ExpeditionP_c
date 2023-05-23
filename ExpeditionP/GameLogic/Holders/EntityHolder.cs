using ExpeditionP.GameLogic.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Holders
{
    public static class EntityHolder
    {
        // id: *mob/boss*_*expedition*_*name*
        internal static Dictionary<string, Entity> RegisteredEntities { get; } = new Dictionary<string, Entity>();
        internal static DataTable EntityTable { get; } = new DataTable();

        internal static void RegisterEntities()
        {
            // Потом по нормальному сделать через прохождение по всем файлам в папке
            //RegisteredEntities.Add("mob_standart_zombie", new Mob_Other_Zombie());
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;
            EntityTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "entitytype";
            column.ReadOnly = true;
            EntityTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "expedition";
            column.ReadOnly = true;
            EntityTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "weight";
            column.ReadOnly = true;
            EntityTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "isappearingrandomly";
            column.ReadOnly = true;
            EntityTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "mindiffmp";
            column.ReadOnly = true;
            EntityTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "maxdiffmp";
            column.ReadOnly = true;
            EntityTable.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[] { EntityTable.Columns["id"] };
            EntityTable.PrimaryKey = PrimaryKeyColumns;

            RegisterEntity(new Entities.Instances.Mobs.Standart.ZombieMob());
            RegisterEntity(new Entities.Instances.Mobs.Standart.VampireMob());
            RegisterEntity(new Entities.Instances.Mobs.Standart.SkeletonMob());
            RegisterEntity(new Entities.Instances.Mobs.Standart.BanditMob());
            RegisterEntity(new Entities.Instances.Mobs.Standart.MercenaryMob());
            RegisterEntity(new Entities.Instances.Mobs.Standart.WerewolfMob());
            RegisterEntity(new Entities.Instances.Mobs.Standart.SlimeMob());
            RegisterEntity(new Entities.Instances.Bosses.Standart.HeadBanditBoss());
        }

        static void RegisterEntity(Entity entity)
        {
            Mob mob = entity as Mob;
            if (mob is null)
            {
                Program.SendToLog("[EntityHolder] Ошибка при регистрации Entity " + entity.GetType().Name + ". Регистрация этого Entity невозможна.");
                return;
            }

            DataRow row = EntityTable.NewRow();
            if (mob as Boss is not null)
            {
                Boss boss = entity as Boss;
                row["entitytype"] = "Boss";
                RegisteredEntities.Add(boss.Info.InternalName, boss);
            }
            else
            {
                try
                {
                    var attack = mob.AI.GetAttackByIndex(0);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException("[EntityHolder] ВНИМАНИЕ! Моб " + mob.GetType().Name.ToString() + " не имеет атак", ex);
                }

                if (mob.LootTable.Items.Count < 1)
                {
                    Program.SendToLog("[EntityHolder] Предупреждение. Моб " + mob.GetType().Name.ToString() + " имеет пустой луттейбл.");
                }
                row["entitytype"] = "Mob";
                RegisteredEntities.Add(mob.Info.InternalName, mob);
            }

            row["id"] = mob.Info.InternalName;
            row["expedition"] = mob.Info.ExpeditionTag;
            row["weight"] = mob.Info.Weight;
            row["isappearingrandomly"] = mob.Info.IsAppearingRandomly;
            row["mindiffmp"] = mob.Info.MinDiffAcquirable;
            row["maxdiffmp"] = mob.Info.MaxDiffAcquirable;
            EntityTable.Rows.Add(row);
        }
    }
}
