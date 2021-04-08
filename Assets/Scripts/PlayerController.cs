using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 3f;
    private float boost = 2f;
    public GameObject indicatorPowerUp;
    private bool hasPowerup;
    public bool isOver { get; set; }

    public static PlayerController instace;

    public static PlayerController Instance()
    {
        if (instace == null) 
            return new PlayerController();
        return instace;
    }

    void Start()
    {
        instace = GetComponent<PlayerController>();
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
            isOver = true;
        }
        float verticalInput = Input.GetAxis("Vertical");
        indicatorPowerUp.transform.position = transform.position;
        //Note : AddForce(Direction * Force);
        rb.AddForce(GameObject.Find("FocalPoint").transform.forward * speed * verticalInput);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
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
