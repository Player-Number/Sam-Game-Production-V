using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    Game_Controller Game_Controller;
    Audio_Manager Audio_Manager;
    //string scene_name;
    //float delay_load_timer = 0.25f;
    //bool to_delay_load = false;

    void Start()
    {
        Game_Controller = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        Audio_Manager = GameObject.Find("Audio_Manager").GetComponent<Audio_Manager>();
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
        Audio_Manager.Play_SFX(Audio_Manager.Button_Pressed);
        if (name == "How_Play" || name == "Controls")
        {
            Audio_Manager.Play_Music(Audio_Manager.Other_Menu);
            Game_Controller.Best_time_Text.gameObject.SetActive(false);
            UI_Not_In_Game();
        }
        else if (name == "Main_Menu")
        {
            Audio_Manager.Stop_Music();
            Audio_Manager.Play_Music(Audio_Manager.Main_Menu);
            Game_Controller.Best_time_Text.gameObject.SetActive(true);
            UI_Not_In_Game();
        }
        else if (name == "Game_Scene")
        {
            Audio_Manager.Play_Music(Audio_Manager.Gameplay);
            Game_Controller.Best_time_Text.gameObject.SetActive(false);
            UI_In_Game();
        }
        SceneManager.LoadScene(name);

        //else if (name != "Main_Menu")
        //{
        //    Game_Controller.Best_time_Text.gameObject.SetActive(false);
        //    Game_Controller.Close_button.SetActive(false);
        //    Game_Controller.Resume_button.SetActive(true);
        //    Game_Controller.To_Main_Menu_button.SetActive(true);
        //    Game_Controller.disable_pause = false;
        //}
        //else
        //{
        //    Game_Controller.Best_time_Text.gameObject.SetActive(false);
        //    Game_Controller.Close_button.SetActive(true);
        //    Game_Controller.Resume_button.SetActive(false);
        //    Game_Controller.To_Main_Menu_button.SetActive(false);
        //    Game_Controller.disable_pause = true;
        //}
        //to_delay_load = true;
    }

    public void Quit()
    {
        Audio_Manager.Play_SFX(Audio_Manager.Button_Pressed);
        Application.Quit();
    }

    public void Open_Settings()
    {
        Game_Controller.Setting_Menu.SetActive(true);
        Audio_Manager.Play_SFX(Audio_Manager.Button_Pressed);
    }

    void UI_Not_In_Game()
    {
        Game_Controller.Close_button.SetActive(true);
        Game_Controller.Resume_button.SetActive(false);
        Game_Controller.To_Main_Menu_button.SetActive(false);
        //Game_Controller.disable_pause = true;
    }

    void UI_In_Game()
    {
        Game_Controller.Close_button.SetActive(false);
        Game_Controller.Resume_button.SetActive(true);
        Game_Controller.To_Main_Menu_button.SetActive(true);
        //Game_Controller.disable_pause = false;
    }
}
