using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void ReplayGame()
    {
        //Enemy.countEnemyDied = 0;
        SceneManager.LoadScene(0);
    }
}
