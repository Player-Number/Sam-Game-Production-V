using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] Camera Cam;

    Rigidbody rb;

    public Transform Orientation;

    public float move_speed;
    float dash_speed = 500;
    float horizontal_input;
    float vertical_input;
    public float jump_force;


    Vector3 move_dir;

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

    public bool is_dashing = false;

    public enum Movement_State
    {
        Running,
        Dashing
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Get_Input();
        //if (!is_dashing)
            Move_Cap();

        //grounded = Physics.Raycast(transform.position, Vector3.down, player_height * 0.5f + 0.15f, ground_layer);

        if (is_grounded)
        {
            rb.linearDamping = grounded_drag;
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jump_force);
            }
        }
        else
            rb.angularDamping = 0;
        FOV();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Get_Input()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        vertical_input = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        move_dir = Orientation.forward * vertical_input + Orientation.right * horizontal_input;

        rb.AddForce(move_dir.normalized * move_speed * 10, ForceMode.Force);

        //if (Input.GetKey(KeyCode.W))
        //    rb.AddForce(rb.transform.forward * Time.deltaTime * rb_move_speed);
        //if (Input.GetKey(KeyCode.A))
        //    rb.AddForce(-rb.transform.right * Time.deltaTime * rb_move_speed);
        //if (Input.GetKey(KeyCode.S))
        //    rb.AddForce(-rb.transform.forward * Time.deltaTime * rb_move_speed);
        //if (Input.GetKey(KeyCode.D))
        //    rb.AddForce(rb.transform.right * Time.deltaTime * rb_move_speed);
    }

    void FOV()
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
