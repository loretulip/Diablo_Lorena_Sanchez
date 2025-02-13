using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour, IInteractuable
{
    // Patr�n single-ton:
    // Asegurarte que sea la unica instancia de "SistemaDialogo"
    // Asegura que esa instancia sea accesible desde cualquier punto del programa

    // 
    public static SistemaDialogo sistema; 

    [SerializeField] private EventManagerSO eventManager;

    [SerializeField] private GameObject marcoDialogo;

    [SerializeField] private TMP_Text textoDialogo;

    [SerializeField] private Transform npcCamera;

    private bool escribiendo;
    private int indiceFraseActual;
    private DialogoSO dialogoActual;


    public static SistemaDialogo sD;
    // Start is called before the first frame update
    void Start()
    {
        // Si el trono est� vac�o
        if (sD==null)
        {
            // reclamo el trono y me lo quedo
            sD = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void IniciarDialogo(DialogoSO dialogo, Transform cameraPoint)
    {
        Time.timeScale = 0f;

        npcCamera.transform.SetPositionAndRotation(cameraPoint.position,cameraPoint.rotation);
     
        // El di�logo actual con el que trabajamos, es el que me dan por par�metro de entrada.
        dialogoActual = dialogo;
        marcoDialogo.SetActive(true);
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
        Time.timeScale = 1f;
        marcoDialogo.SetActive(false);
        indiceFraseActual = 0; // Para posteriores di�logos
        escribiendo = false;
        StopAllCoroutines();

        if(dialogoActual.tieneMision)
        {
            // Comunico al Event Manager que hay una mision en este di�logo
            eventManager.NuevaMision(dialogoActual.mision);
        }
        dialogoActual = null;


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

    public void Interactuar(Transform interactuador)
    {
        throw new System.NotImplementedException();
    }
}
