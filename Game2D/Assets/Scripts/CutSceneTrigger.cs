using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    GameObject hero;
    GameObject obj;

    [SerializeField] public PlayableDirector timelineDirector;

    private void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        obj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(hero.transform.position, obj.transform.position) <= 1.5f) 
        {
            timelineDirector.Play();
        }
    }
}
