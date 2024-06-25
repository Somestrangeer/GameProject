using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField]
    GameObject Escmenu;
    [SerializeField]
    GameObject DieMenu;
    [SerializeField]
    GameObject hero;


    // Start is called before the first frame update
    void Start()
    {
        Escmenu.SetActive(false);
        DieMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Escmenu.SetActive(true);
            DieMenu.SetActive(true) ;
            //Time.timeScale = 0;
        }
        if (hero.active == false)
        {
            DieMenu.SetActive(true) ;
        }
    }

    public void EscMenuOff()
    {
        Escmenu.SetActive(false);
        //Time.timeScale = 0;
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        //Time.timeScale = 0;
    }

    public void ExitButton()
    {
        Application.Quit(); //Работает только после компиляции (по крайней мере так было сказано в гайдах)
    }
}
