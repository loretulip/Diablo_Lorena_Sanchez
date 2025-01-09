using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private SistemaCombate combate;
    private SistemaPatrulla patrulla;
    private Transform mainTarget;

    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public Transform MainTarget { get => mainTarget; set => mainTarget = value; }

    public void ActivaCombate()
    {
        // Nos dicen de activar el combate
        patrulla.enabled = true;
    }

    internal void ActivaCombate(Transform target)
    {
        mainTarget = target;
        combate.enabled = true;

    }

    public void ActivarPatrulla()
    {
        combate.enabled = false;
        patrulla.enabled = true;
    }
}
