using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerVisual : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private NavMeshAgent agent;

    // Start is called before the first frame update
    private void Awake()
    {
        // Referencia a mi animator
        anim=GetComponent<Animator>();
    }
    void Start()
    {
        // Velocidad máxima
        //agent.speed

        //Velocidad actual
        //agent.velocity
    }

    // Update is called once per frame
    void Update()
    {
        // Todos los frames voy actualizando mi velocity en función a mi velocidad actual
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);
    }
}
