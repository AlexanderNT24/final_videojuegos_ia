using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRecogerArmas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    private bool flechaRecolectada = false;
    void Start()
    {
        //agregar esto para un prefab
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !flechaRecolectada)
        {
            Debug.Log("Jugador recogió una flecha");
            gameManager.GanarFlechas();
            Destroy(gameObject);
            flechaRecolectada = true;
        }
    }
}
