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
        private DiceAnimator _diceAnimator = default;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _dice = GetComponent<AbstractDice>();
            _diceAnimator = FindObjectOfType<DiceAnimator>();
        }

        private void OnEnable()
        {
            _diceAnimator.OnStateDiceChanged += ShowDiceValue;
            _diceAnimator.OnDiceAllStatesShowed += ResetSprite;
        }

        private void OnDisable()
        {
            _diceAnimator.OnStateDiceChanged -= ShowDiceValue;
            _diceAnimator.OnDiceAllStatesShowed -= ResetSprite;
        }

        private void ShowDiceValue(int value)
        {
            _image.overrideSprite = _dice.GetSpriteDice(value - 1);
        }
        
        private void ResetSprite()
        {
            _image.overrideSprite = null;
        }

    }
}

