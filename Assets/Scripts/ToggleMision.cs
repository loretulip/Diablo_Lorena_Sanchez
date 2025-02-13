using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMision : MonoBehaviour
{

    [SerializeField]
    private TMP_Text textoMision;

    private Toggle toggle;

    public Toggle Toggle { get => toggle; set => toggle = value; }
    public TMP_Text TextoMision { get => textoMision; set => textoMision = value; }


    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
}
