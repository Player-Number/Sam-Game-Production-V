using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] AudioSource Music_Audio_Source;
    public AudioSource SFX_Audio_Source;

    [Header("Muisc")]
    public AudioClip Main_Menu;
    public AudioClip Gameplay;
    public AudioClip Other_Menu;
    public AudioClip Win_OST;

    [Header("SFX")]
    public AudioClip Collecting;
    public AudioClip Door_Open;
    public AudioClip Button_Pressed;
    //public AudioClip Win;

    void Start()
    {
        Music_Audio_Source.clip = Main_Menu;
        Music_Audio_Source.Play();
    }

    private void Update()
    {

    }

    public void Play_SFX(AudioClip SFX)
    {
        SFX_Audio_Source.PlayOneShot(SFX);
    }

    public void Play_Music(AudioClip Muisc)
    {
        Music_Audio_Source.clip = Muisc;
        Music_Audio_Source.Play();
    }

    public void Stop_SFX()
    {
        SFX_Audio_Source.Stop();
    }
    public void Stop_Music()
    {
        Music_Audio_Source.Stop();
    }
}
