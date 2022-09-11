using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Q3 
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        
        // Outlet
        Rigidbody2D _rigidbody2D;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        SpriteRenderer sprite;
        Animator animator;
        public TMP_Text scoreUI;

        // State Tracking
        public int jumpsLeft = 2;
        public int score;
        public bool isPaused;

        // Methods
        private void Awake()
        {
            instance = this;    
        }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            score = PlayerPrefs.GetInt("Score");
        }

        private void FixedUpdate()
        {
            animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
            if(_rigidbody2D.velocity.magnitude > 0)
            {
                animator.speed = _rigidbody2D.velocity.magnitude / 3f;
            }
            else
            {
                animator.speed = 1f;
            }
        }

        private void Update()
        {           
            // Update UI
            scoreUI.text = score.ToString();

            if (isPaused)
            {
                return;
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                MenuController.instance.Show();
            }
            
            Movement();
            Aim();
            Shoot();
            Jump();
        }

        void Movement()
        {
            // Move Left
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = true;
            }

            // Move Right
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = false;
            }
        }

        void Aim()
        {
            Vector3 mousePosition = Input.mousePosition; // (0, 0, 0) at left bottom
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition); // (0, 0, 0) at center
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);
        }

        void Shoot()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);

                // Generate new projectile from player's position
                newProjectile.transform.position = transform.position; 
                // Generate new projectile's direction from aimPivot's rotation
                newProjectile.transform.rotation = aimPivot.rotation; 
            }
        }

        void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpsLeft > 0)
                {
                    jumpsLeft--;
                    _rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                }
            }
            animator.SetInteger("JumpsLeft", jumpsLeft);
        }

        // Rest Double Jump
        private void OnCollisionStay2D(Collision2D other)
        {
            // Check if collide with ground
            if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // hits for things hit by the ray under character
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.85f);
                //Debug.DrawRay(transform.position, -transform.up * 0.85f);

                for (int i=0; i < hits.Length; i++)
                {
                    // Get one hit from array hits
                    RaycastHit2D hit = hits[i];

                    // Check if the collider we hit is below the character is ground
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        jumpsLeft = 2;
                    }
                }
            }
        }
    }
}

