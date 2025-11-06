using TMPro;
using UnityEngine;
//using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject End_Screen;
    [SerializeField] Player_Movement Player_Movement;
    //[SerializeField] InputActionAsset input_actions;

    [Header("Text")]
    [SerializeField] TMP_Text Collectable_Text;
    [SerializeField] TMP_Text Timer_Text;
    [SerializeField] TMP_Text Final_Time_Text;
    [SerializeField] TMP_Text Best_time_Text;
    [SerializeField] TMP_Text Best_time_end_Text;
    
    public GameObject Door;

    Game_Controller Game_Controller;
    Audio_Manager Audio_Manager;

    Rigidbody rb;

    public float Collectable_remaining = 2;
    float Timer = 0;

    Vector3 new_room_trigger_pos;
    //public float best_time = 0;
    //bool disable_pause = false;

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
            Best_time_Text.text = "Best Time: N/A";

        //Game_Controller.disable_pause = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //move_input = input_actions.FindAction("Move");
    }

    void Update()
    {
        Other_Actions();

        Timer += Time.deltaTime;
        Timer_Text.text = Timer.ToString("F2");

        //if (transform.position == new Vector3(0,1,0))
        //    Timer = 0;
    }

    private void Other_Actions()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            transform.position = new_room_trigger_pos;

        else if (Input.GetKeyDown(KeyCode.Alpha9)) // win (dev
            transform.position = new(0, 2, 210); 

        else if(Input.GetKeyDown(KeyCode.Q))
            transform.position = (transform.position + Cam.gameObject.transform.forward * 10); // Dev        

        //if (Input.GetKeyDown(KeyCode.P) && disable_pause == false) // pause 
        //{
        //    Pause_Menu.gameObject.SetActive(true);
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //    Time.timeScale = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.Mouse1) && dash_cool <= 0)
        //{
        //    rb.AddForce(Cam.gameObject.transform.forward * dash_force, ForceMode.Impulse); // dash
        //    dash_cool = 3;
        //    Speedlines.SetActive(true);
        //    Speedlines_timer = 0.5f;
        //}
        //else if (dash_cool > 0)
        //{
        //    dash_cool -= Time.deltaTime;
        //    Speedlines_timer -= Time.deltaTime;
        //    Dash_cool_Text.text = "Dash Cooldown: " + dash_cool.ToString("F0"); // F3
        //    if (Speedlines_timer <= 0)
        //        Speedlines.SetActive(false);
        //}
        //else if (dash_cool < 0)
        //    dash_cool = 0;


        //if (Input.GetKey(KeyCode.Space) && is_grounded == true) // jump
        //{
        //    rb.AddForce(Vector3.up * jump_force);
        //    //is_grounded = false;
        //}
        //if (rb.linearVelocity.y == 0)
        //    is_grounded = true;
        //else
        //    is_grounded = false;

        //is_grounded = Physics.Raycast(transform.position, Vector3.down, 1 * 0.5f + 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            Collectable_remaining -= 1;
            Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
            other.gameObject.SetActive(false);
            Audio_Manager.Play_SFX_One_Shot(Audio_Manager.Collecting);
            if (Collectable_remaining <= 0)
                Door.GetComponent<Door>().enabled = true;
        }
        else if (other.gameObject.tag == "New_Room")
        {
            new_room_trigger_pos = other.transform.position;
            Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
            other.gameObject.SetActive(false);
            //move_door = false;
        }
        else if (other.gameObject.tag == "Death")
        {
            transform.position = new_room_trigger_pos;
            rb.linearVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Bounce_Pad")
        {
            rb.AddForce(Vector3.up * Player_Movement.jump_force * 2, ForceMode.Impulse);
        }
        else if (other.gameObject.name == "Win_Trigger")
        {
            End_Screen.SetActive(true);
            Final_Time_Text.text = "Final Time: " + Timer.ToString("F2");
            other.gameObject.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Audio_Manager.Play_Music(Audio_Manager.Win_OST);
            Time.timeScale = 0;
            if (Timer < Game_Controller.Best_time || Game_Controller.Best_time == 0)
            {
                Game_Controller.Best_time = Timer;
                Best_time_end_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
                Game_Controller.Best_time_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
            }
            else
                Best_time_end_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
            //Game_Controller.disable_pause = true;
            //Audio_Manager.Play_SFX(Audio_Manager.Win);
        }
    }

    //private void Unused()
    //{
    //    //if (Input.GetKey(KeyCode.W))
    //    //{
    //    //    transform.position = (rb.transform.forward * Time.deltaTime);
    //    //    transform.position = Vector3.up * Time.deltaTime;
    //    //}

    //    //if (mud_timer > 0)
    //    //{
    //    //    mud_timer -= Time.deltaTime;
    //    //    is_grounded = false;
    //    //    //if (mud_timer <= 0)
    //    //    //{
    //    //    //    mud_timer = 1;
    //    //    //    can_jump = true;
    //    //    //} 
    //    //}

    //    // Normalize speed to a 0-1 ratio
    //    //float speed_ratio = Mathf.InverseLerp(min_speed, max_speed, current_speed);

    //    //// Calculate the target FOV based on the speed ratio
    //    //float target_FOV = Mathf.Lerp(min_FOV, max_FOV, speed_ratio);

    //    //Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, target_FOV, Time.deltaTime * FOV_change_speed);

    //    //if (move_door)
    //    //{
    //    //    Door.GetComponent<Door>().enabled = true;
    //    //    //Door.transform.position += Vector3.up * Time.deltaTime * 2;
    //    //    //Door_opening_sfx.Play();
    //    //    //if (Door.transform.position.y >= 6.5f)
    //    //    //{
    //    //    //    move_door = false;
    //    //    //}
    //    //}
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //if (other.gameObject.name == "Speed_Area")
    //    //{
    //    //    rb_move_speed /= 2;
    //    //}
    //    //if (other.gameObject.name == "Mud")
    //    //{
    //    //    move_speed *= 6;
    //    //}
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Floor")
    //    {
    //        is_grounded = true;
    //    }
    //}

    //private void New_Input_Move()
    //{
    //    Debug.Log(move_input.ReadValue<Vector2>() + "move_input.ReadValue<Vector2>()");
    //    dir = move_input.ReadValue<Vector2>();
    //    Vector2 move_amount = dir * move_speed * Time.deltaTime;
    //    transform.position = new Vector3(transform.position.x + move_amount.x, transform.position.y, transform.position.z + move_amount.y);

    //}
}
