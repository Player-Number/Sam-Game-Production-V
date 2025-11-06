using Unity.VisualScripting;
using UnityEngine;

public class Player_Cam : MonoBehaviour
{
    
    Settings Settings;
    public float sens_X;
    public float sens_Y; // def - 200
    float rot_X;
    float rot_Y;
    public Transform Orientation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Settings = FindAnyObjectByType<Settings>();
        Settings.Sensitivity_On_Val_Changed();
    }

    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X") * sens_X * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * sens_Y * Time.deltaTime;

        rot_X -= mouse_y;
        rot_Y += mouse_x;
        rot_X = Mathf.Clamp(rot_X, -90f, 90);

        transform.rotation = Quaternion.Euler(rot_X, rot_Y, 0);
        Orientation.rotation = Quaternion.Euler(0, rot_Y, 0);
    }
}
