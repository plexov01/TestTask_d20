namespace TestTask_d20.Feautures.Difficulty
{
    using System;
    using UnityEngine;
    /// <summary>
    /// Сложность выброса успешного броска кубика
    /// </summary>
    public sealed class DifficultyController : MonoBehaviour
    {
        public event Action<int> OnDifficultyChanged = delegate {  };
        public int Difficulty = default;

        private void Start()
        {
            OnDifficultyChanged(Difficulty);
        }
    }

}
