using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mision")]

public class MisionSO : ScriptableObject
{
    public string ordenInicial; // EJ.: "Recoge las 3 setas"
    public string ordenFinal; // Ej.: "Dale las setas a Mauricio"

    public bool repetir; // Indica si la misión tiene varios pasos.

    // En el caso de que se repitan
    public int repeticionesTotales;

    [NonSerialized] // Para que se pueda resetear la variable entre partidas
    public int estadoActual = 0; // Variable que marca dónde estás, si tienes 1,2,3 setas...

    public int indiceMision; // Listado de misiones, es el identificador único para las misiones
}
