using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] AudioSource button_pressed;
    public GameObject Setting_Menu;
    public GameObject Close_button;
    public GameObject Resume_button;
    public GameObject To_Main_Menu_button;
    public TMP_Text Best_time_Text;
    public Canvas Main_Menu;
    public float Best_time = 0;
    public bool disable_pause = true;


    public static Game_Controller Instance { get; private set; }

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && disable_pause == false) // pause 
        {
            Setting_Menu.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Setting_Menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        button_pressed.Play();
    }

    public void To_Main_Menu()
    {
        //Game_Controller.GetComponent<Game_Controller>().Best_time_Text.gameObject.SetActive(true);
        //Game_Controller.GetComponent<Game_Controller>().Best_time_Text.text = "Best Time: " + best_time.ToString("F2");
        button_pressed.Play();
        Setting_Menu.SetActive(false);
        SceneManager.LoadScene("Main_Menu");
    }

    public void Close_Settings()
    {
        Setting_Menu.SetActive(false);
        button_pressed.Play();
    }

    //public void To_Game()
    //{
    //    button_pressed.Play();
    //    SceneManager.LoadScene("Game_Scene");
    //}

    //public void Quit()
    //{
    //    button_pressed.Play();
    //    Application.Quit();
    //}
}
