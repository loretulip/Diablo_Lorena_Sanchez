using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{
    // Awake vs OnEnable vs Start
    [SerializeField] private Enemigo main;
    [SerializeField] private float velocidadCombate;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private Animator anim;


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
        // Si exite un main target y ese target es alcanzable
        if(main.MainTarget !=null && agent.CalculatePath(main.MainTarget.position,new NavMeshPath()))
        {
            EnfocarObjetivo();
            // Voy persiguiendo al target en todo momento (calculando su posición)
            agent.SetDestination(main.MainTarget.position);

            if (agent.pathPending && agent.remainingDistance > distanciaAtaque)
            {
                anim.SetBool("attacking", true);
            }

        }
        else // Si no es alcanzable...
        {
            main.ActivarPatrulla();
        }
    }
    private void EnfocarObjetivo()
    {
        Vector3 direccionATarget = (main.MainTarget.position - this.transform.position).normalized;
        direccionATarget.y= 0f;
        Quaternion rotacionATarget = Quaternion.LookRotation(direccionATarget);
        transform.rotation = rotacionATarget;
    }
    #region Ejecutados por eventos de animación
    private void FinAnimacionAtaque()
    {
        anim.SetBool("attacking", false);
    }
    private void Atacar()
    {
        main.MainTarget.GetComponent<Player>().HacerDanho(danhoAtaque);
    }
    #endregion

}
