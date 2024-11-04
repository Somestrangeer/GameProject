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
        SaveData pre = saveSystem.Load();
        SaveData saveData = new SaveData
        {
            sceneName = "ForestNewScene 1",
            health = 0,
            visited = true, /*two cutscenes*/
            coordinates = new Vector3(51.01f, -5.72f, 0),
            talked = (pre.talked.Count < 1) ? new List<string>() : pre.talked
        };
        Hero.MakeSpecficSave(saveData);
    }

    public void NextLvlButton1()
    {
        Hero.getHero().transform.position = new Vector3(-9.52f, -1.22f, 0);

        SceneManager.LoadScene(2); //grandfather
        SaveData pre = saveSystem.Load();
        SaveData saveData = new SaveData
        {
            sceneName = "GrandfatherHouse",
            health = 0,
            visited = true, /*two cutscenes*/
            coordinates = new Vector3(-9.52f, -1.22f, 0),
            talked = (pre.talked.Count < 1) ? new List<string>() : pre.talked
        };
        Hero.MakeSpecficSave(saveData);
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
        Hero.getHero().transform.position = new Vector3(5.84f, 12.8f, 0);
        SceneManager.LoadScene(4); //cave
        SaveData pre = saveSystem.Load();
        SaveData saveData = new SaveData
        {
            sceneName = "Cave",
            health = 300,
            visited = true, /*two cutscenes*/
            coordinates = new Vector3(5.84f, 12.8f, 0),
            talked = (pre.talked.Count < 1) ? new List<string>() : pre.talked
        };
        Hero.MakeSpecficSave(saveData);
    }

    public void NextLvlButton4()
    {
        Hero.getHero().transform.position = new Vector3(0.5f, -40f, 0);
        SceneManager.LoadScene(5);//extr temple
        SaveData pre = saveSystem.Load();
        SaveData saveData = new SaveData
        {
            sceneName = "TempleExterior",
            health = 100,
            visited = false, /*two cutscenes*/
            coordinates = new Vector3(0.5f, -40f, 0),
            talked = (pre.talked.Count < 1) ? new List<string>() : pre.talked
        };
        Hero.MakeSpecficSave(saveData);
    }

    public void NextLvlButton5()
    {
        Hero.getHero().transform.position = new Vector3(-9.49f, -36.5f, 0);
        SceneManager.LoadScene(6);//inter temple
        SaveData pre = saveSystem.Load();
        SaveData saveData = new SaveData
        {
            sceneName = "TempleInterior",
            health = 100,
            visited = false, /*two cutscenes*/
            coordinates = new Vector3(-9.49f, -36.5f, 0),
            talked = (pre.talked.Count < 1) ? new List<string>() : pre.talked
        };
        Hero.MakeSpecficSave(saveData);
    }

    public void ExitButton()
    {
        Application.Quit(); //Работает только после компиляции (по крайней мере так было сказано в гайдах)
    }
}
