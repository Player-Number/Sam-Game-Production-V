using TMPro;
using UnityEngine;

public class Delete_Best_Time : MonoBehaviour
{
    [SerializeField] TMP_Text timer_text;
    [SerializeField] GameObject del_box;
    Game_Controller controller;
    Audio_Manager Audio_Manager;
    float timer = 3;
    bool is_box_active = false;
    void Start()
    {
        controller = FindAnyObjectByType<Game_Controller>();
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Delete) && controller.Best_time != 0)
        //{
        //    timer -= Time.deltaTime;
        //    timer_text.text = timer.ToString("F2");
        //    timer_text.gameObject.SetActive(true);
        //    if (timer <= 0)
        //    {
        //        controller.Best_time = 0;
        //        PlayerPrefs.SetFloat("Best_Time", 0);
        //        controller.Best_time_Text.text = "Best Time: N/A";
        //        timer_text.gameObject.SetActive(false);
        //    }
        //}
        //else
        //{
        //    timer = 3;
        //    timer_text.gameObject.SetActive(false);
        //}

        if (Input.GetKeyDown(KeyCode.Delete) && controller.Best_time != 0)
        {
            //Audio_Manager.Play_SFX_Button_Pressed();
            if (!is_box_active)
            {
                del_box.SetActive(true);
                controller.can_open_setting = false;
                is_box_active = true;
            }
            else
            {
                No();
            }
        }

    }

    public void Yes()
    {
        controller.Best_time = 0;
        PlayerPrefs.SetFloat("Best_Time", 0);
        controller.Best_time_Text.text = "Best Time: None";
        del_box.SetActive(false);
        controller.can_open_setting = true;
        is_box_active = false;
        //Audio_Manager.Play_SFX_Button_Pressed();
    }
    public void No()
    {
        del_box.SetActive(false);
        controller.can_open_setting = true;
        is_box_active = false;
        //Audio_Manager.Play_SFX_Button_Pressed();
    }
}
