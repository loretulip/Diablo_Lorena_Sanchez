using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Trazar un raycast desde la cámara a la posición del ratón
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hit));
        {
            if(Input.GetMouseButtonDown(0))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
