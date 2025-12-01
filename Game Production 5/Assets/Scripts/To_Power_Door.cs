using Unity.VisualScripting;
using UnityEngine;

public class To_Power_Door : MonoBehaviour
{
    [SerializeField] ParticleSystem Power_Door;
    [SerializeField] Material Door_Open_Glow;
    [SerializeField] float speed;
    public GameObject Door;
    Player player;
    float speedx2;
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        speedx2 = speed * 2;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (player.Collectable_remaining == 0) 
            speed = speedx2;
        transform.position = Vector3.MoveTowards(transform.position, Door.transform.position, Time.fixedDeltaTime * speed);
        if (transform.position == Door.transform.position)
        {
            Power_Door.gameObject.SetActive(true);
            if (player.Collectable_remaining <= 0)
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
