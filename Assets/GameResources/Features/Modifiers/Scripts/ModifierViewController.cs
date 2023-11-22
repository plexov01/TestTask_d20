namespace TestTask_d20.Feautures.Modifier
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Контроллер UI модификатора
    /// </summary>
    [RequireComponent(typeof(Image), typeof(Animation))]
    public class ModifierViewController : MonoBehaviour
    {
        
        [SerializeField] private Text _modifierNumber = default;
        private Image _modifierImage = default;
        private Animation _animation = default;

        private void Awake()
        {
            _modifierImage = GetComponent<Image>();
            _animation = GetComponent<Animation>();
            
            AnimationClip clip = Resources.Load<AnimationClip>("Modifiers/Animations/Apply");
            _animation.AddClip(clip, clip.name);
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
        
        /// <summary>
        /// Проиграть анимацию применения модификатора
        /// </summary>
        public void ApplyModifierAnimation()
        {
            _animation.Play();
        }
        
    }

}
