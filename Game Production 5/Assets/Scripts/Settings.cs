using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer Audio_Mixer;
    [SerializeField] Slider Music_Silder;
    [SerializeField] Slider SFX_Silder;
    public Slider Sensitivity_Slider;
    public Slider FOV_Slider;
    Player_Movement Player_Movement;


    //void Start()
    //{
    //    //if (PlayerPrefs.HasKey("Music_Vol"))
    //    //{
    //    //    Load_Music_Vol();
    //    //}
    //    //else
    //    //{
    //    //    Set_Music_Vol();
    //    //}
    //    //if (PlayerPrefs.HasKey("SFX_Vol"))
    //    //{
    //    //    Load_SFX_Vol();
    //    //}
    //    //else
    //    //{
    //    //    Set_SFX_Vol();
    //    //}
    //}

    //void Update()
    //{

    //}

    public void Set_Music_Vol()
    {
        Audio_Mixer.SetFloat("Music", Mathf.Log10(Music_Silder.value) * 20);
        //PlayerPrefs.Save();
    }
    public void Set_SFX_Vol()
    {
        Audio_Mixer.SetFloat("SFX", Mathf.Log10(SFX_Silder.value) * 20);
        //PlayerPrefs.Save();
    }

    public void Setting_FOV()
    {
        Player_Movement = FindAnyObjectByType<Player_Movement>();
        if (Player_Movement != null)
        {
            Player_Movement.min_FOV = FOV_Slider.value;
            Player_Movement.max_FOV = FOV_Slider.value + 30;
            Player_Movement.Cam.fieldOfView = FOV_Slider.value;
            //Player_Movement.Speedlines.shape.radius = 1;
        }
    }

    //void Load_Music_Vol()
    //{
    //    Music_Silder.value = PlayerPrefs.GetFloat("Music_Vol");
    //    Set_Music_Vol();
    //}

    //void Load_SFX_Vol()
    //{
    //    SFX_Silder.value = PlayerPrefs.GetFloat("SFX_Vol");
    //    Set_SFX_Vol();
    //}
}
