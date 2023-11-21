namespace TestTask_d20.Feautures.Dice
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Анимация кости
    /// </summary>
    public class DiceAnimation : MonoBehaviour
    {
        [Range(0, 1000)] public float WidthDiceArea = default;
        [Range(0, 1000)] public float HeightDiceArea = default;

        public Transform Dice = default;

        private List<Sprite> _diceAllStates = new List<Sprite>();
        
        private List<Sprite> _diceStateAnimation = new List<Sprite>();

        private void Awake()
        {
            _diceAllStates = Resources.LoadAll<Sprite>("Dices/Dice_d20/Images/DiceStates").ToList();
            
        }

        private IEnumerator StartAnimationCoroutine()
        {
            _diceStateAnimation = _diceAllStates;

            float timeChangeState = 0.1f;
            float deltaTimeChangeState = timeChangeState;
            int currentAnimation = 0;
            
            Vector2 moveVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            moveVector = moveVector.normalized;
            
            Vector2 deltaMoveVector = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
            
            float rollTime = 4f;
            float deltaRollTime = rollTime;
            
            float speed = 15;
            moveVector *= speed;

            while (deltaRollTime > 0f)
            {
                deltaRollTime -= Time.deltaTime;
                

                deltaMoveVector = moveVector * speed;
                deltaMoveVector *= deltaRollTime/rollTime;
                
                Dice.transform.Translate(deltaMoveVector * Time.deltaTime);
                // Debug.Log(deltaRollTime +" "+ deltaMoveVector + " " + Time.deltaTime);
                

                if (Math.Abs(Dice.transform.position.x - transform.position.x) > WidthDiceArea/2)
                {
                    moveVector.x *= -1;
                    // Dice.transform.rotation.eulerAngles = new Vector3(0, Random.Range(-15f, 15f));
                    Dice.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-25f, 25f));
                    // Dice.transform.Rotate(Vector3.up, Random.Range(-15f,15f));
                    // new Quaternion().
                }
                
                if (Math.Abs(Dice.transform.position.y - transform.position.y) > HeightDiceArea/2)
                {
                    moveVector.y *= -1;
                    
                }
                
                deltaTimeChangeState -= Time.deltaTime;
                // if (deltaTimeChangeState > timeChangeState)
                if (deltaTimeChangeState < 0f)
                {
                    deltaTimeChangeState = timeChangeState;

                    currentAnimation = Random.Range(0, _diceStateAnimation.Count);

                    Dice.gameObject.GetComponent<Image>().sprite =
                                                        _diceStateAnimation[currentAnimation];
                    
                    

                    // if (currentAnimation < _diceStateAnimation.Count -1 )
                    // {
                    //     currentAnimation++;
                    // }
                    // else
                    // {
                    //     currentAnimation = 0;
                    // }
                    // Debug.Log(deltaTimeChangeState);
                    
                }
                
                                
                
                yield return null; 
            }

                       
        }

        public void StartAnimation()
        {
            StartCoroutine(StartAnimationCoroutine());
            AbstractDice dice = FindObjectOfType<AbstractDice>();
                // Resources.Load<AbstractDice>("Dice/Dice_d20/ScriptableObjects/Dice_d20");
            dice.Difficulty = 10;
            // Debug.Log(dice.ThrowDice());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(WidthDiceArea,HeightDiceArea));
        }
    }

}
