namespace TestTask_d20.Feautures.Modifier
{
    using UnityEngine;
    using UnityEngine.Serialization;

    /// <summary>
    /// Абстрактный модификатор
    /// </summary>
    [CreateAssetMenu(fileName = "Modifier", menuName = "ScriptableObjects/Modifier")]
    public class Modifier : ScriptableObject
    {
        /// <summary>
        /// Количество очков, которое добавляет модификатор
        /// </summary>
        public int ModifierNumber = default;

        /// <summary>
        /// Изображение модификатора
        /// </summary>
        public Sprite ModifierSprite = default;
    }

}
