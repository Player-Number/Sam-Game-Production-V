using UnityEngine;

public class Move_Cam : MonoBehaviour
{
    public Transform Cam_pos;
    //void Start()
    //{
        
    //}

    void Update()
    {
        transform.position = Cam_pos.position;
    }
}
