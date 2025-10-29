using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] Audio_Manager Audio_Manager;
    Change_Scene Change_Scene;

    public GameObject Setting_Menu;
    public GameObject Close_button;
    public GameObject Resume_button;
    public GameObject To_Main_Menu_button;
    public TMP_Text Best_time_Text;
    public Canvas Main_Menu;
    public float Best_time = 0; // int.MaxValue

    //public bool disable_pause = true;
    //[SerializeField] TMP_Text Sensitivity_num;
    //public Slider Sensitivity_Slider;
    //public Slider FOV_Slider;
    //public GameObject Setting_button;


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
        if (Input.GetKeyDown(KeyCode.P)) // && disable_pause == false (pause
        {
            Setting_Menu.gameObject.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Audio_Manager.SFX_Audio_Source.Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Setting_Menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Audio_Manager.Play_SFX(Audio_Manager.Button_Pressed);
        Audio_Manager.SFX_Audio_Source.Play();
    }

    public void To_Main_Menu()
    {
        //Game_Controller.GetComponent<Game_Controller>().Best_time_Text.gameObject.SetActive(true);
        //Game_Controller.GetComponent<Game_Controller>().Best_time_Text.text = "Best Time: " + best_time.ToString("F2");
        Audio_Manager.Play_SFX(Audio_Manager.Button_Pressed);
        Setting_Menu.SetActive(false);
        Change_Scene = GameObject.Find("Change_Scene").GetComponent<Change_Scene>();
        Change_Scene.Scene_To_Load("Main_Menu");
    }

    public void Close_Settings()
    {
        Setting_Menu.SetActive(false);
        //Setting_button.SetActive(true);
        Audio_Manager.Play_SFX(Audio_Manager.Button_Pressed);
    }

    //public void On_Val_Changed(TMP_Text Val_Text, Slider Slider)
    //{
    //    Val_Text.text = Slider.value.ToString();
    //}

    //public void Update_Setting_Num(TMP_Text Val_Text)
    //{
    //    Val_Text.text = GetComponent<Slider>().value.ToString("F0");
    //}


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
