namespace TestTask_d20.Feautures.Modifier
{
    using ObjectCreator;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    /// <summary>
    /// Контроллер для отображения UI модификаторов
    /// </summary>
    public class ModifiersController : MonoBehaviour
    {
        private List<Modifier> _modifiers = new List<Modifier>();

        private GameObject _prefabModifier = default;

        private ObjectCreator _objectCreator = default;
        
        private void Awake()
        {
            _objectCreator = FindObjectOfType<ObjectCreator>();
            _prefabModifier = Resources.Load<GameObject>("Modifiers/Prefabs/Modifier");
            _modifiers = Resources.LoadAll<Modifier>("Modifiers/ScriptableObjects").ToList();
            
            PlaceModifiers();
        }
        
        /// <summary>
        /// Расположить модификаторы
        /// </summary>
        public void PlaceModifiers()
        {
            for (int i = 0; i < _modifiers.Count; i++)
            {
                GameObject newInstance = _objectCreator.CreateObject(_prefabModifier, transform.position, Quaternion.identity, transform);

                var modifier = newInstance.GetComponent<ModifierViewController>();
                string textInstance = (_modifiers[i].ModifierNumber >= 0 ? "+" : "-") + _modifiers[i].ModifierNumber;
                modifier.UpdateUi(textInstance, _modifiers[i].ModifierSprite);
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