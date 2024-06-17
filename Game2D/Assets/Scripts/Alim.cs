using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alim : MonoBehaviour
{
    // Start is called before the first frame update

    private static float hp = 20;
    private float damage = 5f;
    private int speed = 3;
    private float attackDistance = 2.0f;

    private static bool battleModeEnable = false;

    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;

    private GameObject shadowUpDown;
    private GameObject shadowLeft;
    private GameObject shadowRight;

    private List<GameObject> killedEnemies = new List<GameObject>();

    private static GameObject alim;
    private static Animator alimAnimator;

    private static List<GameObject> enemiesList;
    private Transform currentTarget;



    public static GameObject getAlim() { return alim; }
    void Start()
    {
        alim = gameObject;
        alimAnimator = alim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesList = EnemiesCollection.getEnemyCollection();
        GameObject nearestObject = FindNearestObject();

        if (nearestObject != null && EnemiesCollection.attackMode)
        {
            currentTarget = nearestObject.transform;

            // Move towards the nearest object
            MoveTowardsTarget(nearestObject);
        }


    }

    GameObject FindNearestObject()
    {
        GameObject nearestObject = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in enemiesList)
        {
            float distanceToObj = Vector3.Distance(obj.transform.position, currentPosition);
            if (distanceToObj < shortestDistance)
            {
                shortestDistance = distanceToObj;
                nearestObject = obj;
            }
        }

        return nearestObject;
    }

    void MoveTowardsTarget(GameObject nearestObject)
    {
        if (currentTarget == null)
            return;

        Enemy enemyObject = nearestObject.GetComponent<Enemy>();
        // Calculate direction to move towards the target
        Vector3 moveDirection = (currentTarget.position - transform.position).normalized;

        // Determine movement along X and Y axes
        float moveX = Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y) ? moveDirection.x : 0;
        float moveY = Mathf.Abs(moveDirection.y) > Mathf.Abs(moveDirection.x) ? moveDirection.y : 0;

        // Set animator parameters based on movement direction
        alimAnimator.SetBool("UpMovement", moveY > 0);
        alimAnimator.SetBool("DownMovement", moveY < 0);
        alimAnimator.SetBool("RightMovement", moveX > 0);
        alimAnimator.SetBool("LeftMovement", moveX < 0);

        // Move the character
        transform.position += moveDirection * speed * Time.deltaTime;

        // Check distance to target
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

        // Check if distance is less than or equal to a certain threshold
        float combatDistanceThreshold = 2.0f; // Adjust this distance threshold as needed

        if (distanceToTarget <= combatDistanceThreshold)
        {
            // Determine if the target is on the left or right side
            bool isTargetOnLeft = moveDirection.x < 0;
            bool isTargetOnRight = moveDirection.x > 0;

            // Set combat animation based on target's position relative to the character
            alimAnimator.SetBool(isTargetOnLeft ? "AttackStateLeft" : "AttackStateRight", true);
            enemyObject.TakeDamage(damage);
            if (!nearestObject.active)
            {
                killedEnemies.Add(nearestObject);
            }
            if (killedEnemies.Count != 0)
            {
                EnemiesCollection.removeEnemies(killedEnemies);
            }
        }
        else
        {
            // Ensure combat animations are off when not in combat range
            alimAnimator.SetBool("AttackStateLeft", false);
            alimAnimator.SetBool("AttackStateRight", false);
        }
    }


}
