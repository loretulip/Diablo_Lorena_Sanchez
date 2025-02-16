using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{
    [SerializeField] private Enemigo main;
    [SerializeField] private float velocidadCombate;
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private float dannoAtaque;



    //1.Define una velocidad de combate.
    //2. Referencia al NavMeshAgent con el que nos vamos a mover.
    //3. Marca como destino constantemente (Update()) al target. (Definido en main)



    private void Awake()
    {
        main.Combate = this;


    }

    //Este se activa cada vez que habilitas
    private void OnEnable()
    {
        agent.speed = velocidadCombate;
        agent.stoppingDistance = distanciaAtaque;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Si el target es alcanzable...
        if (main.MainTarget != null && agent.CalculatePath(main.MainTarget.position, new NavMeshPath()))
        {
            EnfocarObjetivo();
            //Para perseguir al personaje en todo momento(calculando su posicion)
            agent.SetDestination(main.MainTarget.position);
            if (!agent.pathPending && agent.remainingDistance <= distanciaAtaque)
            {
                anim.SetBool("Attack", true);
            }

        }
        else
        {
            main.ActivarPatrulla();
        }

    }

    private void EnfocarObjetivo()
    {
        Vector3 direccionATarget = (main.MainTarget.position - this.transform.position).normalized;
        direccionATarget.y = 0;
        Quaternion rotacionATarget = Quaternion.LookRotation(direccionATarget);
        transform.rotation = rotacionATarget;
    }

    #region Ejecutados por eventos de animacion.
    private void Atacar()
    {
        main.MainTarget.GetComponent<Player>().HacerDanno(dannoAtaque);
    }

    private void FinAnimacionAtaque()
    {
        anim.SetBool("Attack", false);
    }
    #endregion
}
