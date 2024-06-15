using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    [SerializeField] public GameObject objectToShow;
    [SerializeField] public List<GameObject> hideObjects;


    GameObject thisObj;
    GameObject hero;

    // Start is called before the first frame update
    void Awake()
    {
        thisObj = gameObject;
        hero = GameObject.FindGameObjectWithTag("Hero");

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(thisObj.transform.position, hero.transform.position) <= 2f && Input.GetKey(KeyCode.E)) 
        {
            objectToShow.SetActive(true);
            foreach (var item in hideObjects)
            {
                item.SetActive(false);
            }
        }
    }
}
