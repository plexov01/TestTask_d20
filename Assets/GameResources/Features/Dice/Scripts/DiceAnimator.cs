namespace TestTask_d20.Feautures.Dice
{
    using System;
    using System.Collections;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// КонтроллерАнимации кости
    /// </summary>
    public class DiceAnimator : MonoBehaviour
    {
        /// <summary>
        /// Значение кости поменялось
        /// </summary>
        public event Action<int> OnStateDiceChanged = delegate {  }; 
        
        /// <summary>
        /// Показаны модификаторы
        /// </summary>
        public event Action<int> OnDiceModifiersApplied = delegate {  };
        
        /// <summary>
        /// Анимация кости полностью показана
        /// </summary>
        public event Action OnDiceShowed = delegate {  }; 
        
        private string _currentAnimation = default;

        private bool _coroutineRunning = false;

        private AbstractDice _dice = default;

        private Animator _animator = default;

        private void Awake()
        {
            _dice = FindObjectOfType<AbstractDice>();
            _animator = _dice.GetComponent<Animator>();
        }

        private void Start()
        {
            _animator.Play("Zoom");
        }

        private void OnEnable()
        {
            _dice.OnDiceThrown += StartDiceThrowAnimation;
        }

        private void OnDisable()
        {
            _dice.OnDiceThrown -= StartDiceThrowAnimation;
        }
        private void StartDiceThrowAnimation(int diceValue)
        {
            if (!_coroutineRunning)
            {
                StartCoroutine(StartRollAnimationCoroutine(diceValue));
            }
        }

        private void StartApplyModifiersAnimation(int finalValue)
        {
            if (!_coroutineRunning)
            {
                StartCoroutine(StartApplyModifiersAnimationCoroutine(finalValue));
            }
        }

        private IEnumerator StartRollAnimationCoroutine(int diceValue)
        {
            _coroutineRunning = true;
            _currentAnimation = "OutZoom";
            _animator.Play(_currentAnimation);
            
            AnimationClip currentAnimationClip = _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == _currentAnimation);

            float timeAnimation = currentAnimationClip.length;
            yield return new WaitForSeconds(timeAnimation);
            
            _currentAnimation = "Roll";
            _animator.Play(_currentAnimation);
            
            currentAnimationClip = _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == _currentAnimation);

            timeAnimation = currentAnimationClip.length;
            yield return new WaitForSeconds(timeAnimation);
            
            OnStateDiceChanged(diceValue);

            _coroutineRunning = false;
            yield return null;
            
        }

        private IEnumerator StartApplyModifiersAnimationCoroutine(int finalValue)
        {
            _coroutineRunning = true;
            OnStateDiceChanged(finalValue);
            _currentAnimation = "ApplyModifiers";
            _animator.Play(_currentAnimation);
            
            AnimationClip currentAnimationClip = _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == _currentAnimation);

            float timeAnimation = currentAnimationClip.length;

            yield return new WaitForSeconds(timeAnimation);
            OnDiceModifiersApplied(finalValue);
            
            
            OnDiceShowed();
            
            _coroutineRunning = false;
        }
        
    }

}
