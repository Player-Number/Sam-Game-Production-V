using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TMP_Text Best_time_Text;
    public Canvas Main_Menu;
    public float Best_time = 0;

    public static Menu Instance { get; private set; }

    void Start()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    //void Update()
    //{

    //}

    public void To_Game()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
