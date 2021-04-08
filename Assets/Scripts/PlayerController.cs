using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private RotateCamera focalPoint;
    [SerializeField] private GameConfiguration configuration;

    private float speed;
    private float forceToEnemy;

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
        speed = configuration.speed;
        forceToEnemy = configuration.forceToEnemy;

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
        if (transform.position.y < -5f)
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
            speed = configuration.speed_powerup;
            forceToEnemy = configuration.forceToEnemy_powerup;
            Destroy(other.gameObject);
            StartCoroutine(nameof(Powerup));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameObject enemy = collision.collider.gameObject;
            Vector3 direction = enemy.transform.position - transform.position;
            enemy.GetComponent<Rigidbody>().AddForce(direction * forceToEnemy, ForceMode.Impulse);
        }
    }
}
