using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private SaveSystem saveSystem;
    // Start is called before the first frame update
    void Start()
    {
        Escmenu.SetActive(false);
        DieMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (saveSystem == null) 
        {
            saveSystem = new SaveSystem();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Escmenu.SetActive(true);
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
        SceneManager.LoadScene(0); //forest
        //Time.timeScale = 0;
    }

    public void Forest()
    {
        Hero.getHero().transform.position = new Vector3(51.01f, -5.72f, 0);
        SceneManager.LoadScene(1);
        Hero.MakeSave(saveSystem.Load().talked);
    }

    public void NextLvlButton1()
    {
        Hero.getHero().transform.position = new Vector3(-9.52f, -1.22f, 0);
        SceneManager.LoadScene(2); //grandfather
        Hero.MakeSave(saveSystem.Load().talked);
    }

    public void NextLvlButton2()
    {
        Hero.getHero().transform.position = new Vector3(0.34f, 10.28f, 0);
        SceneManager.LoadScene(3);
        SaveData saveData = new SaveData
        {
            sceneName = "Village",
            health = 0,
            visited = false, /*two cutscenes*/
            coordinates = new Vector3(0.34f, 10.28f, 0),
            talked = new List<string>()
        };
        Hero.MakeSpecficSave(saveData);
    }

    public void NextLvlButton3()
    {
        SceneManager.LoadScene(4);
    }

    public void NextLvlButton4()
    {
        SceneManager.LoadScene(5);
    }

    public void NextLvlButton5()
    {
        SceneManager.LoadScene(6);
    }

    public void ExitButton()
    {
        Application.Quit(); //Работает только после компиляции (по крайней мере так было сказано в гайдах)
    }
}
