using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Alim : MonoBehaviour
{
    // Start is called before the first frame update

    private static float hp = 20;
    private float damage = 5f;
    private int speed = 2;
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

    private static List<GameObject> enemiesList = EnemiesCollection.getEnemyCollection();
    private Transform currentTarget;
    float bias = 0.0f;

    public static bool canMoveToEndPoint = false;

    public static void SetCanMove() { canMoveToEndPoint = true; }
    // Define the path points
    public Vector2 point1 = new Vector2(54.31f, -80.24f);
    public Vector2 point2 = new Vector2(78.85f, -80.24f);
    public Vector2 point3 = new Vector2(78.85f, -87.87f);
    public Vector2 point4 = new Vector2(110f, -87.87f);

    // Current position on the path
    private int currentPointIndex = 0;
    private bool reached = false;

    public static GameObject getAlim() { return alim; }
    void Start()
    {
        alim = gameObject;
        alimAnimator = alim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reached)
            return;
        //enemiesList = EnemiesCollection.getEnemyCollection();

        if (canMoveToEndPoint && SceneManager.GetActiveScene().name == "Village") 
        {
            // Get the current target point
            Vector2 targetPoint = GetCurrentTargetPoint();

            // Calculate direction to the target
            Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;

            // Move towards the target
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            // Check if we've reached the target point
            if (Vector2.Distance(transform.position, targetPoint) < 1f)
            {
                // Move to the next point
                currentPointIndex++;

                // If we've reached the last point, stop moving
                if (currentPointIndex >= 4)
                {
                    currentPointIndex = 3; // Stay at the final point
                    alimAnimator.SetBool("RightMovement", false);
                    reached = true;
                    return; // Stop moving
                }
            }
        }

    }

    // Helper function to get the current target point
    private Vector2 GetCurrentTargetPoint()
    {
        switch (currentPointIndex)
        {
            case 0:
                alimAnimator.SetBool("UpMovement", true);
                return point1;
            case 1:
                alimAnimator.SetBool("UpMovement", false);
                alimAnimator.SetBool("RightMovement", true);
                return point2;
            case 2:
                alimAnimator.SetBool("RightMovement", false);
                alimAnimator.SetBool("DownMovement", true);
                return point3;
            case 3:
                alimAnimator.SetBool("DownMovement", false);
                alimAnimator.SetBool("RightMovement", true);
                return point4;
            default:
                alimAnimator.SetBool("RightMovement", false);
                return point4; // Default to the last point
        }
    }



}
