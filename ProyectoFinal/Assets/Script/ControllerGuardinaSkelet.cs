using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGuardinaSkelet : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator animator;
    private int currentAnimation = 1;
    private int flechasRecibidas = 0;
    private bool colisionManejada = false;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {    
        currentAnimation = 3;
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
