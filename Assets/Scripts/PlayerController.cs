using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private RotateCamera focalPoint;

    private float speed = 3f;
    private float boost = 2f;

    public GameObject indicatorPowerUp;
    private bool hasPowerup;

    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        focalPoint = FindObjectOfType<RotateCamera>();
        instance = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    IEnumerator Powerup()
    {
        hasPowerup = true;
        indicatorPowerUp.SetActive(true);
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        indicatorPowerUp.SetActive(false);
    }
    void Update()
    {
        if (transform.position.y < -10)
        {
            this.PostEvent(GameEvent.OnPlayerFall, null);
        }


        if (GameManager.Instance.GameState == GameBaseState.PLAY)
        {
            float verticalInput = Input.GetAxis("Vertical");
            indicatorPowerUp.transform.position = transform.position;
            rb.AddForce(focalPoint.gameObject.transform.forward * speed * verticalInput);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            //this.PostEvent(GameEvent.OnPlayerEatPowerup, null);
            Destroy(other.gameObject);
            StartCoroutine(nameof(Powerup));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && hasPowerup)
        {
            GameObject enemy = collision.collider.gameObject;
            Vector3 direction = enemy.transform.position - transform.position;
            enemy.GetComponent<Rigidbody>().AddForce(direction * speed * boost, ForceMode.Impulse);
        }
    }
}
