using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour, IInteractuable
{
    private Outline outline;

    [SerializeField] private Texture2D cursorInteraccion;
    [SerializeField] private Texture2D cursorPorDefecto;

    public void Interactuar()
    {

    }

    public void Interactuar(Transform interactuador)
    {

    }

    // Start is called before the first frame update

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorInteraccion, new Vector2(0, 0), CursorMode.Auto);
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorPorDefecto, new Vector2(0, 0), CursorMode.Auto);

        outline.enabled = false;
    }
}
