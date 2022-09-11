using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q8
{
    public class Breakable : MonoBehaviour
    {
        public void Break()
        {
            Destroy(gameObject);
        }
    }
}
