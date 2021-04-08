using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private float speed;

    [SerializeField] private GameConfiguration configuration;

    // Start is called before the first frame update
    void Start()
    {
        speed = configuration.enemySpeed;

        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameBaseState.PLAY)
        {
            if (transform.position.y < -5f)
            {
                this.PostEvent(GameEvent.OnEnemyFall, null);
                Destroy(this.gameObject);
            }
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * speed);
        }
    }
}
