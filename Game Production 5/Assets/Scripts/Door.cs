using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] AudioSource Door_opening_sfx;

    void Start()
    {
        Door_opening_sfx.Play();
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * 2;
        if (transform.position.y >= 6.5f)
        {
            GetComponent<Door>().enabled = false;
            Door_opening_sfx.Stop();
        }

    }
}
