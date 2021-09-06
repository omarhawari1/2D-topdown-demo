using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]private float bulletForce;

    private Rigidbody2D rb;

    private void Start() 
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() 
    {
        rb.AddForce(transform.up * bulletForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
