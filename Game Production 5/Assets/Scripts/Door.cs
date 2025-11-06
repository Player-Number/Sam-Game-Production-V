using UnityEngine;

public class Door : MonoBehaviour
{
    //[SerializeField] AudioSource Door_opening_sfx;
    [SerializeField] float open_speed = 2;
    Audio_Manager Audio_Manager;
    void Start()
    {
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        Audio_Manager.Play_SFX_One_Shot(Audio_Manager.Door_Open);
    }

    void Update()
    {
        transform.position += open_speed * Time.deltaTime * Vector3.up;
        if (transform.position.y >= 6.5f)
        {
            GetComponent<Door>().enabled = false;
            Audio_Manager.Stop_SFX();
        }
    }
}
