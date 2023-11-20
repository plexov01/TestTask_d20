namespace TestTask_d20.Feautures.Dice
{
    using System;
    using System.Collections.Generic;
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
        
        protected int _diceMaxValue = default;
        [SerializeField, Min(1)] protected int _difficulty = default;
        
        /// <summary>
        ///  Сложность броска кубика
        /// </summary>
        public int Difficulty
        {
            set {
                if (value > 0 && value <= _diceMaxValue)
                {
                    _difficulty = value;
                }
            }
        }

        protected List<Sprite> _diceStates = new List<Sprite>();
        protected List<bool> _historyDiceRolls = new List<bool>();

        protected int _historyCapacity = 3;
        
        /// <summary>
        /// Бросить ккость
        /// </summary>
        /// <returns></returns>
        public virtual void ThrowDice()
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

            OnDiceThrown(diceValue);
        }
        
        /// <summary>
        /// Получить спрайт состояния кости
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Sprite GetSpriteDice(int indexSprite)
        {
            if (indexSprite > _diceStates.Count-1)
            {
                Debug.Log("indexSprite больше, чем количество спрайтов");
                return null;
            }

            return _diceStates[indexSprite];
        }
        
    }

}
