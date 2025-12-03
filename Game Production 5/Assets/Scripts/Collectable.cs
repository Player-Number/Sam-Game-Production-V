using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] float rotation_speed;
    void Start()
    {
        transform.localEulerAngles = new(15, 0, 0);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime, Space.World);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {

    //    }
    //}
}
