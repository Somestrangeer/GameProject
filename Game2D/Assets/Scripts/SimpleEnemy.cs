using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, Enemy
{
    private float hp = 200.0f;
    private float damage = 5.0f;
    private float attackDistance = EnemyParams.attackArea;
    private float damageSpeed = EnemyParams.damageSpeed;
    private bool isAttacking = false;

    private GameObject simpleEnemy;
    private Animator simpleEnemyAnimator;

    

    private void Awake()
    {
        // Get enemy
        simpleEnemy = gameObject;
        simpleEnemyAnimator = simpleEnemy.GetComponent<Animator>();

    }

    private void Update()
    {
        // We always update the var to get the changed value
        attackDistance = EnemyParams.attackArea;

        if (Vector3.Distance(simpleEnemy.transform.position, Hero.getHero().transform.position) - 0.4f <= attackDistance && !isAttacking)
        {
            Debug.Log("YEAS");
            //Start the function in parallel
            if (EnemiesCollection.attackMode)
                StartCoroutine(Attack());
        }
        else 
        {
            simpleEnemyAnimator.SetBool("AttackStateLeft", false);
            simpleEnemyAnimator.SetBool("IsAttackingLeft", false);
            simpleEnemyAnimator.SetBool("AttackStateRight", false);
            simpleEnemyAnimator.SetBool("IsAttacingRight", false);
        }
    }

    private IEnumerator Attack()
    {
        if (isAttacking) yield break;

        Renderer rend = Hero.getHero().GetComponent<Renderer>();

        isAttacking = true;

        //Start animations
        if (simpleEnemy.transform.position.x < Hero.getHero().transform.position.x)
        {
            simpleEnemyAnimator.SetBool("AttackStateLeft", false);
            simpleEnemyAnimator.SetBool("IsAttackingLeft", false);

            simpleEnemyAnimator.SetBool("AttackStateRight", true);
            simpleEnemyAnimator.SetBool("IsAttacingRight", true);
        }
        else 
        {
            simpleEnemyAnimator.SetBool("AttackStateRight", false);
            simpleEnemyAnimator.SetBool("IsAttacingRight", false);

            simpleEnemyAnimator.SetBool("AttackStateLeft", true);
            simpleEnemyAnimator.SetBool("IsAttackingLeft", true);
        }

        

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

    public void EnableAttackMode()
    {
        EnemiesCollection.attackMode = true;
    }


}
