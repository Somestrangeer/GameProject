using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, Enemy
{
    private float hp = 200.0f;
    private float damage = 30.0f;
    private float attackDistance = EnemyParams.attackArea;
    private float damageSpeed = EnemyParams.damageSpeed;
    private bool isAttacking = false;

    private GameObject simpleEnemy;

    private void Awake()
    {
        // Get enemy
        simpleEnemy = gameObject;

    }

    private void Update()
    {
        // We always update the var to get the changed value
        attackDistance = EnemyParams.attackArea;

        if (Vector3.Distance(simpleEnemy.transform.position, Hero.getHero().transform.position) <= attackDistance && !isAttacking)
        {
            //Start the function in parallel
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        if (isAttacking) yield break;

        //rend - is a simulation of animation. NOTE: CHANGE TO ANIMATION LATER
        Renderer rend = Hero.getHero().GetComponent<Renderer>();

        isAttacking = true;

        //The hero was hit
        rend.material.color = Color.red;
        Hero.TakeDamage(damage);

        yield return new WaitForSeconds(damageSpeed);

        rend.material.color = Color.white;

        yield return new WaitForSeconds(damageSpeed);

        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0.0f)
        {
            Die();
        }
    }

    private void Die() 
    {
        // We hide the enemy and remove it from the enemy's list 
        simpleEnemy.SetActive(false);

    }


}
