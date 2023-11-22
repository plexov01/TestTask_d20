namespace TestTask_d20.Feautures.Difficulty
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// UI контроллер сложности броска костей
    /// </summary>
    public class DifficultyViewController : MonoBehaviour
    {
        [SerializeField] private Text _difficultyText = default;

        private DifficultyController _difficultyController = default;

        private void Awake()
        {
            _difficultyController = FindObjectOfType<DifficultyController>();
        }

        private void OnEnable()
        {
            _difficultyController.OnDifficultyChanged += UpdateUi;
        }

        private void OnDisable()
        {
            _difficultyController.OnDifficultyChanged -= UpdateUi;
        }

        private void UpdateUi(int difficulty)
        {
            _difficultyText.text = difficulty.ToString();
        }

    }

}
