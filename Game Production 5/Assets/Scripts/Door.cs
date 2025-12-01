using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Door : MonoBehaviour
{
    //[SerializeField] AudioSource Door_opening_sfx;
    [SerializeField] float open_speed = 2;
    [SerializeField] List<MeshRenderer> Glow;
    //public Material glow;
    Audio_Manager Audio_Manager;
    void Start()
    {
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        Audio_Manager.Play_SFX_One_Shot(Audio_Manager.Door_Open);
        //glow.EnableKeyword("_EMISSION");
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

    public void Change_Glow(Material open_glow)
    {
        foreach (MeshRenderer mat in Glow)
        {
            mat.material = open_glow;
        }
    }
}
