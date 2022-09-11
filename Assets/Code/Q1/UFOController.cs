using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q1
{
    public class UFOController : MonoBehaviour
    {
        //Configuration
        public float speed = 4f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //Move up
            if(Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            }

            // Move down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            }

            // Move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            }

            // Move right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            }
        }
    }
}
