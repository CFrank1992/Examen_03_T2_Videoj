using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{

    public float velocityX = 10f;

    private const string TAG_ENEMY = "Enemigo";

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag != "Ninja")
        {
            Destroy(this.gameObject);
            Debug.Log("Destruido1");
        }
        /*if(other.gameObject.CompareTag(TAG_ENEMY))
        {
            Destroy(other.gameObject);
            Debug.Log("Destruido2");
        }*/
    }
}
