using TMPro;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] TMP_Text Dash_cool_Text;

    Rigidbody rb;

    public Transform Orientation;

    public float move_speed;
    float og_move_speed;
    public float air_speed;
    public float dash_speed;
    float horizontal_input;
    float vertical_input;
    public float jump_force;

    Vector3 move_dir;

    public bool is_dashing = false;

    public Movement_State state;

    [Header("Dashing")]
    public float dash_force;
    public float dash_force_up;
    float dash_cool = 1;
    float dash_cool_timer = 2;
    public float dash_duration = 0.2f;
    public float Max_Y_speed;

    [Header("Ground Check")]
    public float player_height;
    public float grounded_drag;
    public LayerMask ground_layer;
    bool is_grounded;

    [Header("FOV Settings")]
    public float max_speed = 10f;
    public float min_FOV = 60f;
    public float max_FOV = 90f;
    public float FOV_change_speed = 5f;
    public float current_FOV_velocity = 60f;
    public float smooth_time = 0.5f;

    public enum Movement_State
    {
        Running,
        Dashing,
        Airborne
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        og_move_speed = move_speed;
    }

    void Update()
    {
        Player_Input();
        FOV_based_on_Speed();
        State_Handler();
        Move_Cap();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Dash();

        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //    rb.AddForce(Cam.gameObject.transform.forward * dash_force, ForceMode.Impulse);

        if (is_grounded)
            rb.linearDamping = grounded_drag;
        else
        {
            rb.linearDamping = 0;
            if (rb.linearVelocity.y < 0)
                rb.AddForce(Vector3.down, ForceMode.Force);
        }

        if (dash_cool_timer > 0)
        {
            dash_cool_timer -= Time.deltaTime;
            Dash_cool_Text.text = "Dash Cooldown: " + dash_cool_timer.ToString("F0");
        }
    }

    private void FixedUpdate()
    {
        Move();
        if (rb.linearVelocity.y == 0)
            is_grounded = true;
        if (!is_grounded)
            is_grounded = Physics.Raycast(transform.position, Vector3.down, player_height * 0.5f + 0.15f, ground_layer);
    }

    void State_Handler()
    {
        if (is_grounded)
        {
            state = Movement_State.Running;
            move_speed = og_move_speed;
        }
        else if (is_dashing)
        {
            state = Movement_State.Dashing;
            move_speed = dash_speed;
        }
        //else
        //    move_speed = og_move_speed;
        //else
        //{
        //    state = Movement_State.Airborne;
        //}
    }

    void Player_Input()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        vertical_input = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && is_grounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        move_dir = Orientation.forward * vertical_input + Orientation.right * horizontal_input;

        if (state == Movement_State.Running)
            rb.AddForce(10 * move_speed * move_dir.normalized, ForceMode.Force);
        else if (!is_grounded)
            rb.AddForce(10 * air_speed * move_speed * move_dir.normalized, ForceMode.Force);

        //if (Input.GetKey(KeyCode.W))
        //    rb.AddForce(rb.transform.forward * Time.deltaTime * rb_move_speed);
        //if (Input.GetKey(KeyCode.A))
        //    rb.AddForce(-rb.transform.right * Time.deltaTime * rb_move_speed);
        //if (Input.GetKey(KeyCode.S))
        //    rb.AddForce(-rb.transform.forward * Time.deltaTime * rb_move_speed);
        //if (Input.GetKey(KeyCode.D))
        //    rb.AddForce(rb.transform.right * Time.deltaTime * rb_move_speed);
    }

    void Jump()
    {
        rb.linearVelocity = new(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
    }

    void Dash()
    {
        if (dash_cool_timer > 0) return;
        else dash_cool_timer = dash_cool;
        Vector3 force_to_apply = Cam.gameObject.transform.forward * dash_force + Orientation.up * dash_force_up;
        rb.AddForce(force_to_apply, ForceMode.Impulse);
        Invoke(nameof(Reset_Dash), dash_duration);
        is_dashing = true;
        rb.useGravity = false;
        //Vector3 dir = Get_Dir(Cam.gameObject.transform);
        //delay_force_to_apply = force_to_apply;
        //Invoke(nameof(force_to_apply), 0.02f);

    }

    void Reset_Dash()
    {
        rb.linearVelocity = Vector3.zero;
        is_dashing = false;
        rb.useGravity = true;
    }
    //private Vector3 delay_force_to_apply;
    //void Delay_Dash_Force()
    //{
    //    rb.AddForce(delay_force_to_apply, ForceMode.Impulse);
    //}

    //Vector3 Get_Dir(Transform forward_T)
    //{
    //    float horizontal_input = Input.GetAxisRaw("Horizontal");
    //    float vertical_input = Input.GetAxisRaw("Vertical");

    //    Vector3 dir;

    //    dir = forward_T.forward;

    //    if (horizontal_input == 0 && vertical_input == 0)
    //        dir = forward_T.forward;

    //    return dir;//.normalized;
    //}

    void FOV_based_on_Speed()
    {
        float current_speed = rb.linearVelocity.magnitude;
        float speed_normalized = Mathf.Clamp01(current_speed / max_speed);
        float target_FOV = Mathf.Lerp(min_FOV, max_FOV, speed_normalized);

        Cam.fieldOfView = Mathf.SmoothDamp(Cam.fieldOfView, target_FOV, ref current_FOV_velocity, smooth_time);
    }

    private void Move_Cap()
    {
        Vector3 flat_vel = new(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flat_vel.magnitude > move_speed)
        {
            Vector3 limit_vel = flat_vel.normalized * move_speed;
            rb.linearVelocity = new(limit_vel.x, rb.linearVelocity.y, limit_vel.z);
        }

        if (Max_Y_speed != 0 && rb.linearVelocity.y > Max_Y_speed)
            rb.linearVelocity = new(rb.linearVelocity.x, Max_Y_speed, rb.linearVelocity.z);
        
        //if (rb.linearVelocity.y >= vertical_move_cap) //rb.maxLinearVelocity
        //    rb.linearVelocity = new(rb.linearVelocity.x, vertical_move_cap, rb.linearVelocity.z);
        //if (rb.linearVelocity.x >= horizontal_move_cap)
        //    rb.linearVelocity = new(horizontal_move_cap, rb.linearVelocity.y, rb.linearVelocity.z);
        //if (rb.linearVelocity.x <= -horizontal_move_cap)
        //    rb.linearVelocity = new(-horizontal_move_cap, rb.linearVelocity.y, rb.linearVelocity.z);
        //if (rb.linearVelocity.z >= horizontal_move_cap)
        //    rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y, horizontal_move_cap);
        //if (rb.linearVelocity.z <= -horizontal_move_cap)
        //    rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y, -horizontal_move_cap);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            is_grounded = true;
            //state = Movement_State.Running;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            is_grounded = false;
        }
    }
}
