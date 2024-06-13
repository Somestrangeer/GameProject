using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Hero : MonoBehaviour
{
    private static float hp = 20;
    private float damage = 50f;
    private int speed = 3;
    private float attackDistance = 2.0f;
    private bool sneakModeEnable = false;
    private static bool battleModeEnable = false;

    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;

    private GameObject shadowUpDown;
    private GameObject shadowLeft;
    private GameObject shadowRight;

    private List<GameObject> killedEnemies = new List<GameObject>();

    // The object of our hero
    private static GameObject hero;
    private static Animator heroAnima;

    public static GameObject getHero() { return hero; }

    private static string sceneName;

    private void Awake()
    {
        // Get the object of hero's sprite
        hero = gameObject;

        sceneName = SceneManager.GetActiveScene().name;

        heroAnima = hero.GetComponent<Animator>();

        SpriteRenderer[] spriteRenderers = hero.GetComponentsInChildren<SpriteRenderer>();
        shadowUpDown = spriteRenderers[1].gameObject;
        shadowLeft = spriteRenderers[2].gameObject;
        shadowRight = spriteRenderers[3].gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (hp <= 10 && sceneName == "Village") 
        {
            //GlobalLightDim.globalLight.intensity -= 0.07f * Time.deltaTime;

        }

        heroMovement();

        if (Input.GetMouseButtonDown(0) && EnemiesCollection.attackMode)
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

        if (Input.GetKey(KeyCode.D) && !isMovingLeft && !isMovingUp && !isMovingDown) 
        {
            shadowUpDown.SetActive(false);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(true);
            heroAnima.Play("Right Animation");
            movement += new Vector3(speed, 0, 0);
            isMovingRight = true;
        }
        else if(Input.GetKey(KeyCode.A) && !isMovingRight && !isMovingUp && !isMovingDown) 
        {
            shadowUpDown.SetActive(false);
            shadowLeft.SetActive(true);
            shadowRight.SetActive(false);
            heroAnima.Play("Left Animation");
            movement -= new Vector3(speed, 0, 0);
            isMovingLeft = true;
        }
        else if(Input.GetKey(KeyCode.W) && !isMovingDown && !isMovingLeft && !isMovingRight) 
        {
            shadowUpDown.SetActive(true);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(false);
            heroAnima.Play("Up Animation");
            movement += new Vector3(0, speed, 0);
            isMovingUp = true;
        }
        else if(Input.GetKey(KeyCode.S) && !isMovingUp && !isMovingLeft && !isMovingRight) 
        {
            shadowUpDown.SetActive(true);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(false);
            heroAnima.Play("Down Animation");
            movement -= new Vector3(0, speed, 0);
            isMovingDown = true;
        }
        else
        {
            shadowUpDown.SetActive(true);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(false);
        }

        if (!Input.GetKey(KeyCode.D))
        {
            if (battleModeEnable)
            {
                heroAnima.SetBool("AttackStateRight", true);
            }
            isMovingRight = false;
            
        }
        if (!Input.GetKey(KeyCode.A))
        {
            if (battleModeEnable)
            {
                heroAnima.SetBool("AttackStateRight", true);
            }
            isMovingLeft = false;
            
        }
        if (!Input.GetKey(KeyCode.W))
        {
            if (battleModeEnable)
            {
                heroAnima.SetBool("AttackStateRight", true);
            }
            isMovingUp = false;
            
        }
        if (!Input.GetKey(KeyCode.S))
        {
            if (battleModeEnable)
            {
                heroAnima.SetBool("AttackStateRight", true);
            }
            isMovingDown = false;
            
        }

        if (movement != Vector3.zero) 
        {
            Footstep.Instance.PlaySound(0.1f);
        }

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

        //Sort the list of enemis by distance to the hero
        enemyList.Sort((a, b) => Vector3.Distance(hero.transform.position, a.transform.position).CompareTo(Vector3.Distance(hero.transform.position, b.transform.position)));

        foreach (GameObject enemy in enemyList) 
        {
            if (enemy != null) 
            {
                // Calculate the distance between the hero and an enemy
                if (Vector3.Distance(hero.transform.position, enemy.transform.position) - 0.4f <= attackDistance) 
                {
                    // We use the interface to interact with an enemy
                    Enemy enemyObject = enemy.GetComponent<Enemy>();

                    if (enemy.transform.position.x < hero.transform.position.x)
                    {
                        heroAnima.SetBool("AttackStateLeft", true);
                        heroAnima.Play("Attack Left Side");
                    }
                    else
                    {
                        heroAnima.SetBool("AttackStateRight", true);
                        heroAnima.Play("Attack Right Side");
                    }
                    
                    enemyObject.TakeDamage(damage);

                    //Store enemy as killed if he's not active (killed)
                    if (!enemy.active) 
                    {
                        killedEnemies.Add(enemy);
                    }

                    //break the loop cuz we need to attack the closest enemy to the hero in the attack area!
                    break;
                }
            }
        }
        // Get rid of killed enemy forever
        if (killedEnemies.Count != 0) 
        {
            EnemiesCollection.removeEnemies(killedEnemies);
        }
    }

    public static void setBattleMode(bool heroInSight, bool side) 
    {
        battleModeEnable = heroInSight;
        if (battleModeEnable) 
        {
            heroAnima.SetBool("AttackStateRight", true);
        }
        if (!battleModeEnable)
        {
            heroAnima.SetBool("AttackStateLeft", false);
            heroAnima.SetBool("AttackStateRight", false);
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
        battleModeEnable = false;
        Renderer rend = hero.GetComponent<Renderer>();
        rend.material.color = Color.white;
        heroAnima.SetBool("AttackStateRight", false);
        hero.SetActive(false);
        EnemiesCollection.attackMode = false;
        if (sceneName == "Village") 
        {
            hp = 50f;
            hero.SetActive(true);
            hero.transform.position = new Vector3(54.5f, -50f, 0);
            //EnemiesCollection.attackMode = true;
            //GlobalLightDim.globalLight.intensity += 0.07f * Time.deltaTime;
            

        }

    }
}
