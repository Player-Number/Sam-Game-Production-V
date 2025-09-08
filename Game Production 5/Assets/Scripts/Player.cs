using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text Collectable_Text;
    [SerializeField] TMP_Text Timer_Text;
    public GameObject Door;

    Rigidbody rb;

    public float Collectable_remaining = 2;
    float move_speed = 500;
    float Timer = 0;
    float horizontal_move_cap = 4;
    float vertical_move_cap = 5;
    float og_move_speed = 5;

    bool move_door = false;
    bool is_grounded = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(rb.transform.forward * Time.deltaTime * move_speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-rb.transform.right * Time.deltaTime * move_speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-rb.transform.forward * Time.deltaTime * move_speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(rb.transform.right * Time.deltaTime * move_speed);
        }
        if (Input.GetKey(KeyCode.Space) && is_grounded == true)
        {
            rb.AddForce(Vector3.up * Time.deltaTime * 25000);
            //is_grounded = false;
        }
        if (rb.linearVelocity.y >= vertical_move_cap)
        {
            rb.linearVelocity = new(rb.linearVelocity.x, vertical_move_cap, rb.linearVelocity.z);
        }
        //if (rb.linearVelocity.y <= -vertical_move_cap)
        //{
        //    rb.linearVelocity = new(rb.linearVelocity.x, -vertical_move_cap, rb.linearVelocity.z);
        //}
        if (rb.linearVelocity.x >= horizontal_move_cap)
        {
            rb.linearVelocity = new(horizontal_move_cap, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        if (rb.linearVelocity.x <= -horizontal_move_cap)
        {
            rb.linearVelocity = new(-horizontal_move_cap, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        if (rb.linearVelocity.z >= horizontal_move_cap)
        {
            rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y, horizontal_move_cap);
        }
        if (rb.linearVelocity.z <= -horizontal_move_cap)
        {
            rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y, -horizontal_move_cap);
        }


        if (rb.linearVelocity.y == 0)
            is_grounded = true;
        else
            is_grounded = false;

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
            //Collectable_remaining = 4;
            Collectable_Text.text = "Collectable Remaining: " + (Collectable_remaining);
            other.gameObject.SetActive(false);
            move_door = false;
        }
        else if (other.gameObject.name == "Mud")
        {
            move_speed /= 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Mud")
        {
            move_speed *= 2;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Floor")
    //    {
    //        is_grounded = true;
    //    }
    //}
}
