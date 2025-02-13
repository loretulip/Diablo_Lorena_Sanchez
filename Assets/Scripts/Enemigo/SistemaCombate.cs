using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{
    [SerializeField] private Enemigo main;
    [SerializeField] private float velocidadCombate;
    [SerializeField] private float distanciaCombate;
    [SerializeField] private int danhoCombate;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;



    // awake vs OnEnabled vs Start VIP TEST
    void Awake()
    {
        main.Combate = this;

    }

    private void OnEnable()
    {
        agent.speed = velocidadCombate;
        agent.stoppingDistance = distanciaCombate;
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (main.MainTarget != null && agent.CalculatePath(main.MainTarget.position, new NavMeshPath()))
        {
            //EnfocarObjetivo();
            agent.SetDestination(main.MainTarget.position);

            if (agent.remainingDistance <= distanciaCombate)
            {
                agent.isStopped = true;

                //ESTO SE DEBERIA DE HACER POR EVENTO DE ANIMACION
                //anim.SetBool("attacking", true);
                Atacar();
                Debug.Log("ATK Enem");
            }

            else
            {
                agent.isStopped = false;
                main.ActivarPatrulla();
            }
        }
    }

    #region Ejecutsods por eventos de anim
    private void Atacar()
    {
        main.MainTarget.GetComponent<Player>().RecibirDanho(danhoCombate);
    }
    private void FinAnimacionAtaque()
    {
        anim.SetBool("attacking", false);
        agent.isStopped = false;
    }



    #endregion

}