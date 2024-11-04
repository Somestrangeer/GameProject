using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneTrigger : MonoBehaviour
{
    GameObject hero;
    GameObject obj;

    [SerializeField] public PlayableDirector timelineDirector;
    [SerializeField] public GameObject hidden;

    private bool IsPlayed = false;

    private void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        obj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Cave")
        {
            if (EnemiesCollection.getEnemyCollection().Count == 0 && !IsPlayed) 
            {
                //Debug.Log(Vector3.Distance(hero.transform.position, obj.transform.position));
                if (Vector3.Distance(hero.transform.position, obj.transform.position) <= 1.5f) 
                {
                    timelineDirector.Play();
                    IsPlayed = true;

                }
                    
            }
        }
        else 
        {
            Debug.Log(Vector3.Distance(hero.transform.position, obj.transform.position));
            if (Vector3.Distance(hero.transform.position, obj.transform.position) <= 5f)
            {
                if(Hero.weReady && hidden != null)
                    hidden.SetActive(true);
                timelineDirector.Play();
            }
        }
        
    }
}
