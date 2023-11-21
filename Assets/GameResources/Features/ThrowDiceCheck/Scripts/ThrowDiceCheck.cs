namespace TestTask_d20.Feautures.ThrowDiceCheck
{
    using Dice;
    using Modifier;
    using System;
    using UnityEngine;
    
    /// <summary>
    /// Проверка броска кубика
    /// </summary>
    public class ThrowDiceCheck : MonoBehaviour
    {
        public event Action<int> OnDifficultyChanged = delegate {  };
        public int Difficulty = default;

        private AbstractDice _dice = default;
        private ModifiersController _modifiersController = default;

        private void Awake()
        {
            _dice = FindObjectOfType<AbstractDice>();
            _modifiersController = FindObjectOfType<ModifiersController>();
        }

        private void Start()
        {
            OnDifficultyChanged(Difficulty);            
        }

        private void OnEnable()
        {
            _dice.OnDiceThrown += CheckThrow;
        }

        private void OnDisable()
        {
            _dice.OnDiceThrown -= CheckThrow;
        }

        /// <summary>
        /// Проверить бросок кости
        /// </summary>
        /// <param name="diceValue"></param>
        public void CheckThrow(int diceValue)
        {
            int sumModifiers = _modifiersController.GetAbilityModifiersSum();
            
            if (diceValue + sumModifiers >= Difficulty)
            {
                Debug.Log("success");
            }
            else
            {
                Debug.Log("unsuccess");
            }
        }

    }

}
