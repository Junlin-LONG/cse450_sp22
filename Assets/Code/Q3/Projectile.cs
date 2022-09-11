using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q3 
{ 
    public class Projectile : MonoBehaviour
    {
        // Outlets
        Rigidbody2D _rigidbody2D;

        // Methods
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.right * 10f;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Target>())
            {
                SoundManager.instance.PlaySoundHit();
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                SoundManager.instance.PlaySoundMiss();
            }
            
            Destroy(gameObject);
        }
    }
}
