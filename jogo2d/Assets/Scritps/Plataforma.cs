using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = gameObject.transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
        collision.gameObject.transform.parent = null;
    }
}
