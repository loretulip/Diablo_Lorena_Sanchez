using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanciaInteraccion;
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private float tiempoRotacion;
    private NavMeshAgent agent;
    private Camera cam;
    private PlayerAnimations playerAnimation;

    private Transform ultimoClick;//Guardo la informacion del NPC actual con el que voy a hablar

    public PlayerAnimations PlayerAnimation { get => playerAnimation; set => playerAnimation = value; }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            Movimiento();
        }

        if (ultimoClick && ultimoClick.TryGetComponent(out IInteractuable interactuable))
        {
            agent.stoppingDistance = distanciaInteraccion;
            //Comprobar si ha llegado al NPC
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
        Debug.Log("Me hacen pupita" + dannoAtaque);
    }
}
