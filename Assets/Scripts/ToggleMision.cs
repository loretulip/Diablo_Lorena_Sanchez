using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMision : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textoMision; // Recipiente donde meter los textos de cada misión

    private Toggle toggle;

    public TMP_Text TextoMision { get => textoMision; set => textoMision = value; }
    public Toggle Toggle { get => toggle; set => toggle = value; }

    void Start()
    {
        
    }

    void Update()
    {
        toggle = GetComponent<Toggle>();
    }
}
