using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COOKIE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 1, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collided");
    }
}
