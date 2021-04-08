using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject popUp;

    void Start()
    {
        popUp.SetActive(false);
    }

    void Update()
    {
        if (PlayerController.instace.isOver)
        {
            popUp.SetActive(true);
            SpawnManager.instace.enabled = false;
        }
    }
}
