namespace TestTask_d20.Feautures.Modifier
{
    using Dice;
    using ObjectCreator;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    /// <summary>
    /// Контроллер для отображения UI модификаторов
    /// </summary>
    public class ModifiersController : MonoBehaviour
    {
        /// <summary>
        /// Модификаторы применены
        /// </summary>
        public event Action<int> OnModifiersApplied = delegate {  }; 
        
        private List<Modifier> _modifiers = new List<Modifier>();
        private List<ModifierViewController> _modifierViewControllers = new List<ModifierViewController>();
        
        private GameObject _prefabModifier = default;

        private ObjectCreator _objectCreator = default;
        private DiceAnimator _diceAnimator = default;
        
        
        private void Awake()
        {
            _objectCreator = FindObjectOfType<ObjectCreator>();
            _diceAnimator = FindObjectOfType<DiceAnimator>();
            
            _prefabModifier = Resources.Load<GameObject>("Modifiers/Prefabs/Modifier");
            _modifiers = Resources.LoadAll<Modifier>("Modifiers/ScriptableObjects").ToList();
            
            PlaceModifiers();
        }
        
        private void OnEnable()
        {
            _diceAnimator.OnDiceStateShowed += StartModifiersAnimation;
        }

        private void OnDisable()
        {
            _diceAnimator.OnDiceStateShowed -= StartModifiersAnimation;
        }

        private IEnumerator StartModifiersAnimationCoroutine()
        {
            foreach (var modifierViewController in _modifierViewControllers)
            {
                modifierViewController.ApplyModifierAnimation();
            }
            
            yield return new WaitForSeconds(1f);
            
            OnModifiersApplied(GetAbilityModifiersSum());
        }
        
        private void StartModifiersAnimation()
        {
            StartCoroutine(StartModifiersAnimationCoroutine());
        } 
        
        /// <summary>
        /// Расположить модификаторы
        /// </summary>
        public void PlaceModifiers()
        {
            for (int i = 0; i < _modifiers.Count; i++)
            {
                GameObject newInstance = _objectCreator.CreateObject(_prefabModifier, transform.position, Quaternion.identity, transform);

                var modifierView = newInstance.GetComponent<ModifierViewController>();
                _modifierViewControllers.Add(modifierView);
                string textInstance = (_modifiers[i].ModifierNumber > 0 ? "+": String.Empty) + _modifiers[i].ModifierNumber;
                modifierView.UpdateUi(textInstance, _modifiers[i].ModifierSprite);
            }
        }
        
        /// <summary>
        /// Получить сумму модификаторов
        /// </summary>
        /// <returns></returns>
        public int GetAbilityModifiersSum()
        {
            int sum = 0;
            for (int i = 0; i < _modifiers.Count; i++)
            {
                sum += _modifiers[i].ModifierNumber;
            }

            return sum;
        }

    }

}