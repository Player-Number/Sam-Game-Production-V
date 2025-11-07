using UnityEngine;

public class Bounce_Pad : MonoBehaviour
{
    [SerializeField] float Bounce_Force; // 2.5
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player_Movement Player_Movement = other.GetComponent<Player_Movement>();
            other.GetComponent<Rigidbody>().AddForce(Bounce_Force * Player_Movement.jump_force * Vector3.up, ForceMode.Impulse);
        }

    }
}
