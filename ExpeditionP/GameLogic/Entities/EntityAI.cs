using ExpeditionP.GameLogic.BattleLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionP.GameLogic.Entities
{
    /// <summary>
    /// Класс, отвечающий за хранение атак и абилок, которые способен совершить моб, а также за 
    /// получение BattleManager'ом следующего действия моба
    /// </summary>
    internal class EntityAI
    {
        List<Attack> AttackList { get; set; }
        List<Ability> AbilityList { get; set; }
        List<EntityBehavior> BehaviorList { get; set; }
        // -1 означает что поведения у Entity нет (и его нужно создать)
        // 0 означает что как минимум одно поведение в BehaviorList зарегистрировано
        // >0 означает соответствующий индекс в этом листе
        int CurrentBehavior { get; set; }

        // Т.е. абилки, которые имеют UsedDirectly = true
        List<int> UsableAbilitiesInCurrentBehavior { get; set; }

        // Предположим, произошло 2 триггера за ход. Если они произойдут одновременно неизвестно что может произойти
        // Соответственно триггеры сначала пойдут в "очередь действий", а затем GetNextAction() возьмет отсюда первое зашедшее действие
        internal Queue<Action> QueuedActions { get; set; }

        internal EntityAI()
        {
            AttackList = new List<Attack>();
            AbilityList = new List<Ability>();
            BehaviorList = new List<EntityBehavior>();
            QueuedActions = new Queue<Action>();
            UsableAbilitiesInCurrentBehavior = new List<int>();

            CurrentBehavior = -1;
        }

        // Добавляет новое действие в запланированные
        internal void DecideNextAction()
        {
            // Сразу проверяем нужно ли создавать дефолтное поведение
            // Это делается именно во время боя, так как при создании класса еще не добавлены все атаки и абилки
            if (CurrentBehavior < 0) AddDefaultBehavior();

            // Проверка на то, имеются ли уже запланированные действия. Если имеются - планировать новое действие не нужно.
            if (QueuedActions.Count > 0) return;

            // Ниже непосредственно логика добавления нового действия
            // Как говорится subject to change, т.к. я возможно добавлю весовую систему определения атаковать мобу или юзать абилку
            // Прямо сейчас используется старая система с 50/50

            // 50/50 выпадет атака или юз абилки
            ActionType actionType = ActionType.Attack;
            int value = 0;
            if (Program.Random.Next(2) > 0) // Если 1, регаем абилку
            {
                foreach (var abilityIndex in UsableAbilitiesInCurrentBehavior)
                {
                    // Если есть хоть одна абилка без кд
                    if (AbilityList[abilityIndex].AvailableIn == 0)
                    {
                        actionType = ActionType.Ability;
                        break;
                    }
                }
            }
            // Если 0 или не нашлось подходящей абилки - в actionType уже указана атака
            if (actionType == ActionType.Attack)
            {
                // Определяем, какую именно атаку будет юзать моб
                if (BehaviorList[CurrentBehavior].AbilityIndexes.Count > 1)
                {
                    value = Program.Random.Next(BehaviorList[CurrentBehavior].AbilityIndexes.Count);
                }
            }

            // Создаем собственно действие
            var action = new Action(actionType, value);
            // Добавляем его в очередь
            QueuedActions.Enqueue(action);
        }

        internal Attack GetAttackByIndex(int index) { return AttackList[index]; }
        internal Ability GetAbilityByIndex(int index) { return AbilityList[index]; }

        // Снижает кулдауны каждой абилки актуального поведения на *amount* ходов
        internal void ReduceAbilityCooldowns(int amount = 1)
        {
            // Если нет конкретного поведения
            if (CurrentBehavior < 0)
            {
                foreach (var ability in AbilityList)
                {
                    if (ability.AvailableIn > 0)
                    {
                        ability.AvailableIn -= amount;
                    }
                }
            }
            // Если выбрано конкретное поведение, снижаем кд только у актуальных используемых абилок
            else
            {
                var behaviorAbilities = BehaviorList[CurrentBehavior].AbilityIndexes;
                foreach (var abilityIndex in behaviorAbilities)
                {
                    var ability = AbilityList[abilityIndex];
                    if (ability.AvailableIn > 0)
                    {
                        ability.AvailableIn -= amount;
                    }
                }
            }
        }

        internal void RegisterAbilities()
        {
            foreach (var ability in AbilityList)
            {
                if (ability.IsReactingToEvents) 
                    ability.RegisterEvents();
            }
        }
        internal void UnregisterAbilities()
        {
            foreach (var ability in AbilityList)
            {
                if (ability.IsReactingToEvents) ability.UnregisterEvents();
            }
        }

        internal void AddAttackToPool(Attack attack) { AttackList.Add(attack); }
        internal void AddAbilityToPool(Ability ability) { AbilityList.Add(ability); }


        internal void AddBehavior(EntityBehavior behavior)
        {
            // Проверяем, что в поведении имеется как минимум одна атака
            if (behavior.AttackIndexes.Count < 1)
            {
                throw new Exception("[EntityAI] Попытка добавить поведение, в котором нет хотя бы одной атаки");
            }
            // Проверяем, что записанные во вводимом поведении атаки и абилки зарегистрированны в этом AI классе
            // Проверяем, что максимальный указанный в поведении индекс не превышает количество зарегистрированных атак
            if (behavior.AttackIndexes.Max() > AttackList.Count)
            {
                throw new Exception("[EntityAI] Попытка добавить поведение, в котором индекс атаки превышает максимальное количество зарегистрированных атак");
            }

            // Абилок в поведении может и не быть
            if (behavior.AbilityIndexes.Count > 0)
            {
                // Проверяем, что максимальный указанный в поведении индекс не превышает количество зарегистрированных абилок
                if (behavior.AbilityIndexes.Max() > AbilityList.Count)
                {
                    throw new Exception("[EntityAI] Попытка добавить поведение, в котором индекс атаки превышает максимальное количество зарегистрированных атак");
                }
            }

            BehaviorList.Add(behavior);

            if (CurrentBehavior < 0) ChangeBehavior(0);
        }
        void AddDefaultBehavior()
        {
            var defaultBehavior = new EntityBehavior();
            for (int i = 0; i < AttackList.Count; i++) defaultBehavior.AttackIndexes.Add(i);
            for (int i = 0; i < AbilityList.Count; i++) defaultBehavior.AbilityIndexes.Add(i);
            AddBehavior(defaultBehavior);
        }
        internal void ChangeBehavior(int indexToSet)
        {
            if (indexToSet < 0 || indexToSet >= BehaviorList.Count)
            {
                throw new Exception("[EntityAI] Смена индекса поведения на несуществующее поведение");
            }

            CurrentBehavior = indexToSet;

            // Обновляем список абилок, которые моб может использовать самостоятельно
            UsableAbilitiesInCurrentBehavior.Clear();
            foreach (var index in BehaviorList[CurrentBehavior].AbilityIndexes)
            {
                if (AbilityList[index].UsedDirectly) UsableAbilitiesInCurrentBehavior.Add(index);
            }
        }

        internal int FindAbilityIndex(Ability abilityToFind)
        {
            for (int i = 0; i < AbilityList.Count; i++)
            {
                if (AbilityList[i].GetType() == abilityToFind.GetType()) return i;
            }
            return -1;
        }
    }
}
