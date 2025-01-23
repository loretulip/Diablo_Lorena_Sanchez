using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Diálogo")]
public class DialogoSO : ScriptableObject
{
    [TextArea(5,10)]
    public string[] frases;
    public float tiempoEntreLetras;
    public bool tieneMision;
    public string textoMision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
