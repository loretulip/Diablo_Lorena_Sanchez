using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour
{
    // Patrón single-ton:
    // Asegurarte que sea la unica instancia de "SistemaDialogo"
    // Asegura que esa instancia sea accesible desde cualquier punto del programa

    // 

    [SerializeField] private GameObject marcos;
    [SerializeField] private TMP_Text textoDialogo;

    private bool escribiendo;

    public static SistemaDialogo sistema;
    // Start is called before the first frame update
    void Start()
    {
        // Si el trono está vacío
        if (sistema==null)
        {
            // reclamo el trono y me lo quedo
            sistema = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    public void IniciarDialogo(DialogoSO dialogo)
    {
        marcos.SetActive(true);
    }
    private void FinalizarDialogo()
    {

    }
    private void EscribirFrase()
    {

    }
    private void SiguienteFrase()
    {

    }
    

}
