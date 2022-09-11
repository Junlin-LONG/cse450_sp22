using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Outlet
    Rigidbody2D rigidbody;

    // State Tracking
    float randomSpeed;

    // Methods
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(0.5f, 3f);
    }

    private void Update()
    {
        rigidbody.velocity = Vector2.left * randomSpeed;
    }

    // Destory asteroid when move out of scene
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
