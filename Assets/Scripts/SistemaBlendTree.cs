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
    void Update()
    {
        velocidad = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Velocity", velocidad);
    }
}
