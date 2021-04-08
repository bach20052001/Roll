using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameBaseState.PLAY)
        {
            if (transform.position.y < -10f)
            {
                Destroy(this.gameObject);
                this.PostEvent(GameEvent.OnEnemyFall, null);
            }
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * speed);
        }
    }
}
