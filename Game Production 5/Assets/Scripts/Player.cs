using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text Collectable_Text;
    [SerializeField] TMP_Text Timer_Text;
    [SerializeField] TMP_Text Dash_cool_Text;
    [SerializeField] Camera Cam;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Speedlines;
    [SerializeField] InputActionAsset input_actions;

    public GameObject Door;

    Rigidbody rb;

    public float Collectable_remaining = 2;
    float rb_move_speed = 500;
    //float move_speed = 10;
    float Timer = 0;
    float horizontal_move_cap = 4;
    float vertical_move_cap = 5;
    float dash_force = 5000;
    float dash_cool = 0;
    float Speedlines_timer = 0;

    bool move_door = false;
    bool is_grounded = true;
    //bool can_jump = true;

    Vector3 new_room_trigger_pos;

    //InputAction move_input;
    //Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        new_room_trigger_pos = transform.position;
        Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
        Dash_cool_Text.text = "Dash Cooldown: " + dash_cool.ToString("F0");
        Time.timeScale = 1.0f;
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
                //Door.gameObject.SetActive(false);
            }
        }

        Timer += Time.deltaTime;
        Timer_Text.text = Timer.ToString("F2");
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new_room_trigger_pos;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause_Menu.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && dash_cool <= 0)
        {
            rb.AddForce(Cam.gameObject.transform.forward * dash_force, ForceMode.Force);
            dash_cool = 3;
            Speedlines.SetActive(true);
            Speedlines_timer = 0.5f;
        }
        else if (dash_cool >= 0)
        {
            dash_cool -= Time.deltaTime;
            Speedlines_timer -= Time.deltaTime;
            Dash_cool_Text.text = "Dash Cooldown: " + dash_cool.ToString("F0");
            if (Speedlines_timer <= 0)
            {
                Speedlines.SetActive(false);
            }

        }
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    rb.AddForce(Cam.gameObject.transform.forward * dash_force, ForceMode.Impulse);
        //}
        if (Input.GetKey(KeyCode.Space) && is_grounded == true)
        {
            rb.AddForce(Vector3.up * 500);
            //rb.linearVelocity = Vector3.up * 500;
            //is_grounded = false;
        }
        if (rb.linearVelocity.y == 0)
            is_grounded = true;
        else
            is_grounded = false;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = (transform.position + Cam.gameObject.transform.forward * 5); //Dev only
        }
    }

    private void Move_Cap()
    {
        if (rb.linearVelocity.y >= vertical_move_cap) //rb.maxLinearVelocity
            rb.linearVelocity = new(rb.linearVelocity.x, vertical_move_cap, rb.linearVelocity.z);
        //if (rb.linearVelocity.y <= -vertical_move_cap)
        //    rb.linearVelocity = new(rb.linearVelocity.x, -vertical_move_cap, rb.linearVelocity.z);
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
            if (Collectable_remaining <= 0)
            {
                move_door = true;
            }
        }
        else if (other.gameObject.tag == "New_Room")
        {
            new_room_trigger_pos = other.transform.position;
            Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
            other.gameObject.SetActive(false);
            move_door = false;
        }
        //else if (other.gameObject.name == "Mud")
        //{
        //    move_speed /= 6;
        //    mud_timer = 1;
        //}
        else if (other.gameObject.tag == "Death")
        {
            transform.position = new_room_trigger_pos;
            rb.linearVelocity = Vector3.zero;
        }
        else if (other.gameObject.tag == "Bounce_Pad")
        {
            rb.AddForce(Vector3.up * 10000);
        }
        //else if (other.gameObject.name == "Speed_Area")
        //{
        //    rb_move_speed *= 2;
        //}
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
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = (rb.transform.forward * Time.deltaTime);
            transform.position = Vector3.up * Time.deltaTime;
        }

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
    }
}
