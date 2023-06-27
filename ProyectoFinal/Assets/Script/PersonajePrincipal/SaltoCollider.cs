using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoCollider : MonoBehaviour
{
    public static bool suelo;
	public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
	{
		suelo = true;

        if (collision.gameObject.tag == "Mar")
        {
            Debug.Log("cayo a mar ");
            gameManager.muertePersonajeMar();
        }

    }
	
	private void OnTriggerExit2D(Collider2D collision)
	{
		suelo = false;
	}
}
