using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NinjaController : MonoBehaviour
{
    //public properties
    public float velocityX = 12f;
    public float velocityY = 5f;
    public float jumpForce = 40f;


    public GameObject shuriken;
    public GameObject leftShuriken;

    // Start is called before the first frame update

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    //private properties
    private bool isJumping = false;

    private bool foundStairs = false;

    //Constants

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_SLIDE = 2;
    private const int ANIMATION_JUMP = 3;
    private const int ANIMATION_CLIMB = 4;
    private const int ANIMATION_THROW = 5;
    private const int ANIMATION_FALL = 6;
    private const int ANIMATION_GLIDE = 7;
    private const int ANIMATION_DIE = 8;
    
    //Tags

    private const string TAG_PISO = "Ground";

    private const string TAG_ESCALERA = "Stair";


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Quieto
        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(ANIMATION_IDLE);

        //caminarDerecha
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            sr.flipX = false;
            changeAnimation(ANIMATION_RUN);

        }

        //caminarIzquierda
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(ANIMATION_RUN);
            
        }

        //Saltar
        if(Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            changeAnimation(ANIMATION_JUMP);
            isJumping= true;
        }

        //ArrojararDerecha
        if(Input.GetKeyUp(KeyCode.X) && !sr.flipX)
        {
            //Crear el objeto
            //1. GameObject que debemos crear
            //2. Position donde va a aparecer
            //3. Rotación
            changeAnimation(ANIMATION_THROW);
            
            var position = new Vector2(transform.position.x,transform.position.y);
            var rotation = shuriken.transform.rotation;
            Instantiate(shuriken,position,rotation);

        }

        //ArrojararIzquierda
        if(Input.GetKeyUp(KeyCode.X) && sr.flipX)
        {
            //Crear el objeto
            //1. GameObject que debemos crear
            //2. Position donde va a aparecer
            //3. Rotación
            changeAnimation(ANIMATION_THROW);
            
            var position = new Vector2(transform.position.x,transform.position.y);
            var rotation = leftShuriken.transform.rotation;
            Instantiate(leftShuriken,position,rotation);

        }

        //subirEscaleras
        if(Input.GetKey(KeyCode.UpArrow) && foundStairs)
        {
            rb.velocity = new Vector2(0, velocityY);
            changeAnimation(ANIMATION_CLIMB);
        }

        //deslizar
        if(Input.GetKey(KeyCode.M))
        {
            changeAnimation(ANIMATION_SLIDE);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == TAG_PISO)
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == TAG_ESCALERA)
        {
            foundStairs = true;
        }
        else
        {
            foundStairs = false;
        }
        
    }


    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
