using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour, IDanhable
{
    [SerializeField] private float vidas = 200;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private float attackingDistance = 2f;
    [SerializeField] private float danhoAtaque = 10f;

    private int totalCoins;
    private NavMeshAgent agent;
    private Camera cam;
    private Transform lastHit;

    private Transform currentTarget;
    private PlayerVisualSystem visualSystem;

    public PlayerVisualSystem VisualSystem { get => visualSystem; set => visualSystem = value; }
    public int TotalCoins { get => totalCoins; set => totalCoins = value; }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(Input.GetMouseButtonDown(0) && Time.timeScale != 0) //Porque si no, "recuerda" hits hechos en pausa.
            {
                agent.SetDestination(hit.point);
                lastHit = hit.transform;
            }
        }

        if(lastHit)
        {
            visualSystem.StopAttacking();

            if (lastHit.TryGetComponent(out IInteractuable interactable))
            {
                agent.stoppingDistance = interactionDistance;
                if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    interactable.Interactuar(transform);
                    lastHit = null; //Para que no siga interactuando
                }
            }
            else if(lastHit.TryGetComponent(out IDanhable _))
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

    private void FaceTarget()
    {
        Vector3 directionToTarget = (currentTarget.transform.position - transform.position).normalized;
        directionToTarget.y = 0f;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        transform.rotation = rotationToTarget;
    }

    public void Atacar()
    {
        currentTarget.GetComponent<IDanhable>().RecibirDanho(danhoAtaque);
    }

    public void RecibirDanho(float danho)
    {
        vidas -= danho;
        if(vidas <= 0)
        {
            Destroy(this);
            visualSystem.EjecutarAnimacionMuerte();
        }
    }
    public void TakeDamage(int amount)
    {
        vidas -= amount;
        Debug.Log("Jugador recibió daño, vida restante: " + vidas);

        if (vidas <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        // Aquí puedes agregar más lógica como reiniciar el nivel o mostrar una pantalla de game over.
    }
}
