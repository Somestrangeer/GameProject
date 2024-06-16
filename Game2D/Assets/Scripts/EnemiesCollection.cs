using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemiesCollection : MonoBehaviour
{
    // Store enemies of the level in this list
    private static List<GameObject> enemiesList = new List<GameObject>();
    private static List<EnemyShadow> shadows = new List<EnemyShadow>();

    private float speed = EnemyParams.enemySpeed;
    private float visibleArea = EnemyParams.visibleArea;
    private float attackArea = EnemyParams.attackArea;

    //Use for cut scene to stop enemies attacking us!
    public static bool attackMode { get; set; }

    private void Awake()
    {
        // Get Enemies by their tag "Enemy" 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemiesList.Add(enemy);

            SpriteRenderer[] spriteShadows = enemy.GetComponentsInChildren<SpriteRenderer>();

            if (spriteShadows.Length >= 4) // Проверяем, что есть как минимум 4 элемента в массиве
            {
                EnemyShadow shadow = new EnemyShadow();
                shadow.enemy = enemy;

                shadow.shadowUpDown  = spriteShadows[1].gameObject; //shadowUpDown
                shadow.shadowLeft = spriteShadows[2].gameObject; //shadowLeft
                shadow.shadowLeft.SetActive(false);
                shadow.shadowRight = spriteShadows[3].gameObject; //shadowRight
                shadow.shadowRight.SetActive(false);

                shadows.Add(shadow);
            }
            else
            {
                // Обработка случая, когда не хватает элементов в массиве spriteShadows
            }
        }

    }

    private void Update()
    {
        // We always update the vars to get the changed values
        visibleArea = EnemyParams.visibleArea;
        attackArea = EnemyParams.attackArea;

        if(attackMode)
            EnemyMovement();
    }

    private void EnemyMovement() 
    {
        GameObject hero = Hero.getHero();
        bool heroInSight = false;

        foreach (GameObject enemy in enemiesList)
        {
           
            // Calculate the distance if the hero is inside the enemy's visibleArea
            float distanceJandar = Vector3.Distance(hero.transform.position, enemy.transform.position);
            float bias = 0.0f;

            Animator enemyAnimator = enemy.GetComponent<Animator>();

            if (distanceJandar <= visibleArea && distanceJandar > 0.1)
            {
                //Hero.setBattleMode(true);

                Vector3 movement = Vector3.zero;

                //Logs
                /*Debug.Log("A - " + Math.Abs(enemy.transform.position.x - hero.transform.position.x).ToString());
                Debug.Log("B - " + distance.ToString());*/

                bias = Math.Abs(enemy.transform.position.x - hero.transform.position.x);

                // Determine movement direction based on hero's position relative to enemy
                //Here and there we use the biases to draw the 'border' around the hero for enemies
                if (enemy.transform.position.x < hero.transform.position.x && bias >= 2)
                {
                    // Move right
                    movement += new Vector3(speed, 0, 0);
                    enemyAnimator.Play("Right Animation");

                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowUpDown.SetActive(false);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowLeft.SetActive(false);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowRight.SetActive(true);
                }
                else if (enemy.transform.position.x > hero.transform.position.x && bias >= 2)
                {
                    // Move left
                    movement -= new Vector3(speed, 0, 0);
                    enemyAnimator.Play("Left Animation");
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowUpDown.SetActive(false);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowLeft.SetActive(true);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowRight.SetActive(false);
                }
                else if (enemy.transform.position.y + 1.37f < hero.transform.position.y + 0.1)
                {
                    // Move up
                    movement += new Vector3(0, speed, 0);
                    enemyAnimator.Play("Up Animation");
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowUpDown.SetActive(true);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowLeft.SetActive(false);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowRight.SetActive(false);
                }
                else if (enemy.transform.position.y /*+ 1.37f */> hero.transform.position.y - 0.2)
                {
                    // Move down
                    movement -= new Vector3(0, speed, 0);
                    enemyAnimator.Play("Down Animation");
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowUpDown.SetActive(true);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowLeft.SetActive(false);
                    shadows.FirstOrDefault(s => s.enemy == enemy).shadowRight.SetActive(false);
                }
                
                // Apply movement only once based on the final direction
                enemy.transform.position += movement * Time.deltaTime;

                heroInSight = true;
            }
            else if (bias <= attackArea)
            {
                heroInSight = true;
            }
        }

        Hero.setBattleMode(heroInSight, true);
    }

    //Remove selected enemy/enemies 
    public static void removeEnemies(List<GameObject> enemies) 
    {
        foreach (GameObject enemy in enemies)
        {
            enemiesList.Remove(enemy);
        }
    } 

    public static List<GameObject> getEnemyCollection() { return enemiesList; }
}

struct EnemyShadow 
{
    public GameObject enemy;
    //public List<GameObject> shadows;

    public GameObject shadowUpDown;
    public GameObject shadowLeft;
    public GameObject shadowRight;
}

