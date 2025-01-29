using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Outline outline;
    [SerializeField] private Texture2D cursorInteraccion;
    [SerializeField] private Texture2D cursorPorDefecto;

    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private MisionSO misionAsociada;

    [SerializeField] private DialogoSO dialogo1;
    [SerializeField] private DialogoSO dialogo2;

    [SerializeField] private GameObject sistemaDialogo;

    [SerializeField] private Transform cameraPoint;

    [SerializeField] private float tiempoRotacion;

    private DialogoSO dialogoActual;

    private void Awake()
    {
        dialogoActual = dialogo1;
        outline = GetComponent<Outline>();

    }
    private void OnEnable()
    {
        // Me "suscribo" al evento para estar atento de cuándo cambiar el diálogo
        eventManager.OnTerminarMision += CambiarDialogo;
    }

    private void CambiarDialogo(MisionSO misionTerminada)
    {
        if (misionTerminada == misionAsociada)
        {
            dialogoActual = dialogo2;
        }
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorInteraccion, Vector2.zero, CursorMode.Auto);
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorPorDefecto, Vector2.zero, CursorMode.Auto);
        outline.enabled = false;
    }

    public void Interactuar(Transform interactuador)
    {
        Debug.Log("dialogo");
        transform.DOLookAt(interactuador.position, tiempoRotacion, AxisConstraint.Y).OnComplete(() => SistemaDialogo.sD.IniciarDialogo(dialogoActual, cameraPoint));
        sistemaDialogo.SetActive(true);
    }   
}

