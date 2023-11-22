namespace TestTask_d20.Feautures.Button
{
    using Dice;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Кнопка бросающая кубик
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ThrowDiceButton : AbstractButton
    {
        private AbstractDice _dice = default;

        protected override void Awake()
        {
            base.Awake();
            _dice = FindObjectOfType<AbstractDice>();
        }

        protected override void OnClick()
        {
            _dice.ThrowDice();
        }
    }

}
