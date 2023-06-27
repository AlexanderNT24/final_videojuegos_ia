using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    GameData gameData = new GameData();
    GameRepository gameRepository;

    Text flechaText;
    Text muerteText;
    Text vidasText;
    Text llaveText;
    Text gameOverText;
    public AudioClip[] audios;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameRepository = GetComponent<GameRepository>();
        //borrar data
        gameRepository.SaveData(gameData);
        flechaText = GameObject.Find("/Canvas/FlechaText").GetComponent<Text>();
        muerteText = GameObject.Find("/Canvas/MuertesText").GetComponent<Text>();
        vidasText = GameObject.Find("/Canvas/VidasText").GetComponent<Text>();
        llaveText = GameObject.Find("/Canvas/LlaveText").GetComponent<Text>();
        gameOverText = GameObject.Find("/Canvas/GameOverText").GetComponent<Text>(); 
        gameData = gameRepository.GetSavedData();

        LoadScreenTexts();
    }
    void LoadScreenTexts()
    {
        flechaText.text = $"Flecha: {gameData.flechas}";
        muerteText.text = $"Muertes: {gameData.muertes}";
        vidasText.text = $"Vidas: {gameData.vidas}";
        llaveText.text = $"Llave: {gameData.llave}";

        if (gameData.vidas <= 0)
        {
            gameOverText.text = $"Game Over";
            Time.timeScale = 0f;
        }
    }
    public List<string> GetSkills()
    {
        return gameData.Skills;
    }
    public void muertePersonajeMar() {
        gameData.vidas = 0;
        gameOverText.text = $"Game Over";
        Time.timeScale = 0f;
    }
    //flechas
    public void PerderFlechas() {
        if (gameData.flechas > 0)
        {
            gameData.flechas--;
            gameRepository.SaveData(gameData);
            LoadScreenTexts();
        }
    }
    public int GetFlechas()
    {
        return gameData.flechas;
    }
    //ganarFlechas
    public void GanarFlechas() {
        gameData.flechas++;
        audioSource.PlayOneShot(audios[2], 5);
        gameRepository.SaveData(gameData);
        LoadScreenTexts();
    }
    //muertes
    public void MuertesEnemigo() {
        gameData.muertes++;
        audioSource.PlayOneShot(audios[1], 5);
        gameRepository.SaveData(gameData);
        LoadScreenTexts();

    }
    //vidas jugador 5
    public void PerderVidas() {
        if (gameData.vidas > 0)
        {
            gameData.vidas--;
            gameRepository.SaveData(gameData);
            LoadScreenTexts();
        }
    }
    public int GetVidas()
    {
        return gameData.vidas;
    }
    public void PerderVidasSkeleton(){
        if (gameData.vidas >= 2)
        {
            gameData.vidas -= 2;
        }
        else if (gameData.vidas == 1) {
            gameData.vidas = 0;
        }
        gameRepository.SaveData(gameData);
        LoadScreenTexts();
    }
    public void AumentarVidas()
    {
        gameData.vidas++;
        audioSource.PlayOneShot(audios[0], 5);
        gameRepository.SaveData(gameData);
        LoadScreenTexts();
    }
    //llaves
    public void llaves() {
        gameData.llave++;
        audioSource.PlayOneShot(audios[0], 5);
        gameRepository.SaveData(gameData);
        LoadScreenTexts();
    }
    public int GetCantidadLlaves()
    {
        return gameData.llave;
    }
}
