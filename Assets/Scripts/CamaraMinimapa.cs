using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMinimapa : MonoBehaviour
{
    [SerializeField] private Player player;
    private Vector3 distanciaAPlayer;
    private void Start()
    {
        // Mido la distancia original que se tiene en la escena
        distanciaAPlayer = transform.position - player.transform.position;
    }
    private void LateUpdate()
    {
        // Mi posición en todos los frames es la del player + cierta distancia
        transform.position = player.transform.position + distanciaAPlayer;   
    }

}
