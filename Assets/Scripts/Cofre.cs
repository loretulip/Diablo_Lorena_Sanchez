using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour, IInteractuable
{
    private Outline outline;

    [SerializeField] private Texture2D cursorCofre;  // Cursor cuando está sobre el cofre
    [SerializeField] private Texture2D cursorPorDefecto; // Cursor por defecto cuando no está sobre el cofre
    [SerializeField] private Transform cameraPoint;


    [SerializeField] private DialogoSO dialogoCofre; // Referencia al ScriptableObject que contiene el diálogo

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    // Función que detecta la interacción (cuando el jugador hace clic en el objeto)
    public void Interactuar(Transform interactuador)
    {
        // Aquí puedes poner la lógica de abrir el cofre si lo deseas

        // Mostrar el diálogo cuando se interactúa con el cofre
        if (dialogoCofre != null)
        {
            // Llamamos al sistema de diálogo para mostrar el diálogo al jugador
            SistemaDialogo.dialogo.IniciarDialogo(dialogoCofre,cameraPoint);
        }
    }

    // Al pasar el ratón por encima, cambia el cursor y activa el outline
    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorCofre, Vector2.zero, CursorMode.Auto);
        outline.enabled = true;
    }

    // Al salir del área de interacción, restaura el cursor y desactiva el outline
    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorPorDefecto, Vector2.zero, CursorMode.Auto);
        outline.enabled = false;
    }
}
