using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    private float tiempoUltimoDisparo; // Variable para almacenar el tiempo del último disparo
    public float tiempoEntreDisparos = 1f; // Tiempo mínimo entre cada disparo en segundos




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

        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!ControllerBotones.ControlValue)
        {
            if (Input.GetKey("d"))
            {
                MoverDerecha();
            }
            else if (Input.GetKey("a"))
            {
                MoverIzquierda();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
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

            if (Input.GetKey("space") && SaltoCollider.suelo)
            {
                Saltar();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Disparar();
            }
            //vidas del jugador
            if (gameManager.GetVidas() <= 0)
            {
                currentAnimation = 8;
            }
        }
        else
        {
            Debug.Log(Application.dataPath);
            string filePathFire = Application.dataPath + "/../../file_fire.txt"; // Ruta del archivo de texto relativa a la carpeta "Assets"
            string filePathJump = Application.dataPath + "/../../file_jump.txt"; // Ruta del archivo de texto relativa a la carpeta "Assets"
            string filePathMove = Application.dataPath + "/../../file_move.txt"; // Ruta del archivo de texto relativa a la carpeta "Assets"

            if (File.Exists(filePathFire))
            {
                string[] lines = File.ReadAllLines(filePathFire);

                string allLines = string.Join("\n", lines);
                Debug.Log(allLines);
                if (allLines == "fire")
                {
                    StartCoroutine(Fire());

                }
                else { }
            }

            if (File.Exists(filePathJump))
            {
                string[] lines = File.ReadAllLines(filePathJump);

                string allLines = string.Join("\n", lines);
                Debug.Log(allLines);
                if (allLines == "jump")
                {
                    StartCoroutine(Jump());
                }
                else { }
            }
            if (File.Exists(filePathMove))
            {
                string[] lines = File.ReadAllLines(filePathMove);

                string allLines = string.Join("\n", lines);
                Debug.Log(allLines);
                if (allLines == "right")
                {
                    MoverDerecha();

                }
                else if (allLines == "left")
                {
                    MoverIzquierda();
                }
                else { }
            }



        }


        animator.SetInteger("Estado", currentAnimation);
    }

    void MoverDerecha()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        rb.velocity = new Vector2(2, rb.velocity.y);
        currentAnimation = 2;
    }
    IEnumerator Fire()
    {
        Disparar();

        yield return new WaitForSeconds(2f);
    }
    IEnumerator Jump()
    {
        Saltar();

        yield return new WaitForSeconds(1f);
    }

    void MoverIzquierda()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        rb.velocity = new Vector2(-2, rb.velocity.y);
        currentAnimation = 2;
    }

    void Saltar()
    {
        currentAnimation = 3;
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        audioSource.PlayOneShot(audios[0], 5);
    }

    void Disparar()
    {
        if (Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            if (gameManager.GetFlechas() > 0)
            {
                Instantiate(Flecha, Disparo.position, Disparo.rotation);
                PerderFlechas();
                audioSource.PlayOneShot(audios[1], 5);

                tiempoUltimoDisparo = Time.time; // Actualizar el tiempo del último disparo
            }
        }
    }

    void PerderFlechas()
    {
        gameManager.PerderFlechas();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovible")
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