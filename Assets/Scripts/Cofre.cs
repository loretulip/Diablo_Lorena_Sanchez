using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour, IInteractuable
{
    private Outline outline;

    [SerializeField] private Texture2D cursorCofre;  // Cursor cuando est� sobre el cofre
    [SerializeField] private Texture2D cursorPorDefecto; // Cursor por defecto cuando no est� sobre el cofre
    [SerializeField] private Transform cameraPoint;


    [SerializeField] private DialogoSO dialogoCofre; // Referencia al ScriptableObject que contiene el di�logo

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    // Funci�n que detecta la interacci�n (cuando el jugador hace clic en el objeto)
    public void Interactuar(Transform interactuador)
    {
        // Aqu� puedes poner la l�gica de abrir el cofre si lo deseas

        // Mostrar el di�logo cuando se interact�a con el cofre
        if (dialogoCofre != null)
        {
            // Llamamos al sistema de di�logo para mostrar el di�logo al jugador
            SistemaDialogo.dialogo.IniciarDialogo(dialogoCofre,cameraPoint);
        }
    }

    // Al pasar el rat�n por encima, cambia el cursor y activa el outline
    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorCofre, Vector2.zero, CursorMode.Auto);
        outline.enabled = true;
    }

    // Al salir del �rea de interacci�n, restaura el cursor y desactiva el outline
    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorPorDefecto, Vector2.zero, CursorMode.Auto);
        outline.enabled = false;
    }
}
