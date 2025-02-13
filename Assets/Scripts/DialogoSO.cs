using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Di�logo")]

public class DialogoSO : ScriptableObject
{
    [TextArea]
    public string[] frases;

    public float tiempoEntreLetras;
    public bool tieneMision;

    public MisionSO mision;

}
