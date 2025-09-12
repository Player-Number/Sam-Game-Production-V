using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject Pause_Menu;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void To_Game()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void To_Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Pause_Menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
