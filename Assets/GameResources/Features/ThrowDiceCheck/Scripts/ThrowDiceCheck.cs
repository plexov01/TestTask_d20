namespace TestTask_d20.Feautures.ThrowDiceCheck
{
    using Dice;
    using Difficulty;
    using Modifier;
    using System;
    using UnityEngine;
    
    /// <summary>
    /// Проверка броска кубика
    /// </summary>
    public class ThrowDiceCheck : MonoBehaviour
    {
        public event Action<bool> OnDiceChecked = delegate {  };
        private int _difficulty = default;
        
        private DifficultyController _difficultyController = default;
        private AbstractDice _dice = default;
        private ModifiersController _modifiersController = default;

        private void Awake()
        {
            _difficultyController = FindObjectOfType<DifficultyController>();
            _dice = FindObjectOfType<AbstractDice>();
            _modifiersController = FindObjectOfType<ModifiersController>();
        }
        
        private void OnEnable()
        {
            _difficultyController.OnDifficultyChanged += SetDifficulty;
            _dice.OnDiceThrown += CheckThrow;
        }

        private void OnDisable()
        {
            _difficultyController.OnDifficultyChanged -= SetDifficulty;
            _dice.OnDiceThrown -= CheckThrow;
        }

        private void SetDifficulty(int difficulty)
        {
            _difficulty = difficulty;
        }

        /// <summary>
        /// Проверить бросок кости
        /// </summary>
        /// <param name="diceValue"></param>
        public void CheckThrow(int diceValue)
        {
            int sumModifiers = _modifiersController.GetAbilityModifiersSum();
            
            if (diceValue + sumModifiers >= _difficulty)
            {
                Debug.Log("success" + (diceValue + sumModifiers));
                //TODO: показать UI прогресса
            }
            else
            {
                Debug.Log("unsuccess" + (diceValue + sumModifiers));
            }

            OnDiceChecked(diceValue + sumModifiers >= _difficulty);
        }

    }

}
