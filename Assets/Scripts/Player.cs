using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanciaInteraccion;

    private NavMeshAgent agent;
    private Camera cam;

    // Guardo la info del NPC actual con el que voy a hablar
    private NPC npcActual;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        if(npcActual)
        {
            if (agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            { 
                Debug.Log("Holi");
                npcActual.Interactuar(this.transform);
                npcActual = null;
                agent.isStopped = true;
                agent.stoppingDistance = 0;
            }
        }
    }

    private void Movimiento()
    {
        // Trazar un raycast desde la cámara a la posición del ratón
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) ;
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Mirar si el punto donde he impactado tiene el script NPC
                if (hit.transform.TryGetComponent(out NPC npc))
                {
                    // Y en ese caso ese NPC es el actual
                    npcActual = npc;
                    agent.stoppingDistance = distanciaInteraccion;
                }
                agent.SetDestination(hit.point);
            }
        }
    }
}
