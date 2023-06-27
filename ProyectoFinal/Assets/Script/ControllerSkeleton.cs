using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSkeleton : MonoBehaviour
{
    public GameObject MoveA;
    public GameObject MoveB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    Animator animator;

    private int currentAnimation = 1;
    private int flechasRecibidas = 0;
    private bool colisionManejada = false;
    private GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = MoveB.transform;
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        currentAnimation = 2;

        if (point.x > 0)
        {
          
            // Mover hacia la derecha
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector3(-1, 1, 1); // Mirar a la derecha
            
        }
        else
        {
            
            // Mover hacia la izquierda
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector3(1, 1, 1); // Mirar a la izquierda
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == MoveB.transform)
        {
            currentPoint = MoveA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == MoveA.transform)
        {
         
            currentPoint = MoveB.transform;
        }
        animator.SetInteger("Estado", currentAnimation);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("flechaPersonaje") && !colisionManejada)
        {
            colisionManejada = true;
            flechasRecibidas++;


            if (flechasRecibidas == 4)
            {
                Debug.Log("El enemigo ha sido derrotado");
                gameManager.MuertesEnemigo();
                // Aquí puedes realizar las acciones necesarias cuando el enemigo muera, como destruirlo u otorgar puntos al jugador.
                Destroy(gameObject);
            }

            colisionManejada = false;
        }
    }
}
