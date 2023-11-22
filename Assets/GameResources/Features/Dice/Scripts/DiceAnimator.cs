namespace TestTask_d20.Feautures.Dice
{
    using Modifier;
    using System;
    using System.Collections;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Контроллер Анимации кости
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
        /// Показано значение кости
        /// </summary>
        public event Action OnDiceStateShowed = delegate {  };
        
        /// <summary>
        /// Анимация кости полностью показана
        /// </summary>
        public event Action OnDiceAllStatesShowed = delegate {  }; 
        
        private string _currentAnimation = default;
        private bool _coroutineRunning = false;

        private AbstractDice _dice = default;
        private ModifiersController _modifiersController = default;

        private Animator _animator = default;

        private void Awake()
        {
            _dice = FindObjectOfType<AbstractDice>();
            _modifiersController = FindObjectOfType<ModifiersController>();
            
            _animator = _dice.GetComponent<Animator>();
        }

        private void Start()
        {
            _animator.Play("Zoom");
        }

        private void OnEnable()
        {
            _dice.OnDiceThrown += StartDiceThrowAnimation;
            _modifiersController.OnModifiersApplied += StartApplyModifiersAnimation;
        }

        private void OnDisable()
        {
            _dice.OnDiceThrown -= StartDiceThrowAnimation;
            _modifiersController.OnModifiersApplied -= StartApplyModifiersAnimation;
        }
        private void StartDiceThrowAnimation(int diceValue)
        {
            if (!_coroutineRunning)
            {
                StartCoroutine(StartRollAnimationCoroutine(diceValue));
            }
        }

        private void StartApplyModifiersAnimation(int modifiersValue)
        {
            Debug.Log(_coroutineRunning);
            if (!_coroutineRunning)
            {
                StartCoroutine(StartApplyModifiersAnimationCoroutine(modifiersValue));
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
            
            OnDiceStateShowed();
            yield return null;
            
        }

        private IEnumerator StartApplyModifiersAnimationCoroutine(int modifiresValue)
        {
            _coroutineRunning = true;

            int currentValue = _dice.DiceCurrentValue + modifiresValue;
            if (currentValue > _dice.DiceMaxValue)
            {
                currentValue = _dice.DiceMaxValue;
            }
            OnStateDiceChanged(currentValue);
            _currentAnimation = "ApplyModifiers";
            _animator.Play(_currentAnimation);
            
            AnimationClip currentAnimationClip = _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == _currentAnimation);

            float timeAnimation = currentAnimationClip.length;

            yield return new WaitForSeconds(timeAnimation+1);


            OnDiceAllStatesShowed();
            
            _coroutineRunning = false;
        }
        
    }

}
