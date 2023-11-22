namespace TestTask_d20.Feautures.Dice
{
    using Difficulty;
    using System;
    using System.Collections.Generic;
    using ThrowDiceCheck;
    using UnityEngine;
    using Random = UnityEngine.Random;
    /// <summary>
    /// Абстрактная игральная кость
    /// </summary>
    public abstract class AbstractDice : MonoBehaviour
    {
        /// <summary>
        /// Кость брошена
        /// </summary>
        public event Action<int> OnDiceThrown = delegate { };
        
        /// <summary>
        /// Исключение несколько проигрышей или выигрышей подряд
        /// </summary>
        public bool EneableNegentropy = false;
        
        protected int _diceCurrentValue = default;
        /// <summary>
        /// Текущеее значение кости
        /// </summary>
        public int DiceCurrentValue => _diceCurrentValue;

        protected int _diceMaxValue = default;
        /// <summary>
        /// Максимальное значение кости
        /// </summary>
        public int DiceMaxValue => _diceMaxValue;
        protected List<Sprite> _diceStates = new List<Sprite>();

        [SerializeField]
        protected int _historyCapacity = 5;
        
        private List<bool> _historyDiceRolls = new List<bool>();
        
        private int _difficulty = default;

        private ThrowDiceCheck _throwDiceCheck = default;        
        private DifficultyController _difficultyController = default;        
        
        protected virtual void Awake()
        {
            _throwDiceCheck = FindObjectOfType<ThrowDiceCheck>();
            _difficultyController = FindObjectOfType<DifficultyController>();
        }

        protected virtual void OnEnable()
        {
            _difficultyController.OnDifficultyChanged += SetDifficulty;
        }

        protected virtual void OnDisable()
        {
            _difficultyController.OnDifficultyChanged -= SetDifficulty;
        }

        /// <summary>
        ///  Установить сложность броска кубика
        /// </summary>
        public void SetDifficulty(int value)
        {
            if (value > 0 && value <= _diceMaxValue)
            {
                _difficulty = value;
            }
            else
            {
                Debug.Log("Difficulty out of dice range");
            }
        }
        
        /// <summary>
        /// Бросить ккость
        /// </summary>
        /// <returns></returns>
        public void ThrowDice()
        {
            int diceValue = Random.Range(1, _diceMaxValue);
            bool success = diceValue >= _difficulty;

            // Проверка на включенный параметр исключения серии одинаковых результатов 
            if (EneableNegentropy)
            {
                List<bool> listCoincidences = _historyDiceRolls.FindAll(x => x == success);
                
                // Проверка на нежелательный повтор
                if (listCoincidences.Count >= _historyCapacity)
                {
                    // Рандомим подходящее число
                    while (success == (diceValue >= _difficulty))
                    {
                        diceValue = Random.Range(1, _diceMaxValue);
                    }
                    success = diceValue >= _difficulty;
                }

            }
            
            _historyDiceRolls.Add(success);

            while (_historyDiceRolls.Count > _historyCapacity)
            {
                _historyDiceRolls.RemoveAt(0);
            }

            _diceCurrentValue = diceValue;
            OnDiceThrown(diceValue);
        }
        
        /// <summary>
        /// Получить спрайт состояния кости по индексу
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Sprite GetSpriteDice(int indexSprite)
        {
            if (indexSprite > _diceStates.Count-1)
            {
                Debug.Log("indexSprite больше, чем количество спрайтов" + indexSprite + " " + (_diceStates.Count - 1));
                return null;
            }

            return _diceStates[indexSprite];
        }
        
    }

}
