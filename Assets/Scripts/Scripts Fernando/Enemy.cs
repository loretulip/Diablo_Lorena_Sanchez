using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDanhable
{

    [SerializeField]
    private float vidasIniciales;


    [SerializeField] 
    private LocalCanvas localCanvas;

    [SerializeField]
    private Image healthBar;


    [SerializeField]
    private Outline outline;

    [SerializeField]
    private Collider coll;

    private NavMeshAgent agent;

    private SistemaCombate combate;
    private SistemaPatrulla patrulla;
    private EnemyVisualSystem visualSystem;

    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
    public EnemyVisualSystem VisualSystem { get => visualSystem; set => visualSystem = value; }

    private float vidasActuales;
    private bool muerto;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        vidasActuales = vidasIniciales;
        
    }
    private void Start()
    {
        patrulla.enabled = true;
    }


    public void RecibirDanho(float danho)
    {
        if(muerto) return;

        vidasActuales -= danho;
        healthBar.fillAmount = vidasActuales / vidasIniciales;
        if(vidasActuales <= 0)
        {
            muerto = true;
            Muerte();
        }
    }

    private void Muerte()
    {

        Destroy(localCanvas.gameObject);
        Destroy(coll);
        Destroy(combate);
        Destroy(patrulla.gameObject);
        Destroy(gameObject, 5);
        visualSystem.EjecutarAnimacionMuerte();
    }


    private void OnMouseEnter()
    {
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
