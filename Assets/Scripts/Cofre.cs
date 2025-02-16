using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour, IInteractuable
{
    [Header("Variables de referencia")]
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private DialogoSO dialogoCofre;

    [Header("Variables de cursor")]
    [SerializeField] private Texture2D cursorCofre;
    [SerializeField] private Texture2D cursorPorDefecto;

    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Interactuar(Transform interactuador)
    {
        if (dialogoCofre != null)
        {
            SistemaDialogo.dialogo.IniciarDialogo(dialogoCofre, cameraPoint);
        }
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorCofre, Vector2.zero, CursorMode.Auto);
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorPorDefecto, Vector2.zero, CursorMode.Auto);
        outline.enabled = false;
    }
}
