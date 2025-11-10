using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float changeDirectionInterval = 1.0f;
    private Vector3 curDirection = Vector3.zero;
    private List<Vector3> directions = new List<Vector3>
    {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        changeDirectionInterval -= Time.deltaTime;
        if (changeDirectionInterval < 0 || curDirection == Vector3.zero)
        { 
            int index = Random.Range(0, directions.Count);
            curDirection = directions[index];
            changeDirectionInterval = 1.0f;
        }
        transform.Translate(curDirection * moveSpeed * Time.deltaTime);

        if (transform.position.y < -10) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb;
        if (!collision.gameObject.TryGetComponent<Rigidbody>(out rb))
            return;

        float forceAmount = 10f;

        if (collision.gameObject.CompareTag("Cube")) forceAmount = 20.0f;
        else if (collision.gameObject.CompareTag("Capsule")) forceAmount = 10.0f;
        else if (collision.gameObject.CompareTag("Sphere")) forceAmount = 30.0f;
        else if (collision.gameObject.CompareTag("Cylinder")) forceAmount = 60.0f;

        rb.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
    }
}
