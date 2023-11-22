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
        /// Показано значение кости
        /// </summary>
        public event Action OnDiceStateShowed = delegate {  };
        
        /// <summary>
        /// Анимация кости полностью показана
        /// </summary>
        public event Action OnDiceAllStatesShowed = delegate {  }; 
        
        private string _currentAnimation = default;
        private bool _coroutineRunning = false;
        
        private AudioSource _audioSource = default;
        private AudioClip _diceThrowClip = default;

        private AbstractDice _dice = default;
        private ModifiersController _modifiersController = default;

        private Animator _animator = default;
        

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _diceThrowClip = Resources.Load<AudioClip>("Dices/Dice_d20/Sounds/ThrowDiceSound");

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
            if (!_coroutineRunning)
            {
                StartCoroutine(StartApplyModifiersAnimationCoroutine(modifiersValue));
            }
        }

        private IEnumerator StartRollAnimationCoroutine(int diceValue)
        {
            _coroutineRunning = true;
            // Проигрываем звук броска кубика
            _audioSource.PlayOneShot(_diceThrowClip);
            
            //Запускаем анимацию OutZoom
            _currentAnimation = "OutZoom";
            _animator.Play(_currentAnimation);
            
            AnimationClip currentAnimationClip = _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == _currentAnimation);

            float timeAnimation = currentAnimationClip.length;
            //Ждём пока воспроизведение анимации закончится
            yield return new WaitForSeconds(timeAnimation);
            
            //Запускаем анимацию Roll
            _currentAnimation = "Roll";
            _animator.Play(_currentAnimation);
            
            currentAnimationClip = _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == _currentAnimation);

            timeAnimation = currentAnimationClip.length;
            
            //Ждём пока воспроизведение анимации закончится
            yield return new WaitForSeconds(timeAnimation);
            
            // Оповещаем, что число на кости изменилось
            OnStateDiceChanged(diceValue);
            _coroutineRunning = false;
            
            // Оповещаем, что этап кручения кости показан
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
            
            // Оповещаем, что число на кости изменилось
            OnStateDiceChanged(currentValue);
            
            //Запускаем анимацию ApplyModifiers
            _currentAnimation = "ApplyModifiers";
            _animator.Play(_currentAnimation);
            
            AnimationClip currentAnimationClip = _animator.runtimeAnimatorController.animationClips
                .First(x => x.name == _currentAnimation);

            float timeAnimation = currentAnimationClip.length;
            
            //Ждём пока воспроизведение анимации закончится
            yield return new WaitForSeconds(timeAnimation+1);
            
            // Оповещаем, что все анимации кости показаны
            OnDiceAllStatesShowed();
            
            _coroutineRunning = false;
        }
        
    }

}
