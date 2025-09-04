using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text Collectable_Text;
    [SerializeField] GameObject Door;

    Rigidbody rb;

    float Collectable_remaining = 2;
    float move_speed = 500;

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
        if (Input.GetKeyDown(KeyCode.Space) && is_grounded == true)
        {
            rb.AddForce(Vector3.up * Time.deltaTime * 20000);
            is_grounded = false;
        }
        
        if (move_door)
        {
            Door.transform.position += Vector3.up * Time.deltaTime * 2;
            if (Door.transform.position.y >= 5)
            {
                Door.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            Collectable_remaining -= 1;
            Collectable_Text.text = "Collectable remaining: " + (Collectable_remaining);
            other.gameObject.SetActive(false);
            if (Collectable_remaining <= 0)
            {
                move_door = true;
            }
        }
        if (other.gameObject.tag == "New_Trigger")
        {
            Collectable_remaining = 1;
            Collectable_Text.text = "Collectable remaining: " + (Collectable_remaining);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            is_grounded = true;
        }
    }
}
