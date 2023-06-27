using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MonedaController : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
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
        if (collision.gameObject.tag == "Player")
        {
            gameManager.AumentarVidas();
            Destroy(gameObject); // D
        }
    }
}
