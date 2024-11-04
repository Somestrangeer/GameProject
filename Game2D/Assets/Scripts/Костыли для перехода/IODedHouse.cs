using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IODedHouse : MonoBehaviour
{
    GameObject houseInForest;
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        houseInForest = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hero.getHero() != null) 
        {
            Debug.Log(Vector2.Distance(Hero.getHero().transform.position, houseInForest.transform.position));
            if(Vector2.Distance(Hero.getHero().transform.position, houseInForest.transform.position) < 4) 
            {
                myButton.gameObject.SetActive(true);
            }
            else 
            {
                myButton.gameObject.SetActive(false);
            }
        }
    }
}
