namespace TestTask_d20.Feautures.Button
{
    using UnityEngine;
    using UnityEngine.UI;
    /// <summary>
    /// Класс абстрактной кнопки
    /// </summary>
    public abstract class AbstractButton : MonoBehaviour
    {
        private Button _button = default;

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();

        protected virtual void OnDestroy() => _button.onClick.RemoveListener(OnClick);
    }

}
