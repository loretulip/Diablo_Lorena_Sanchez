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
    private Transform ultimoClick;

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
        if (ultimoClick&&ultimoClick.TryGetComponent(out NPC npc))
        {
            agent.stoppingDistance = distanciaInteraccion;
            // Comprobar si he llegado al NPC
            if (agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            { 
                npc.Interactuar(this.transform);
                ultimoClick = null;               
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

}
