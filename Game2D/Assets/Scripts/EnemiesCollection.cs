using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemiesCollection : MonoBehaviour
{
    // Store enemies of the level in this list
    private static List<GameObject> enemiesList = new List<GameObject>();

    private int speed = EnemyParams.enemySpeed;
    private float visibleArea = EnemyParams.visibleArea;
    private float attackArea = EnemyParams.attackArea;

    private void Awake()
    {
        // Get Enemies by their tag "Enemy" 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies) 
        {
            enemiesList.Add(enemy);
        }
    }

    private void Update()
    {
        // We always update the vars to get the changed values
        visibleArea = EnemyParams.visibleArea;
        attackArea = EnemyParams.attackArea;

        EnemyMovement();
    }

    private void EnemyMovement() 
    {
        GameObject hero = Hero.getHero();

        foreach (GameObject enemy in enemiesList)
        {
            // Calculate the distance if the hero is inside the enemy's visibleArea
            float distance = Vector3.Distance(hero.transform.position, enemy.transform.position);

            if (distance <= visibleArea && distance > attackArea)
            {
                Vector3 movementX = Vector3.zero;
                Vector3 movementY = Vector3.zero;
                // If the enemy is on the left by x coordinate
                if (enemy.transform.position.x < hero.transform.position.x)
                {
                    movementX += new Vector3(speed, 0, 0);
                    enemy.transform.position += movementX * Time.deltaTime;

                    // If the enemy is above the hero we get it up
                    if (enemy.transform.position.y < hero.transform.position.y)
                    {
                        movementY += new Vector3(0, speed, 0);
                        enemy.transform.position += movementY * Time.deltaTime;
                    }
                }
                // If the enemy is on the left by x coordinate
                else
                {
                    movementX -= new Vector3(speed, 0, 0);
                    enemy.transform.position += movementX * Time.deltaTime;

                    // If the enemy is below the hero we get it down
                    if (enemy.transform.position.y > hero.transform.position.y)
                    {
                        movementY -= new Vector3(0, speed, 0);
                        enemy.transform.position += movementY * Time.deltaTime;
                    }
                }
            }
        }
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

