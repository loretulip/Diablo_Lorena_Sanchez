using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Player : MonoBehaviour, IDanhable
{
    private NavMeshAgent agent;
    private Camera cam;

    private Transform ultimoClick;//guardo la info del npc actual con el que quiero hablar

    [SerializeField] private float distanciaInteraccion = 2f;
    [SerializeField] private float attackingDistance = 2f;
    [SerializeField] private float tiempoRotacion;
    [SerializeField] private Animator anim;

    [SerializeField] private float vida;

    //ESTE ES EL DANHO DEL JUGADOR
    [SerializeField] private int danhoAtaque;

    private PlayerAnimations playerAnimations;


    //NUEVAS COSAS
    private Transform lastHit;
    private Transform currentTarget;
    [SerializeField] private PlayerVisualSystem visualSystem;
    [SerializeField] private float interactionDistance = 2f;

    private bool vivo = true;
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
        if (vivo)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (Input.GetMouseButtonDown(0) && Time.timeScale != 0) //Porque si no, "recuerda" hits hechos en pausa.
                {
                    agent.SetDestination(hit.point);
                    lastHit = hit.transform;
                }
            }

            if (lastHit)
            {

                visualSystem.StopAttacking();

                if (lastHit.TryGetComponent(out IInteractuable interactable))
                {
                    agent.stoppingDistance = interactionDistance;
                    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        interactable.Interactuar(transform);
                        lastHit = null; //Para que no siga interactuando
                    }
                }
                else if (lastHit.TryGetComponent(out IDanhable _))
                {
                    currentTarget = lastHit;
                    agent.stoppingDistance = attackingDistance;
                    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        FaceTarget();
                        visualSystem.StartAttacking();
                    }
                }
                else
                {
                    agent.stoppingDistance = 0f;
                }
            }
        }

    }

    private void FaceTarget()
    {
        Vector3 directionToTarget = (currentTarget.transform.position - transform.position).normalized;
        directionToTarget.y = 0f;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        transform.rotation = rotationToTarget;
    }

    private void LanzarInteraccion(IInteractuable interactuable)
    {
        interactuable.Interactuar(transform);
        ultimoClick = null;
    }

    private void Movimiento()
    {

        //trazar un raycast desde lacam a la posicion del raton
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                //mirar si el pto de inpacto tiene el script npc


                agent.SetDestination(hit.point); //directo al punto del impacto


                ultimoClick = hit.transform;

            }
        }
    }

    public void Atacar()
    {
        currentTarget.GetComponent<IDanhable>().RecibirDanho(danhoAtaque);
        Debug.Log("Auch!");
    }


    public void RecibirDanho(int danho)
    {
        vida -= danho;
        if (vida <= 0)
        {
            Muerte();
            vida = 0;
        }
    }

    public void Muerte()
    {
        vivo = !vivo;
        //Destroy(this);
        visualSystem.EjecutarAnimacionMuerte();
        Debug.Log("Muerte Player");
        Invoke("EscenaMuerte", 2);

    }

    private void EscenaMuerte()
    {
        SceneManager.LoadScene(4);
    }


}

