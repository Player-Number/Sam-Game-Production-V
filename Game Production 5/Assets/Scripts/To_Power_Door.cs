using Unity.VisualScripting;
using UnityEngine;

public class To_Power_Door : MonoBehaviour
{
    [SerializeField] ParticleSystem Power_Door;
    [SerializeField] Material Door_Open_Glow;
    [SerializeField] float speed;
    public GameObject Door;
    Player player;
    float speed_fast;
    bool count_door_power = false;
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        speed_fast = speed * 3;
    }

    //void Update()
    //{

    //}

    private void FixedUpdate()
    {
        if (player.door_power == 1) 
            speed = speed_fast;
        transform.position = Vector3.MoveTowards(transform.position, Door.transform.position, Time.fixedDeltaTime * speed);
        if (transform.position == Door.transform.position)
        {
            if (!count_door_power)
            {
                player.door_power--;
                count_door_power = true;
            }
            Power_Door.gameObject.SetActive(true);
            if (player.door_power <= 0)
            {
                Door.GetComponent<Door>().enabled = true;
                Door.GetComponent<Door>().Change_Glow(Door_Open_Glow);
            }

            if (!Power_Door.isEmitting)
            {
                Destroy(gameObject);
            }
        }

    }
}
