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

    private bool escribiendo;
    private int indiceFraseActual;

    private DialogoSO dialogoActual;

    public static SistemaDialogo dialogo;

    private void Awake()
    {
        if (dialogo == null)
        {
            dialogo = this;
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

    private IEnumerator EscribirFrase()
    {
        escribiendo = true;
        textoDialogo.text = "";
        char[] frasePorLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();
        foreach (char letra in frasePorLetras)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(dialogoActual.tiempoEntreLetras);
        }
        escribiendo = false;
    }

    public void SiguienteFrase()
    {
        if (escribiendo)
        {
            CompletarFrase();
        }
        else
        {
            indiceFraseActual++;
            if (indiceFraseActual < dialogoActual.frases.Length)
            {
                StartCoroutine(EscribirFrase());
            }
            else
            {
                FinalizarDialogo();
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
        Time.timeScale = 1f;
        marcos.SetActive(false);
        StopAllCoroutines();
        indiceFraseActual = 0;
        escribiendo = false;
        if (dialogoActual.tieneMision)
        {
            eventManager.NuevaMision(dialogoActual.mision);
        }

        dialogoActual = null;
    }
}
