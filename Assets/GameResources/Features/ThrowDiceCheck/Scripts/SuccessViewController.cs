namespace TestTask_d20.Feautures.ThrowDiceCheck
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Контроллер UI успешности брошенных костей
    /// </summary>
    public class SuccessViewController : MonoBehaviour
    {
        [Range(0.01f,2)] public float TimeShowingSuccess = default;
        [SerializeField] private GameObject _successPanel = default;
        [SerializeField] private Text _successText = default;

        private bool _coroutineRunning = false;
        private Coroutine _hideAfterCoroutine = default;
        private ThrowDiceCheck _throwDiceCheck = default;
        
        private void Awake()
        {
            _throwDiceCheck = FindObjectOfType<ThrowDiceCheck>();
            _successPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _throwDiceCheck.OnDiceChecked += ShowSuccess;
        }

        private void OnDisable()
        {
            _throwDiceCheck.OnDiceChecked -= ShowSuccess;
        }

        private IEnumerator HideAfter(float time)
        {
            _coroutineRunning = true;
            yield return new WaitForSecondsRealtime(time);
            _successPanel.SetActive(false);
            _coroutineRunning = false;

        }

        private void ShowSuccess(bool success)
        {
            _successPanel.SetActive(true);
            
            if (success)
            {
                _successText.color = Color.green;
                _successText.text = "Success!";
            }
            else
            {
                _successText.color = Color.red;
                _successText.text = "Unsuccess!";
            }

            if (_coroutineRunning)
            {
                StopCoroutine(_hideAfterCoroutine);
            }

            _hideAfterCoroutine = StartCoroutine(HideAfter(TimeShowingSuccess));

        }


    }

}
