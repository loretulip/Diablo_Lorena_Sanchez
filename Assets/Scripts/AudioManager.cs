using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Musica de Fondo")]
    [SerializeField] AudioClip musicaMenuPrincipal;
    [SerializeField] AudioClip musicaJuego;
    [SerializeField] AudioClip musicaGameOver;

    [Header("Musica de Efectos")]

    private AudioSource musicaSource;
    private AudioSource efectoSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        musicaSource=gameObject.AddComponent<AudioSource>();
        musicaSource.loop = true;
        musicaSource.playOnAwake = false;

        efectoSource=gameObject.AddComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Pantalla Inicio")
            ReproducirMusica(musicaMenuPrincipal);
        else if (scene.name =="Juego")
        {
            ReproducirMusica(musicaJuego);
        }
        else if (scene.name == "GameOver")
        {
            ReproducirMusica(musicaGameOver);
        }
    }
    public void ReproducirMusica(AudioClip clip)
    {
        if (musicaSource.clip != clip)
        {
            musicaSource.clip=clip;
            musicaSource.Play();
        }
    }

   
}
