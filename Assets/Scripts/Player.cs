using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [Header("Distancias y tiempos")]
    [SerializeField] private float distanciaInteraccion;
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private float tiempoRotacion;

    [Header("Componentes")]
    private NavMeshAgent agent;
    private Camera cam;
    private PlayerAnimations playerAnimation;

    [Header("Estado del jugador")]
    private Transform ultimoClick; 

    public PlayerAnimations PlayerAnimation { get => playerAnimation; set => playerAnimation = value; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            Movimiento();
        }

        if (ultimoClick && ultimoClick.TryGetComponent(out IInteractuable interactuable))
        {
            agent.stoppingDistance = distanciaInteraccion;
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                LanzarInteraccion(interactuable);
            }
        }
        else if (ultimoClick && ultimoClick.TryGetComponent(out Enemigo enemigo))
        {
        }
        else if (ultimoClick)
        {
            agent.stoppingDistance = 0f;
        }
    }

    private void LanzarInteraccion(IInteractuable interactuable)
    {
        interactuable.Interactuar(transform);
        ultimoClick = null;
    }

    void Movimiento()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                agent.SetDestination(hit.point);
                ultimoClick = hit.transform;
            }
        }
    }

    public void HacerDanno(float dannoAtaque)
    {
        Debug.Log("Me hacen pupita " + dannoAtaque);
    }
}
