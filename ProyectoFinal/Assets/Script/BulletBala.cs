using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBala : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float speed;
    bool haGolpeado = false;
    public GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * -speed;
        Destroy(gameObject, 8f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !haGolpeado)
        {
            Debug.Log("golpeo bala ");
            haGolpeado = true;
            gameManager.PerderVidas();
            // Resto de acciones a realizar cuando la flecha golpea al personaje
            Destroy(gameObject);
        }
    }
}
