using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 3f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySpider") ||
        collision.gameObject.CompareTag("EnemyFly") ||
        collision.gameObject.CompareTag("EnemyShield") ||
        collision.gameObject.CompareTag("EnemigoCalv") ||
        collision.gameObject.CompareTag("EnemyDispara") ||
        collision.gameObject.CompareTag("EnemyArcher") ||
        collision.gameObject.CompareTag("EnemySkeleton") ||
        collision.gameObject.CompareTag("EnemyDisp") ||
        collision.gameObject.CompareTag("EnemyMage") ||
        collision.gameObject.CompareTag("EnemySpear") ||
        collision.gameObject.CompareTag("GuardiaSkeleton"))
        {
            Destroy(gameObject);
        }

    }
}
