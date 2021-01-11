using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    [SerializeField] private float tumble;

    private Vector3 randomRotate;
    void Start()
    {

    }

    void Update()
    {
        rb.AddForce(Vector3.back * force * Time.deltaTime, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(1, 1, 1) * tumble;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
