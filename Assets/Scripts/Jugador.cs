using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    

    [Header("Jugador")]

    [SerializeField] private float distanciaInteraccion;
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private Animator anim;

    private NavMeshAgent agent;
    private Camera cam;
    private float tiempoRotacion;

    private PlayerAnimations playerAnimations;

    // Guardo la info del NPC actual con el que voy a hablar
    private Transform ultimoClick;

    public PlayerAnimations PlayerAnimations { get => playerAnimations; set => playerAnimations = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale >= 1)
        {
            Movimiento();
        }

        // Si existe u npc al cual cliqué
        if (ultimoClick&&ultimoClick.TryGetComponent(out IInteractuable interactuable))
        {
            agent.stoppingDistance = distanciaInteraccion;
            // Comprobar si he llegado al NPC
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                //transform.DOLookAt(npc.transform.position, tiempoRotacion, AxisConstraint.Y).OnComplete(() => LanzarInteraccion(npc));
                LanzarInteraccion(interactuable);
            }
        }
        else if (ultimoClick && ultimoClick.TryGetComponent(out Enemigo enemigo))
        {
           // if()
            {

            }
        }
        else if(ultimoClick)
        {
            agent.stoppingDistance = 0f;
        }
    }

    private void Movimiento()
    {
        // Trazar un raycast desde la cámara a la posición del ratón
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit)) // Eliminar el punto y coma aquí
        {
            if (Input.GetMouseButtonDown(0))
            {
                agent.SetDestination(hit.point);
                ultimoClick = hit.transform;                
            }
        }
    }
    private void LanzarInteraccion(IInteractuable interactuable)
    {
        //interactuable.Interactuar();
        ultimoClick = null;
    }
   
    public void HacerDanho(float danhoAtaque)
    {
        Debug.Log("Me hacen " + danhoAtaque + " de daño.");

    }
    
}
