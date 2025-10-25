using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] AudioSource Door_opening_sfx;
    [SerializeField] float open_speed = 2;

    void Start()
    {
        Door_opening_sfx.Play();
    }

    void Update()
    {
        transform.position += open_speed * Time.deltaTime * Vector3.up;
        if (transform.position.y >= 6.5f)
        {
            GetComponent<Door>().enabled = false;
            Door_opening_sfx.Stop();
        }
    }
}
