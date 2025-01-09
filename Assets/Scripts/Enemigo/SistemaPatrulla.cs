using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaPatrulla : MonoBehaviour
{
    [SerializeField] private Transform ruta;

    [SerializeField] private Enemigo main;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float velocidadPatrulla;

    List<Vector3> listadoPuntos = new List<Vector3>();

    private Vector3 destinoActual; // Marca el destino al cual tenemos que ir.

    private int indiceDestinoActual=-1; // Marca el índice del nuevo punto para patrullar

    private void Awake()
    {
        // Voy recorriendo todos los puntos de mi ruta...
        foreach (Transform punto in ruta)
        {
            // y los añado a mi lista.
            listadoPuntos.Add(punto.position);
        }
        main.Patrulla = this;

    }
    private void Start()
    {
       
    }
    private void OnEnable()
    {
        //Comunico al main que el sistema de patrulla soy yo

        StartCoroutine(PatrullarYEsperar());
        indiceDestinoActual = -1;
        agent.speed = velocidadPatrulla;
    }

    private IEnumerator PatrullarYEsperar()
    {
        while(true)
        {
            CalcularDestino(); // 1. Calculas nuevo destino
            agent.SetDestination(destinoActual); // 2. Se te marca dicho destino

            // 3. Esperas a llegar a dicho destino y repites
            yield return new WaitUntil( () => !agent.pathPending && agent.remainingDistance <= 0.2f); //Espera hasta que llegues al siguiente punto

            // 4. Una vez llegado al punto esperamos x tiempo en dicho tiempo
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
    private void CalcularDestino()
    {
        indiceDestinoActual++;
        // Count para las listas: Es lo mismo que Length en los arrays.
        if (indiceDestinoActual >= listadoPuntos.Count)
        {
            // Si no me quedan puntos volveré al punto 0
            indiceDestinoActual = 0;
        }
        destinoActual = listadoPuntos[indiceDestinoActual];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) // Si lo que se ha metido en el trigger es el player
        {
            main.ActivaCombate(other.transform); //Deshabilito patrulla
            StopAllCoroutines(); //Paro corrutinas
        }
    }
}
