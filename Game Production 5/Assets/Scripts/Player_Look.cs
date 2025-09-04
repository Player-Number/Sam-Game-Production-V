using UnityEngine;

public class Player_Look : MonoBehaviour
{
    [SerializeField] Transform player_body;
    float min_view_dis = 85f;
    float mouse_sen = 200f;
    float x_rot = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X") * mouse_sen * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * mouse_sen * Time.deltaTime;

        x_rot -= mouse_y;
        x_rot = Mathf.Clamp(x_rot, -90f, min_view_dis);

        transform.localRotation = Quaternion.Euler(x_rot, 0, 0);
        player_body.Rotate(Vector3.up * mouse_x);
    }
}
