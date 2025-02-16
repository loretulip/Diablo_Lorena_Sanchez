using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaPatrullla : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Enemigo main; 
    [SerializeField] private Transform ruta;
    [SerializeField] private NavMeshAgent agent;

    [Header("Parámetros de Patrullaje")]
    [SerializeField] private float velocidadPatrulla; 
    [SerializeField] private float stoppingDistancePatrulla; 

    [Header("Variables Internas")]
    private int indiceRutaActual = -1;
    private Vector3 destinoActual;


    List<Vector3> listadoPuntos = new List<Vector3>();

    private void Awake()
    {
        main.Patrulla = this;
        foreach (Transform punto in ruta)
        {
            listadoPuntos.Add(punto.position);
        }
    }

    private void OnEnable()
    {
        indiceRutaActual = -1;
        agent.speed = velocidadPatrulla;
        agent.stoppingDistance = stoppingDistancePatrulla;
        StartCoroutine(PatrullaryEsperar());
    }   

    private IEnumerator PatrullaryEsperar()
    {
        while (true)
        {
            CalcularDestino();
            agent.SetDestination(destinoActual);
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= 0.2f); 
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }

    private void CalcularDestino()
    {
        indiceRutaActual++;
        if (indiceRutaActual >= listadoPuntos.Count)
        {
            indiceRutaActual = 0;
        }
        destinoActual = listadoPuntos[indiceRutaActual];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            main.ActivaCombate(other.transform);
            this.enabled = false;

        }

    }

}
