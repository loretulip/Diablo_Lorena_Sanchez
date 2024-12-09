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
    private int indiceFraseActual;
    private DialogoSO dialogoActual;


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

    public void IniciarDialogo(DialogoSO dialogo)
    {
        Time.timeScale = 0f;
        // El diálogo actual con el que trabajamos, es el que me dan por parámetro de entrada.
        dialogoActual = dialogo;
        marcos.SetActive(true);
        StartCoroutine (EscribirFrase());
    }
    public void SiguienteFrase()
    {
        if(escribiendo)
        {
            CompletarFrase();
        }
        else
        {
            indiceFraseActual ++;

            // Si aun me quedan frases...
            if(indiceFraseActual > dialogoActual.frases.Length)
            {
             StartCoroutine(EscribirFrase()); // ...a escribo
            }
            else
            {
                TerminarDialogo();
            }
        }
    }
    private void CompletarFrase()
    {
        escribiendo = false;
        StopAllCoroutines();
        textoDialogo.text = dialogoActual.frases[indiceFraseActual];
    }
    private void TerminarDialogo()
    {
        marcos.SetActive(false);
        StopAllCoroutines();
        indiceFraseActual = 0; // Para posteriores diálogos
        escribiendo = false;
        dialogoActual = null;
        Time.timeScale = 1f;
        
    } 
    
    private IEnumerator EscribirFrase()
    {
        escribiendo = true;
        textoDialogo.text = "";
        char[] fraseEnLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();
        foreach (char letra in fraseEnLetras)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(dialogoActual.tiempoEntreLetras);
        }
        escribiendo = false;
    }   

}
