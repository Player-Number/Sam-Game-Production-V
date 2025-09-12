using NUnit.Framework;
using UnityEngine;

public class Entered_New_Room : MonoBehaviour
{
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Collectables;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.GetComponent<Player>().Door = Door;
            other.GetComponent<Player>().Collectable_remaining = Collectables.transform.childCount;
            gameObject.SetActive(false);
        }
    }
}
