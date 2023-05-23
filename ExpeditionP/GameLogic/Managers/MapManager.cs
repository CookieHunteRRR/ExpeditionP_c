using ExpeditionP.GameLogic.Holders;
using ExpeditionP.GameLogic.Maps;
using ExpeditionP.GameLogic.Maps.Expeditions;
using ExpeditionP.GameLogic.Maps.Expeditions.Mythological;
using ExpeditionP.GameLogic.Maps.Expeditions.Other;
using ExpeditionP.GameLogic.Maps.Expeditions.Standart;
using ExpeditionP.GameLogic.Maps.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Managers
{
    internal class MapManager
    {
        internal MapGenerator MapGenerator { get; init; }
        internal Dictionary<string, Map> LoadedMaps { get; init; }

        /// <summary>
        /// Словарь с ключом, отвечающим за внутреннее наименование карты в формате string и значением - кол-вом пройденных экспедиций (int)
        /// </summary>
        internal Dictionary<string, int> CompletedExpeditions { get; init; }

        internal MapManager()
        {
            MapGenerator = new MapGenerator();
            LoadedMaps = new Dictionary<string, Map>();
            CompletedExpeditions = new Dictionary<string, int>();
        }

        internal void LoadMap(Map map)
        {
            if (LoadedMaps.ContainsKey(map.Info.InternalName))
            {
                Program.SendToLog("[MapManager] Карта с внутренним ID " + map.Info.InternalName + " уже загружена, пропуск");
                return;
            }
            if (map.Info.InternalName == Constants.defaultInternalMapName)
            {
                Program.SendToLog("[MapManager] Предупреждение: добавляется карта без измененного внутреннего ID карты");
            }
            LoadedMaps.Add(map.Info.InternalName, map);
        }

        internal void LoadAllMaps()
        {
            LoadMap(new TutorialMap());
            //LoadMap(new TrainingFieldsMap());
            //LoadMap(new VolatileMap());
            LoadMap(new StandartEasyMap());
        }
    }
}
