using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFlecha3 : MonoBehaviour
{
    public GameObject FlechaPrefabs;
    public float tiempoTotalGeneracion = 10f; // Tiempo total de generación en segundos
    public int cantidadFlechas = 10; // Cantidad de flechas a generar
    public float intervaloGeneracionMinimo = 1f; // Intervalo mínimo entre la generación de flechas

    private float tiempoEntreFlechas;
    private float tiempoTranscurrido = 0f;
    private int flechasGeneradas = 0;
    private float tiempoUltimaGeneracion = 0f;
    // Start is called before the first frame update
    void Start()
    {
        tiempoEntreFlechas = Mathf.Max(intervaloGeneracionMinimo, tiempoTotalGeneracion / cantidadFlechas);
    }

    // Update is called once per frame
    void Update()
    {
        if (flechasGeneradas < cantidadFlechas)
        {
            tiempoTranscurrido += Time.deltaTime;

            if (tiempoTranscurrido - tiempoUltimaGeneracion >= tiempoEntreFlechas)
            {
                GenerarFlecha();
                tiempoUltimaGeneracion = tiempoTranscurrido;
                flechasGeneradas++;
            }
        }
    }
    void GenerarFlecha()
    {
        float x = Random.Range(-3f, -2f);
        var position = new Vector3(x, 3, 0);
        Quaternion rotation = Quaternion.Euler(0, 0, -90);
        GameObject flecha = Instantiate(FlechaPrefabs, position, rotation);
        flecha.transform.Rotate(0, 0, 180);
    }
}
