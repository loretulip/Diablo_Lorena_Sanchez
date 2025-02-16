using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour
{
    [SerializeField] private GameObject marcos;
    [SerializeField] private TMP_Text textoDialogo;
    [SerializeField] private Transform npcCamera;
    [SerializeField] private EventManagerSO eventManager;

    private bool escribiendo;//Determina si el sistema esta escribiendo o no
    private int indiceFraseActual;//Marca la frase por la que voy

    private DialogoSO dialogoActual;//Para almacenar con que dialogo estamos

    public static SistemaDialogo dialogo;

    private void Awake()
    {
        //Si el dialogo esta vacio...
        if (dialogo == null)
        {
            //Reclamo ese espacio para sacar mi dialogo
            dialogo = this;
            //Y no me destruyo entre escenas
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void IniciarDialogo(DialogoSO dialogo, Transform cameraPoint)
    {
        Time.timeScale = 0f;

        npcCamera.SetPositionAndRotation(cameraPoint.position, cameraPoint.rotation);
        dialogoActual = dialogo;
        marcos.SetActive(true);
        StartCoroutine(EscribirFrase());
    }

    //Que el texto aparezca letra por letra
    private IEnumerator EscribirFrase()
    {
        escribiendo = true;
        //Limpio el texto antes de poner una nueva frase.
        textoDialogo.text = "";
        char[] frasePorLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();
        foreach (char letra in frasePorLetras)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(dialogoActual.tiempoEntreLetras);
            //yield return new WaitForSeconds(dialogoActual.tiempoEntreLetras);
        }
        escribiendo = false;
    }

    public void SiguienteFrase()
    {
        if (escribiendo)//Si estamos escribiendo una frase
        {
            CompletarFrase();
        }
        else
        {
            indiceFraseActual++;//Avanzo de indice de frase
            //Y si aun quedan frases
            if (indiceFraseActual < dialogoActual.frases.Length)
            {
                StartCoroutine(EscribirFrase());
            }
            else
            {
                FinalizarDialogo();// Si no quedan frases, termino y cierro dialogo
            }

        }
    }

    private void CompletarFrase()
    {
        StopAllCoroutines();
        textoDialogo.text = dialogoActual.frases[indiceFraseActual];
        escribiendo = false;
    }

    private void FinalizarDialogo()
    {
        Time.timeScale = 1f;//Se termina la pausa.
        marcos.SetActive(false);
        StopAllCoroutines();
        indiceFraseActual = 0;//Para posteriores dialogos.
        escribiendo = false;
        if (dialogoActual.tieneMision)
        {
            //Comunico al EM que hay una mision en este dialogo.
            eventManager.NuevaMision(dialogoActual.mision);
        }

        dialogoActual = null;//Ya no tenemos ningun dialogo.
    }
}
