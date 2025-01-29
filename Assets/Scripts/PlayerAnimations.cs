using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    

    [Header("Animacion")]

    [SerializeField] 
    private Player main;

    private Animator anim;

    [Header("Musica de Efectos")]
    [SerializeField] AudioClip[] sonidoPasos;
    private AudioSource efectoSource;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //main = PlayerAnimations this;
        efectoSource = gameObject.AddComponent<AudioSource>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EjecutarAtaque()
    {
        anim.SetBool("attacking", true);
    }
    public void PararAtaque()
    {
        anim.SetBool("attacking", false);

    }
    public void ReproducirPasos()
    {
        if (sonidoPasos.Length > 0)
        {
            AudioClip randomClip = sonidoPasos[UnityEngine.Random.Range(0, sonidoPasos.Length)];
            efectoSource.PlayOneShot(randomClip);
        }
    }
}
