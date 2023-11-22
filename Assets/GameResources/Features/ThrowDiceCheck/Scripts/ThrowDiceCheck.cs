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
        private int _currentDiceValue = default;
        
        private AbstractDice _dice = default;
        private DiceAnimator _diceAnimator = default;
        private ModifiersController _modifiersController = default;
        private DifficultyController _difficultyController = default;
        private void Awake()
        {
            _difficultyController = FindObjectOfType<DifficultyController>();
            _dice = FindObjectOfType<AbstractDice>();
            _diceAnimator = FindObjectOfType<DiceAnimator>();
            _modifiersController = FindObjectOfType<ModifiersController>();
        }
        
        private void OnEnable()
        {
            _difficultyController.OnDifficultyChanged += SetDifficulty;
            _dice.OnDiceThrown += SetCurrentDiceValue;
            _diceAnimator.OnDiceAllStatesShowed += CheckThrow;
        }

        private void OnDisable()
        {
            _difficultyController.OnDifficultyChanged -= SetDifficulty;
            _dice.OnDiceThrown -= SetCurrentDiceValue;
            _diceAnimator.OnDiceAllStatesShowed -= CheckThrow;
        }

        private void SetDifficulty(int difficulty)
        {
            _difficulty = difficulty;
        }

        /// <summary>
        /// Проверить бросок кости
        /// </summary>
        /// <param name="diceValue"></param>
        public void CheckThrow()
        {
            int sumModifiers = _modifiersController.GetAbilityModifiersSum();

            OnDiceChecked(_currentDiceValue + sumModifiers >= _difficulty);
        }

        private void SetCurrentDiceValue(int currentDiceValue)
        {
            _currentDiceValue = currentDiceValue;
        }

    }

}
