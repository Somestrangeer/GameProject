using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using Cinemachine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class Hero : MonoBehaviour
{
    public GameObject HealtBar; //�����

    private static float hp = 1120;
    private float damage = 50f;
    private int speed = 3;
    private float attackDistance = 2.0f;
    private bool sneakModeEnable = false;
    private static bool battleModeEnable = false;
    public float damageSpeed = 0.2f;

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
    private static Slider heroSlider; //�����

    public static GameObject getHero() { return hero; }
    public float getHp() { return hp; }
    public static void setHp(float hp1) {  hp = hp1; }

    public static string sceneName = "";

    private CinemachineVirtualCamera virtualCamera;
    private GameObject objCameraSize;
    public float targetOrthographicSize = 12f;
    public float smoothTime = 0.5f;
    private float velocity = 0f;
    private float currentOrthographicSize = 8.108678f;
    private bool isAttacking = false;

    public static string gameProgress;

    private static bool firstSave = false;

    public static bool weReady = false;

    [SerializeField] public GameObject darkShield1;
    public static GameObject darkShield;

    private SaveSystem save = new SaveSystem();

    private void Awake()
    {
        // Get the object of hero's sprite
        darkShield = darkShield1;
        hero = gameObject;

        sceneName = SceneManager.GetActiveScene().name;

        /*if (SaveData.sceneName != sceneName)
        {
            // Переходим на нужную сцену асинхронно
            SceneManager.LoadSceneAsync(SaveData.sceneName);
            Debug.Log("Name her" + sceneName);

        }*/

        /*Не хочу заморачиваться*/
       /* if (SaveData.position.x != 0 && SaveData.position.y != 0) 
        {
            hero.transform.position = SaveData.position;
        }
        if (SaveData.health != 0f) 
        {
            hp = SaveData.health;
        }
        if (SaveData.gameProgress != null) 
        {
            gameProgress = SaveData.gameProgress;
        }*/

        heroAnima = hero.GetComponent<Animator>();

        heroSlider = HealtBar.GetComponent<Slider>(); //�����

        //heroSlider.value = hp;

        SpriteRenderer[] spriteRenderers = hero.GetComponentsInChildren<SpriteRenderer>();
        shadowUpDown = spriteRenderers[1].gameObject;
        shadowLeft = spriteRenderers[2].gameObject;
        shadowRight = spriteRenderers[3].gameObject;



        if(sceneName == "TempleInterior") 
        {
            objCameraSize = GameObject.FindGameObjectWithTag("SizeCamera");
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }



        SaveData data = save.Load();

        if(data.health != 0) 
        {
            hp = data.health;
            hero.transform.position = data.coordinates;

            /*if(sceneName == "Village" && data.visited) 
            {
                var k = GameObject.FindWithTag("DisableThis");
            }*/
            if (data.visited) 
            {
                var cutScene = GameObject.FindWithTag("CutScene");
                if (cutScene != null)
                {
                    cutScene.SetActive(false);
                    var cam = GameObject.FindWithTag("AdditionalCamera");
                    if (cam != null)
                        cam.SetActive(false);
                    /*var audio = GameObject.FindWithTag("AudioManager");
                    if (audio != null)
                        audio.SetActive(false);*/
                }
            }
        }

        /*if (!firstSave)
        {
            SaveData data1 = save.Load();

            MakeSave(data1.talked);
            firstSave = true;
        }*/


    }

    /**/
    /*async void Start()
    {
        

        if (SaveData.sceneName != SceneManager.GetActiveScene().name)
        {
            // Переходим на нужную сцену асинхронно
            SceneManager.LoadSceneAsync(SaveData.sceneName);
            hero.transform.position = SaveData.position;
            hp = SaveData.health;
            gameProgress = SaveData.gameProgress;

        }
    }*/

    // Update is called once per frame
    private void Update()
    {

        /*var audio = GameObject.FindWithTag("AudioManager");
        if (audio != null)
        {
            Debug.Log("HERE");
            //audio.SetActive(false);
            //audio.SetActive(true);
        }*/


        /* var cam = GameObject.FindWithTag("AdditionalCamera");
         if (cam != null)
         {
             Debug.Log("dfd");
             if (cam.active == false)
             {
                 Debug.Log("dfd1");
                 var audio = GameObject.FindWithTag("AudioManager");
                 if (audio != null)
                     audio.SetActive(false);
             }
         }*/


        if (hp <= 10 && sceneName == "Village") 
        {
            //GlobalLightDim.globalLight.intensity -= 0.07f * Time.deltaTime;

        }

        //healthBar.SetHealth(hp);

        heroMovement();

        if (Input.GetMouseButtonDown(0) && EnemiesCollection.attackMode && !isAttacking)
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

        if (sceneName == "TempleInterior") 
        {
            resizeCamera();
        }
        

        if (movement != Vector3.zero) 
        {
            Footstep.Instance.PlaySound(0.3f);
        }

        // To make an animation we have to multiply it by time
        hero.transform.position += movement * Time.deltaTime;
    }
    private void resizeCamera() 
    {
        if (Vector3.Distance(objCameraSize.transform.position, hero.transform.position) <= 10)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, targetOrthographicSize, ref velocity, smoothTime);
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, currentOrthographicSize, ref velocity, smoothTime);
            //virtualCamera.m_Lens.OrthographicSize = currentOrthographicSize;
        }
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
    private IEnumerator Attack(GameObject enemy, Enemy enemyObject)
    {
        if (isAttacking) yield break;

        Renderer rend = enemy.GetComponent<Renderer>();

        isAttacking = true;

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

        AttackSound.Instance.PlaySoundAttack(0.3f);//��������������� ����� �����

        //Store enemy as killed if he's not active (killed)
        if (!enemy.active)
        {
            killedEnemies.Add(enemy);
        }

        //The hero was hit
        rend.material.color = Color.red;
        //Hero.TakeDamage(damage);

        yield return new WaitForSeconds(damageSpeed);

        rend.material.color = Color.white;

        yield return new WaitForSeconds(damageSpeed);

        isAttacking = false;
    }
    private void attackEnemy()
    {
        List<GameObject> enemyList = EnemiesCollection.getEnemyCollection();


        //Sort the list of enemis by distance to the hero
        enemyList.Sort((a, b) =>
        {
            // Проверка на null для первого объекта
            if (a == null && b == null) return 0; // Оба null - считаем равными
            if (a == null) return 1; // a null, b не null - a идет после b
            if (b == null) return -1; // b null, a не null - a идет перед b

            // Если оба объекта не null, сравниваем их расстояния
            return Vector3.Distance(hero.transform.position, a.transform.position)
                .CompareTo(Vector3.Distance(hero.transform.position, b.transform.position));
        });

        foreach (GameObject enemy in enemyList) 
        {
            if (enemy != null) 
            {
                // Calculate the distance between the hero and an enemy
                if (Vector3.Distance(hero.transform.position, enemy.transform.position) - 1.2f <= attackDistance) 
                {
                    // We use the interface to interact with an enemy
                    Enemy enemyObject = enemy.GetComponent<Enemy>();
                    StartCoroutine(Attack(enemy, enemyObject));

                  

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
            // Ïðîâåðÿåì, íå èãðàåò ëè óæå ìóçûêà áîÿ
            if (!AudioManager.instance.isPlaying("battleMusic"))
            {
                heroAnima.SetBool("AttackStateRight", true);
                AudioManager.instance.PlayBattleMusic();
            }
            

        }
        if (!battleModeEnable)
        {
            if (!AudioManager.instance.isPlaying("Background"))
            {
                if(hero == null) 
                {
                    hero = GameObject.FindWithTag("Hero");
                    heroAnima = hero.GetComponent<Animator>();
                }
                heroAnima.SetBool("AttackStateLeft", false);
                heroAnima.SetBool("AttackStateRight", false);
                AudioManager.instance.PlayNormalMusic();

            }
            
        }
    }
    
    public static void TakeDamage(float damage)
    {
        hp -= damage;

        heroSlider.value = Hero.hp;

        if (hp <= 0.0f)
        {
            Die();
        }
    }



    /*Swith to another cutscene*/
    public void ReplacementStart()
    {
        StartCoroutine(Replace());
    }
    private static IEnumerator Replace()
    {
        Debug.Log("HERE");
        if(darkShield != null) 
        {
            darkShield.SetActive(true);

        }

        yield return new WaitForSeconds(3f); // Ждем 3 секунды после гашения света
        if (darkShield != null)
        {
            darkShield.SetActive(false);

        }
        battleModeEnable = false;
        EnemiesCollection.attackMode = false;
        Hero.hero.transform.position = new Vector3(54.5f, -50f, 0);


        weReady = true;
    }

    public static void MakeSave(List<string> talkedWith) 
    {
        SaveSystem save = new SaveSystem();
        List<string> talked = talkedWith;
        /*if (talked.Contains("Grandfather")) 
        {
            save.Save(SceneManager.GetActiveScene().name, hp, true, new Vector3(51.01f, -5.72f, 0), talked);
            return;
        }*/
        save.Save(SceneManager.GetActiveScene().name, hp, true, hero.transform.position, talked);
    }

    public void MakeSaveNonStatic()
    {
        SaveSystem save = new SaveSystem();
        SaveData saveData = save.Load();
        List<string> talked = saveData.talked;
        /*if (talked.Contains("Grandfather")) 
        {
            save.Save(SceneManager.GetActiveScene().name, hp, true, new Vector3(51.01f, -5.72f, 0), talked);
            return;
        }*/
        save.Save(SceneManager.GetActiveScene().name, hp, true, hero.transform.position, talked);
    }

    public static void MakeSpecficSave(SaveData data)
    {
        SaveSystem save = new SaveSystem();
        
        save.Save(data.sceneName, hp, data.visited, hero.transform.position, data.talked);
    }

    private static void Die()
    {
        // We hide the hero
        /*battleModeEnable = false;
        Renderer rend = hero.GetComponent<Renderer>();
        rend.material.color = Color.white;
        heroAnima.SetBool("AttackStateRight", false);*/

        /*SaveData.health = 300f;
        SaveData.position = hero.transform.position;
        SaveData.gameProgress = gameProgress;
        SaveData.sceneName = sceneName;*/

        hero.SetActive(false);

        EnemiesCollection.attackMode = false;

        /*if (sceneName == "Village") 
        {
            hp = 50f;
            hero.SetActive(true);
            hero.transform.position = new Vector3(54.5f, -50f, 0);
            //EnemiesCollection.attackMode = true;
            //GlobalLightDim.globalLight.intensity += 0.07f * Time.deltaTime;
            

        }*/

    }
}
