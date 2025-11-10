using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float powerupStrength = 15f;
    private Vector3 offsetPosition;
    private Rigidbody playerRigidbody;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    private bool hasPowerup = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        offsetPosition = new Vector3(0, -0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        //Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        //playerRigidbody.MovePosition(playerRigidbody.position + movement);
        playerRigidbody.AddForce(focalPoint.transform.forward * verticalInput * moveSpeed);
        powerupIndicator.transform.position = transform.position + offsetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            // Add power-up effect here
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine() 
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer.normalized * powerupStrength, ForceMode.Impulse);
        }
    }
}
