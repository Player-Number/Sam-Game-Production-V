using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Update_Setting_Num : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Update_Setting_Number(TMP_Text Val_Text)
    {
        Val_Text.text = GetComponent<Slider>().value.ToString("F0");
    }

}
