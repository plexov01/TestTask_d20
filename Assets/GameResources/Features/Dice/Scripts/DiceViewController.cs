namespace TestTask_d20.Feautures.Dice
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Контроллер управления визуальным отображением кости
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class DiceViewController: MonoBehaviour
    {
        private Image _image = default;
        private AbstractDice _dice = default;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _dice = GetComponent<AbstractDice>();
        }

        private void OnEnable()
        {
            _dice.OnDiceThrown += ShowDiceValue;
        }

        private void OnDisable()
        {
            _dice.OnDiceThrown -= ShowDiceValue;
        }

        private void ShowDiceValue(int value)
        {
            _image.sprite = _dice.GetSpriteDice(value - 1);
        }

    }
}

