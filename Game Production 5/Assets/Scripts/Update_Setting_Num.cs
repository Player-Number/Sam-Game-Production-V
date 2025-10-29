using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Update_Setting_Num : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value.ToString("F0");
    }

    void Update()
    {
        
    }

    public void Update_Setting_Number(TMP_Text Val_Text)
    {
        Val_Text.text = GetComponent<Slider>().value.ToString("F0");
        if (gameObject.CompareTag("Volume")) 
            Val_Text.text = (100 * GetComponent<Slider>().value).ToString("F0");
        //if (gameObject.name == "FOV_Slider")
        //{
        //    float def = GameObject.Find("Player").gameObject.GetComponent<Player_Movement>().min_FOV;
        //}
    }

}
