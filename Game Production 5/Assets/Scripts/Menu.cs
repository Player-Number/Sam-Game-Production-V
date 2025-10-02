using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //[SerializeField] 
    public TMP_Text Best_time_Text;
    public Canvas Main_Menu;

    //GameObject Pause_Menu;
    //[SerializeField] GameObject Main_Menu;
    //[SerializeField] GameObject Player_UI;
    //[SerializeField] GameObject End_Screen;
    //[SerializeField] GameObject Player;

    //public bool disable_pause = true;
    //bool In_main_menu = true;

    public float Best_time = 0;

    public static Menu Instance { get; private set; }

    void Start()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }

        //DontDestroyOnLoad(gameObject);
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        //Time.timeScale = 0;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P) && disable_pause == false)
        //{
        //    Pause_Menu.gameObject.SetActive(true);
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //    Time.timeScale = 0;
        //}
        //if (In_main_menu)
        //{
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}

        //if (Pause_Menu == null)
        //{
        //    Pause_Menu = GameObject.Find("Panel_Pause");
        //}
    }

    public void To_Game()
    {
        //Player_UI.SetActive(true);
        //Main_Menu.gameObject.SetActive(false);
        //disable_pause = false;
        //In_main_menu = false;
        //Time.timeScale = 1;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        //Best_time_Text.gameObject.SetActive(false);
        SceneManager.LoadScene("Game_Scene");
    }

    //public void To_Main_Menu()
    //{
    //    //Time.timeScale = 0;
    //    //Player.transform.position = new(0,1,0);
    //    //Player_UI.SetActive(false);
    //    //Pause_Menu.SetActive(false);
    //    //End_Screen.SetActive(false);
    //    //disable_pause = true;
    //    //In_main_menu = true;
    //    //Main_Menu.SetActive(true);

    //    SceneManager.LoadScene("Main_Menu");
    //    Best_time_Text.gameObject.SetActive(true);
    //    Best_time_Text.text = "Best_Time: " + Best_time.ToString("F2");
    //}

    //public void Resume()
    //{
    //    Time.timeScale = 1.0f;
    //    Pause_Menu.SetActive(false);
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    public void Quit()
    {
        Application.Quit();
    }
}
