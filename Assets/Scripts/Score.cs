using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = score.ToString();
    }

    void Update()
    {
        GetComponent<Text>().text = score.ToString();
    }
}
