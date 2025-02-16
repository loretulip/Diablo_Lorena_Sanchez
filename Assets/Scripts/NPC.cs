using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractuable
{
    [Header("Componentes")]
    private Outline outline;

    [Header("Configuración de cursor")]
    [SerializeField] private Texture2D cursorNPC;
    [SerializeField] private Texture2D cursorPorDefecto;

    [Header("Interacción y diálogos")]
    [SerializeField] private float tiempoRotacion;
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private DialogoSO dialogo1;
    [SerializeField] private DialogoSO dialogo2;
    private DialogoSO dialogoActual;

    [Header("Eventos y misiones")]
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private MisionSO misionAsociada;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        dialogoActual = dialogo1;
    }

    private void OnEnable()
    {
        eventManager.OnTerminarMision += CambiarDialogo;
    }

    private void CambiarDialogo(MisionSO misionTerminada)
    {
        if (misionTerminada == misionAsociada)
        {
            dialogoActual = dialogo2;
        }
    }

    public void Interactuar(Transform interactuador)
    {
        transform.DOLookAt(interactuador.position, tiempoRotacion, AxisConstraint.Y)
            .OnComplete(() => SistemaDialogo.dialogo.IniciarDialogo(dialogoActual, cameraPoint));
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorNPC, Vector2.zero, CursorMode.Auto);
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorPorDefecto, Vector2.zero, CursorMode.Auto);
        outline.enabled = false;
    }
}
