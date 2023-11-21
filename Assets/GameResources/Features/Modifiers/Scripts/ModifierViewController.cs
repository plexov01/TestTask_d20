namespace TestTask_d20.Feautures.Modifier
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Контроллер UI модификатора
    /// </summary>
    public class ModifierViewController : MonoBehaviour
    {
        
        [SerializeField] private Text _modifierNumber = default;
        private Image _modifierImage = default;

        private void Awake()
        {
            _modifierImage = GetComponent<Image>();
        }

        /// <summary>
        /// Обновить UI модификатора
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sprite"></param>
        public void UpdateUi(string text, Sprite sprite)
        {
            _modifierNumber.text = text;
            _modifierImage.sprite = sprite;
        }

    }

}
