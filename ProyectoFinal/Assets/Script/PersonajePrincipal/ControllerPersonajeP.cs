using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPersonajeP : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rb;
    SpriteRenderer sr;
	public float jumpSpeed = 7;

    private int currentAnimation = 1;
    // Start is called before the first frame update

    public Transform Disparo;
    public GameObject Flecha;
    public GameManager gameManager;

    public AudioClip[] audios;
    private AudioSource audioSource;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAnimation = 1;
        var velocityY = rb.velocity.y;
        rb.velocity = new Vector2(0, velocityY);

        if (Input.GetKey("d"))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(2, velocityY);
			currentAnimation = 2;
        }
        else if (Input.GetKey("a"))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            rb.velocity = new Vector2(-2, velocityY);
			currentAnimation = 2;
        }
		else
        {
            rb.velocity = new Vector2(0, velocityY);
        }
		
        if (Input.GetKey("z"))
        {
            currentAnimation = 4;
            audioSource.PlayOneShot(audios[2], 5);
        }
        if (Input.GetKey("c"))
        {
            currentAnimation = 5;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentAnimation = 6;
        }
        if (Input.GetKey("q"))
        {
            audioSource.PlayOneShot(audios[3], 5);
            currentAnimation = 7;
        }
		if (Input.GetKey("v"))
        {
            currentAnimation = 8;
        }
		
		if(Input.GetKey("space") && SaltoCollider.suelo)
		{
			currentAnimation = 3;
			rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            audioSource.PlayOneShot(audios[0],5);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (gameManager.GetFlechas() > 0)
            {
                Instantiate(Flecha, Disparo.position, Disparo.rotation);
                PerderFlechas();
                audioSource.PlayOneShot(audios[1], 5);
            }
        }
        //vidas del jugador
        if (gameManager.GetVidas() <= 0)
        {
            currentAnimation = 8;
        }

        animator.SetInteger("Estado", currentAnimation);
    }
    void PerderFlechas()
    {
        gameManager.PerderFlechas();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = collision.transform;
        }    
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovible")
        {
            transform.parent = null;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySpider") ||
            collision.gameObject.CompareTag("EnemyFly") ||
            collision.gameObject.CompareTag("EnemyShield") ||
            collision.gameObject.CompareTag("EnemigoCalv") ||
            collision.gameObject.CompareTag("EnemyDispara") ||
            collision.gameObject.CompareTag("EnemyArcher") ||
            collision.gameObject.CompareTag("EnemyMage") ||
            collision.gameObject.CompareTag("EnemyDisp") ||
            collision.gameObject.CompareTag("EnemySpear"))
        {
            Debug.Log("golpeo 1");
            gameManager.PerderVidas();
        }
        else if ((collision.gameObject.tag == "EnemySkeleton" || collision.gameObject.tag == "GuardiaSkeleton"))
        {
            gameManager.PerderVidasSkeleton();
            Debug.Log("golpeo skeleton");

        }
        
    }

}