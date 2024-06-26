using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestToHouse : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject hero;
    GameObject door;
    bool DoorOpened = false;


    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        door = GameObject.FindGameObjectWithTag("Door");
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        string sceneName = "GrandfatherHouse";

        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("Here1");
            if (Vector3.Distance(hero.transform.position, door.transform.position) < 6)
            {
                Debug.Log("Here2");
                BoxCollider2D boxCollider = door.GetComponentInChildren<BoxCollider2D>();
                boxCollider.enabled = false;

                if (!DoorOpened)
                {
                    SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                    bool h = SceneManager.UnloadScene("ForestScene");

                    DoorOpened = true;
                }
            }
        }

    }
}
