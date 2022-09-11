using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projctile : MonoBehaviour
{
    // Outlets
    Rigidbody2D rigidbody;

    // State tracking
    Transform target;

    // Methods
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float acceleration = GameController.instance.missileSpeed / 2f;
        float maxSpeed = GameController.instance.missileSpeed;

        ChooseNearestTarget();
        if(target != null)
        {
            // Rotate towards target
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            rigidbody.MoveRotation(angle);
        }

        // Accelerate forward
        rigidbody.AddForce(transform.right * acceleration);

        // Cap max speed
        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
    }

    void ChooseNearestTarget()
    {
        float closestDistance = 9999f;
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for(int i=0; i < asteroids.Length; i++)
        {
            Asteroid asteroid = asteroids[i];

            if(asteroid.transform.position.x > transform.position.x) // Make sure asteroid to our right
            {
                Vector2 directionToTarget = asteroid.transform.position - transform.position;

                if(directionToTarget.sqrMagnitude < closestDistance)
                {
                    closestDistance = directionToTarget.sqrMagnitude;
                    target = asteroid.transform;
                }
            }
        }      
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Asteroid>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            GameObject explosion = Instantiate(GameController.instance.explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.25f);

            GameController.instance.EarnPoints(10);
        }
    }
}
