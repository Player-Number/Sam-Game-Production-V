using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text Collectable_Text;
    [SerializeField] TMP_Text Timer_Text;
    [SerializeField] TMP_Text Final_Timer_Text;
    [SerializeField] TMP_Text Dash_cool_Text;
    [SerializeField] TMP_Text Best_time_Text;
    [SerializeField] TMP_Text Best_time_end_Text;
    [SerializeField] Camera Cam;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Speedlines;
    [SerializeField] GameObject End_Screen;
    [SerializeField] InputActionAsset input_actions;
    
    [SerializeField] AudioSource Collect_sfx;
    [SerializeField] AudioSource Win_sfx;
    [SerializeField] AudioSource Gameplay_ost;

    public GameObject Door;

    GameObject Menu;

    Rigidbody rb;

    public float best_time = 0;
    public float Collectable_remaining = 2;
    float rb_move_speed = 500;
    float Timer = 0;
    float horizontal_move_cap = 4;
    float vertical_move_cap = 5;
    float dash_force = 150;
    float jump_force = 500;
    float dash_cool = 0;
    float Speedlines_timer = 0;

    bool move_door = false;
    bool is_grounded = true;
    bool disable_pause = false;

    Vector3 new_room_trigger_pos;

    //InputAction move_input;
    //Vector2 dir;

    [Header("FOV Settings")]
    public float max_speed = 10f; 
    public float min_FOV = 60f; 
    public float max_FOV = 90f; 
    public float FOV_change_speed = 5f; 
    public float current_FOV_velocity = 60f;
    public float smooth_time = 0.5f;
    //public float min_speed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        new_room_trigger_pos = transform.position;
        Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
        Dash_cool_Text.text = "Dash Cooldown: " + dash_cool.ToString("F0");
        Time.timeScale = 1;
        Menu = GameObject.Find("Menu");
        best_time = Menu.GetComponent<Menu>().Best_time;
        Best_time_Text.text = "Best Time " + Menu.GetComponent<Menu>().Best_time.ToString("F2");
        Menu.GetComponent<Menu>().Main_Menu.gameObject.SetActive(false);

        //move_input = input_actions.FindAction("Move");
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Other_Actions();
        Move_Cap();

        if (move_door)
        {
            Door.transform.position += Vector3.up * Time.deltaTime * 2;
            if (Door.transform.position.y >= 6.5f)
            {
                move_door = false;
            }
        }
        //if (transform.position == new Vector3(0,1,0))
        //    Timer = 0;

        Timer += Time.deltaTime;
        Timer_Text.text = Timer.ToString("F2");

        // FOV based on speed
        float current_speed = rb.linearVelocity.magnitude;
        float speed_normalized = Mathf.Clamp01(current_speed / max_speed);
        float target_FOV = Mathf.Lerp(min_FOV, max_FOV, speed_normalized);

        Cam.fieldOfView = Mathf.SmoothDamp(Cam.fieldOfView, target_FOV, ref current_FOV_velocity, smooth_time);

    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(rb.transform.forward * Time.deltaTime * rb_move_speed);
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-rb.transform.right * Time.deltaTime * rb_move_speed);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-rb.transform.forward * Time.deltaTime * rb_move_speed);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(rb.transform.right * Time.deltaTime * rb_move_speed);
    }

    private void Other_Actions()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            transform.position = new_room_trigger_pos;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            transform.position = new(0, 2, 200); // dev

        if (Input.GetKeyDown(KeyCode.Mouse1) && dash_cool <= 0)
        {
            rb.AddForce(Cam.gameObject.transform.forward * dash_force, ForceMode.Impulse); // dash
            dash_cool = 3;
            Speedlines.SetActive(true);
            Speedlines_timer = 0.5f;
        }
        else if (dash_cool > 0)
        {
            dash_cool -= Time.deltaTime;
            Speedlines_timer -= Time.deltaTime;
            Dash_cool_Text.text = "Dash Cooldown: " + dash_cool.ToString("F0"); // F3
            if (Speedlines_timer <= 0)
                Speedlines.SetActive(false);
        }
        //else if (dash_cool < 0)
        //    dash_cool = 0;

        if (Input.GetKey(KeyCode.Space) && is_grounded == true) // jump
        {
            rb.AddForce(Vector3.up * jump_force);
            //is_grounded = false;
        }
        if (rb.linearVelocity.y == 0)
            is_grounded = true;
        else
            is_grounded = false;

        if (Input.GetKeyDown(KeyCode.P) && disable_pause == false) // pause 
        {
            Pause_Menu.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
            transform.position = (transform.position + Cam.gameObject.transform.forward * 10); // Dev only
    }

    private void Move_Cap()
    {
        if (rb.linearVelocity.y >= vertical_move_cap) //rb.maxLinearVelocity
            rb.linearVelocity = new(rb.linearVelocity.x, vertical_move_cap, rb.linearVelocity.z);
        if (rb.linearVelocity.x >= horizontal_move_cap)
            rb.linearVelocity = new(horizontal_move_cap, rb.linearVelocity.y, rb.linearVelocity.z);
        if (rb.linearVelocity.x <= -horizontal_move_cap)
            rb.linearVelocity = new(-horizontal_move_cap, rb.linearVelocity.y, rb.linearVelocity.z);
        if (rb.linearVelocity.z >= horizontal_move_cap)
            rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y, horizontal_move_cap);
        if (rb.linearVelocity.z <= -horizontal_move_cap)
            rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y, -horizontal_move_cap);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            Collectable_remaining -= 1;
            Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
            other.gameObject.SetActive(false);
            Collect_sfx.Play();
            if (Collectable_remaining <= 0)
                move_door = true;
        }
        else if (other.gameObject.tag == "New_Room")
        {
            new_room_trigger_pos = other.transform.position;
            Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
            other.gameObject.SetActive(false);
            move_door = false;
        }
        else if (other.gameObject.tag == "Death")
        {
            transform.position = new_room_trigger_pos;
            rb.linearVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Bounce_Pad")
        {
            rb.AddForce(Vector3.up * 200, ForceMode.Impulse);
        }
        else if (other.gameObject.name == "Win_Trigger")
        {
            End_Screen.SetActive(true);
            Final_Timer_Text.text = "Final Timer: " + Timer.ToString("F2");
            other.gameObject.SetActive(false);
            disable_pause = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Win_sfx.Play();
            Gameplay_ost.Stop();
            Time.timeScale = 0;
            if (Timer > best_time)
            {
                best_time = Timer;
                Best_time_end_Text.text = "Best Time: " + best_time.ToString("F2");
                Menu.GetComponent<Menu>().Best_time = best_time;
            }
            else
                Best_time_end_Text.text = "Best Time: " + Menu.GetComponent<Menu>().Best_time.ToString("F2");
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Pause_Menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void To_Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
        Best_time_Text.gameObject.SetActive(true);
        Menu.GetComponent<Menu>().Best_time_Text.text = "Best Time: " + best_time.ToString("F2");
        Menu.GetComponent<Menu>().Main_Menu.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Speed_Area")
        {
            rb_move_speed /= 2;
        }
        //if (other.gameObject.name == "Mud")
        //{
        //    move_speed *= 6;
        //}
    }

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

    private void Unused()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position = (rb.transform.forward * Time.deltaTime);
        //    transform.position = Vector3.up * Time.deltaTime;
        //}

        //if (mud_timer > 0)
        //{
        //    mud_timer -= Time.deltaTime;
        //    is_grounded = false;
        //    //if (mud_timer <= 0)
        //    //{
        //    //    mud_timer = 1;
        //    //    can_jump = true;
        //    //} 
        //}

        // Normalize speed to a 0-1 ratio
        //float speed_ratio = Mathf.InverseLerp(min_speed, max_speed, current_speed);

        //// Calculate the target FOV based on the speed ratio
        //float target_FOV = Mathf.Lerp(min_FOV, max_FOV, speed_ratio);

        //Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, target_FOV, Time.deltaTime * FOV_change_speed);
    }
}
