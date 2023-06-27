using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSpear : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private bool muerteContada = false;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "flechaPersonaje" && !muerteContada)
        {
            muerteContada = true;
            gameManager.MuertesEnemigo();
            Destroy(gameObject);
        }
    }
}
