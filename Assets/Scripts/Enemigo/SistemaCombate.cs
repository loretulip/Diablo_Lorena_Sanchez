using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{
    // Awake vs OnEnable vs Start
    [SerializeField] private Enemigo main;
    [SerializeField] private float velocidadCombate;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float distanciaAtaque;

    // Start is called before the first frame update

    //Define una vel de combate
    //Ref al navmeshagent con el que nos vamos a mover
    //marca como destino constantemente(update) al target. definido en main
    private void Awake()
    {
        main.Combate = this;       
    }
    private void OnEnable()
    {
        agent.speed = velocidadCombate;
        agent.stoppingDistance = distanciaAtaque;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        agent.SetDestination(main.MainTarget.position);
    }
}
