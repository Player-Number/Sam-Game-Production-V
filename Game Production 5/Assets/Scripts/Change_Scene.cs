using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    [SerializeField] AudioSource button_pressed;
    Game_Controller Game_Controller;
    string scene_name;
    float delay_load_timer = 0.25f;
    bool to_delay_load = false;

    void Start()
    {
        Game_Controller = GameObject.Find("Game_Controller").gameObject.GetComponent<Game_Controller>();
    }

    void Update()
    {
        //if (to_delay_load)
        //{
        //    delay_load_timer -= Time.deltaTime;
        //    if (delay_load_timer <= 0)
        //    {
        //        SceneManager.LoadScene(name);
        //        if (name != "Main_Menu")
        //        {
        //            Menu.Best_time_Text.gameObject.SetActive(false);
        //        }
        //        else
        //            Menu.Best_time_Text.gameObject.SetActive(true);
        //    }
        //}
    }

    public void Scene_To_Load(string name)
    {
        button_pressed.Play();
        SceneManager.LoadScene(name);
        if (name != "Main_Menu")
        {
            Game_Controller.Best_time_Text.gameObject.SetActive(false);
            Game_Controller.Close_button.SetActive(false);
            Game_Controller.Resume_button.SetActive(true);
            Game_Controller.To_Main_Menu_button.SetActive(true);
            Game_Controller.disable_pause = false;
        }
        else
        {
            Game_Controller.Best_time_Text.gameObject.SetActive(false);
            Game_Controller.Close_button.SetActive(true);
            Game_Controller.Resume_button.SetActive(false);
            Game_Controller.To_Main_Menu_button.SetActive(false);
            Game_Controller.disable_pause = true;
        }
        //to_delay_load = true;
    }

    public void Quit()
    {
        button_pressed.Play();
        Application.Quit();
    }

    public void Open_Settings()
    {
        Game_Controller.Setting_Menu.SetActive(true);
        button_pressed.Play();
    }

}
