using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Outline outline;
    [SerializeField] private Texture2D cursorInteraccion;
    [SerializeField] private Texture2D cursorPorDefecto;

    [SerializeField] private float tiempoRotacion;
    // Start is called before the first frame update

    private void Awake()
    {
        outline = GetComponent<Outline>();

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("Holaaaaaaaaaaaaa");
        transform.DOLookAt(interactuador.position, tiempoRotacion, AxisConstraint.X, Vector3.up);
    }
}
