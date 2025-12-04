using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera Gameplay_Cam;
    [SerializeField] Camera Win_Cam;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject End_Screen;
    [SerializeField] GameObject Gameplay_UI;
    [SerializeField] Player_Movement Player_Movement;
    [SerializeField] ParticleSystem Collected_Particle;
    [SerializeField] ParticleSystem To_Power_Door;
    [SerializeField] ParticleSystem Player_Death;
    //[SerializeField] InputActionAsset input_actions;

    public GameObject Door;

    Game_Controller Game_Controller;
    Audio_Manager Audio_Manager;

    Rigidbody rb;

    public float Collectable_remaining = 2;
    public float door_power = 2;
    float Timer = 0;

    Vector3 new_room_trigger_pos;
    [Header("Text")]
    [SerializeField] TMP_Text Collectable_Text;
    [SerializeField] TMP_Text Timer_Text;
    [SerializeField] TMP_Text Final_Time_Text;
    [SerializeField] TMP_Text Best_time_Text;
    [SerializeField] TMP_Text Best_time_end_Text;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        new_room_trigger_pos = transform.position;
        Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
        Time.timeScale = 1;
        Game_Controller = FindAnyObjectByType<Game_Controller>();
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        if (Game_Controller.Best_time != 0)
            Best_time_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
        else
            Best_time_Text.text = "Best Time: None";

        //move_input = input_actions.FindAction("Move");
    }

    void Update()
    {
        Other_Actions();

        Timer += Time.deltaTime;
        Timer_Text.text = Timer.ToString("F2");    
    }

    private void Other_Actions()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            transform.position = new(new_room_trigger_pos.x, new_room_trigger_pos.y + 1, new_room_trigger_pos.z);

        else if (Input.GetKeyDown(KeyCode.Alpha9)) // win
            transform.position = new(0, 2, 210);

        else if (Input.GetKeyDown(KeyCode.R))
            Door.GetComponent<Door>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Collect();
            other.gameObject.SetActive(false);
            Audio_Manager.Play_SFX_One_Shot(Audio_Manager.Collecting);
            ParticleSystem Collected_Particle_inst = Instantiate(Collected_Particle, other.transform.position, Quaternion.identity);
            if (!Collected_Particle_inst.isEmitting)
            {
                Destroy(Collected_Particle_inst);
            }
            ParticleSystem To_Power_Door_inst = Instantiate(To_Power_Door, other.transform.position, Quaternion.identity);
            To_Power_Door_inst.gameObject.GetComponent<To_Power_Door>().Door = Door;
        }
        else if (other.CompareTag("New_Room"))
        {
            new_room_trigger_pos = other.transform.position;
            Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
        }
        else if (other.CompareTag("Death"))
        {
            Player_Death.transform.position = transform.position;
            Player_Death.Play();
            transform.position = new_room_trigger_pos;
            rb.linearVelocity = Vector3.zero;
        }
        else if (other.gameObject.name == "Win_Trigger")
        {
            End_Screen.SetActive(true);
            Gameplay_UI.SetActive(false);
            Gameplay_Cam.gameObject.SetActive(false);
            Win_Cam.gameObject.SetActive(true);
            Final_Time_Text.text = "Final Time: " + Timer.ToString("F2");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Audio_Manager.Play_Music(Audio_Manager.Win_OST);
            Game_Controller.lock_mouse = false;
            if (Timer < Game_Controller.Best_time || Game_Controller.Best_time == 0)
            {
                Game_Controller.Best_time = Timer;
                Best_time_end_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
                Game_Controller.Best_time_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
                PlayerPrefs.SetFloat("Best_Time", Game_Controller.Best_time);
            }
            else
                Best_time_end_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
        }
    }

    public void Collect()
    {
        Collectable_remaining--;
        Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
    }
}
