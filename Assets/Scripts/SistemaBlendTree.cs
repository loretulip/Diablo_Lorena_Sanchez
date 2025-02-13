using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaBlendTree : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private NavMeshAgent agent;



    private void Awake()
    {
        //Ref a mi animator
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //todos los frames voy actualizando mi velocity en funcion de mi vel actual
        anim.SetFloat("velocity", agent.velocity.magnitude / agent.speed);
    }
}
