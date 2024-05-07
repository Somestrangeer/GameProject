using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTemple : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject hero;
    GameObject door;
    Animator doorAnima;

    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        door = GameObject.FindGameObjectWithTag("Door");

        doorAnima = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E)) 
        {
            Debug.Log("Here1");
            if (Vector3.Distance(hero.transform.position, door.transform.position) < 6) 
            {
                Debug.Log("Here2");
                doorAnima.SetBool("openDoor", true);
                BoxCollider2D boxCollider = door.GetComponentInChildren<BoxCollider2D>();
                boxCollider.enabled = false;


                AudioManager.instance.Play("Door");
            }
        }
        
    }
}
