using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour, IDanhable
{
    private SistemaCombate combate;
    private SistemaPatrulla patrulla;

    private Transform mainTarget;

    // acceder en otros scripts sin estar en public (encapsular)
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
    public Transform MainTarget { get => mainTarget; }

    [SerializeField] private int vida = 100;
    [SerializeField] private Image imgVida;

    public void Start()
    {
        patrulla.enabled = true; // empieza juego activo patrulla
    }


    public void ActivaCombate(Transform target)
    {
        mainTarget = target;

        combate.enabled = true; // nos dicen de activar combate
    }

    public void ActivarPatrulla()
    {
        Patrulla.enabled = true;
    }
    public void ActualizarVida()
    {
        imgVida.fillAmount = vida / 100;
    }
    public void RecibirDanho(int danho)
    {
        vida -= danho;
        if (vida <= 0)
        {
            Muerte();
            vida = 0;
        }
        ActualizarVida();
    }

    public void Muerte()
    {

    }
}
