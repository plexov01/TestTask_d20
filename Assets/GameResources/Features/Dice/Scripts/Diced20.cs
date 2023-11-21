namespace TestTask_d20.Feautures.Dice
{
    using System.Linq;
    using UnityEngine;
    /// <summary>
    /// Реализация кубика с гранью 20
    /// </summary>
    public class Diced20 : AbstractDice
    {
        private const string DiceImagesPath = "Dices/Dice_d20/Images/DiceStates";

        protected override void Awake()
        {
            base.Awake();
            _diceStates = Resources.LoadAll<Sprite>(DiceImagesPath).ToList();
            _diceMaxValue = 20;
        }
        
    }

}
