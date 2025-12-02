using UnityEngine;

public class Wall_Hop : MonoBehaviour
{
    [SerializeField] float wall_hop_jump_force;
    Player_Movement player_move;

    bool touching_wall = false;
    bool can_wall_hop = false;
    void Start()
    {
        player_move = FindAnyObjectByType<Player_Movement>();
    }

    void Update()
    {
        transform.position = player_move.transform.position;
        //Debug.Log("touching_wall " + touching_wall);
        //Debug.Log("player_move.is_grounded " + player_move.is_grounded);
        //Debug.Log("can_wall_hop " + can_wall_hop);
        if (touching_wall && !player_move.is_grounded && can_wall_hop && Input.GetKeyDown(KeyCode.Space))
        {
            player_move.Jump(wall_hop_jump_force); // , player_move.GetComponent<Rigidbody>()
            can_wall_hop = false;
            Debug.Log("Wall_Hop");
        }
        else if (!can_wall_hop && player_move.is_grounded)
        {
            can_wall_hop = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            touching_wall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            touching_wall = false;
        }
    }
}
