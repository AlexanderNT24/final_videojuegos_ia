using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEscena1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
   
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
            if (gameManager.GetCantidadLlaves() >=1)
            {
                SceneManager.LoadScene("Scene2");
            }
            else
            {
                Debug.Log("No tienes suficientes llaves. No puedes cambiar de escena.");
            }
        }
    }
}
