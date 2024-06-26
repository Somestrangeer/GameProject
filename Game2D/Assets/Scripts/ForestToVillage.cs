using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestToVillage : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject hero;
    GameObject capsule;
    bool DoorOpened = false;


    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        capsule = GameObject.FindGameObjectWithTag("Capsule");
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        string sceneName = "Village";

        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("Here1");
            if (Vector3.Distance(hero.transform.position, capsule.transform.position) < 6)
            {
                Debug.Log("Here2");
                BoxCollider2D boxCollider = capsule.GetComponentInChildren<BoxCollider2D>();
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
