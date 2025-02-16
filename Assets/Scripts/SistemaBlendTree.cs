using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaBlendTree : MonoBehaviour
{

    private Animator anim;
    private float velocidad;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        velocidad = agent.velocity.magnitude / agent.speed;
        //Durante todos los frames voy actualizando la velocidad actual
        anim.SetFloat("Velocity", velocidad);
    }
}
