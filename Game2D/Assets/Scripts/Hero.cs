using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private static float hp = 200f;
    private float damage = 50f;
    private int speed = 3;
    private float attackDistance = 2.0f;
    private bool sneakModeEnable = false;

    private List<GameObject> killedEnemies = new List<GameObject>();

    // The object of our hero
    private static GameObject hero;

    public static GameObject getHero() { return hero; }

    private void Awake()
    {
        // Get the object of hero's sprite
        hero = gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        heroMovement();

        // There's a bug. When We click once this condition will fire for 3-5 times
        // Fix lately
        if (Input.GetMouseButtonDown(0))
        {
            attackEnemy();
        }
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            sneakHero();
        }
        
    }

    private void heroMovement()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.D)) { movement += new Vector3(speed, 0, 0); }
        if (Input.GetKey(KeyCode.A)) { movement -= new Vector3(speed, 0, 0); }
        if (Input.GetKey(KeyCode.W)) { movement += new Vector3(0, speed, 0); }
        if (Input.GetKey(KeyCode.S)) { movement -= new Vector3(0, speed, 0); }

        // To make an animation we have to multiply it by time
        hero.transform.position += movement * Time.deltaTime;
    }
    private void sneakHero() 
    {
        if (!sneakModeEnable) 
        {
            // NOTE: Add animation here later
            hero.transform.localScale = new Vector3(1, 0.5f, 1);

            EnemyParams.visibleArea = 4f;
            EnemyParams.attackArea = 1.5f;

            sneakModeEnable = true;
        }
        else 
        {
            // NOTE: Add animation here later
            hero.transform.localScale = new Vector3(1, 1, 1);

            EnemyParams.visibleArea = 8f;
            EnemyParams.attackArea = 2f;

            sneakModeEnable = false;
        }
        

    }
    private void attackEnemy()
    {
        List<GameObject> enemyList = EnemiesCollection.getEnemyCollection();

        foreach (GameObject enemy in enemyList) 
        {
            if (enemy != null) 
            {
                // We use the interface to interact with an enemy
                Enemy enemyObject = enemy.GetComponent<Enemy>();


                // Calculate the distance between the hero and an enemy
                if (Vector3.Distance(hero.transform.position, enemy.transform.position) <= attackDistance) 
                {
                    enemyObject.TakeDamage(damage);

                    //Store enemy as killed if he's not active (killed)
                    if (!enemy.active) 
                    {
                        killedEnemies.Add(enemy);
                    }
                }
            }
        }
        // Get rid of killed enemy forever
        if (killedEnemies.Count != 0) 
        {
            EnemiesCollection.removeEnemies(killedEnemies);
        }
    }

    public static void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0.0f)
        {
            Die();
        }
    }

    private static void Die()
    {
        // We hide the hero
        hero.SetActive(false);

    }
}
