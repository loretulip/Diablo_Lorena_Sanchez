using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaPatrullla : MonoBehaviour
{
    [SerializeField] Enemigo main;
    [SerializeField] private Transform ruta;
    private int indiceRutaActual = -1;//Marca el indice del nuevo punto al cual dirigirse y hay que inicializarlo a -1 para que empiece en el cero
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float velocidadPatrulla;
    [SerializeField] private float stoppingDistancePatrulla;
    private Vector3 destinoActual;//Marca el destino al cual tenemos que ir

    List<Vector3> listadoPuntos = new List<Vector3>();

    private void Awake()
    {
        main.Patrulla = this;
        //Voy recorriendo todos los puntos que tiene mi ruta
        foreach (Transform punto in ruta)
        {
            //Y los añado en mi lista
            listadoPuntos.Add(punto.position);
        }
    }

    private void OnEnable()
    {
        //Reiniciamos la ruta
        indiceRutaActual = -1;
        //Volvemos a la velocidad de patrulla
        agent.speed = velocidadPatrulla;
        agent.stoppingDistance = stoppingDistancePatrulla;
        //Y volvemos a activar las corrutinas
        StartCoroutine(PatrullaryEsperar());
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    private IEnumerator PatrullaryEsperar()
    {
        while (true)
        {
            CalcularDestino();
            agent.SetDestination(destinoActual);
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= 0.2f); //Espera hasta que llegues a ese punto.
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }

    private void CalcularDestino()
    {
        indiceRutaActual++;
        //Count es lo mismo que los length en los arrays
        if (indiceRutaActual >= listadoPuntos.Count)
        {
            //Si he completado la ruta volvere al punto inicial
            indiceRutaActual = 0;
        }
        destinoActual = listadoPuntos[indiceRutaActual];
    }

    private void OnTriggerEnter(Collider other)
    {
        //Mirar a ver si lo que entra en mi trigger es el player
        if (other.CompareTag("Player"))
        {
            //Si es asi... Parar todas las corrutinas
            StopAllCoroutines();
            main.ActivaCombate(other.transform);
            //Desactivar ESTE script.
            this.enabled = false;

        }

    }

}
