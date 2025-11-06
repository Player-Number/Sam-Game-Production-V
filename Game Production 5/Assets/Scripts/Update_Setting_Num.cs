using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Update_Setting_Num : MonoBehaviour
{
    TMP_Text Value_Text;
    void Start()
    {
        Value_Text = GetComponentInChildren<TMP_Text>();
        Update_Setting_Number();
    }

    //void Update()
    //{
        
    //}

    public void Update_Setting_Number()
    {
        Value_Text.text = GetComponent<Slider>().value.ToString("F0");
        if (gameObject.CompareTag("Volume"))
            Value_Text.text = (100 * GetComponent<Slider>().value).ToString("F0");
    }

}
