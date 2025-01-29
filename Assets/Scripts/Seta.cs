using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta : MonoBehaviour, Interactuable
{
    private Outline outline;
    [SerializeField] private EventManagerSO eventManager;

    [SerializeField] private MisionSO misionAsociada;

    public void Interactuar(Transform interactuador)
    {
        misionAsociada.estadoActual++; // Estamos a un paso más de completar la misión
        if(misionAsociada.estadoActual<misionAsociada.repeticionesTotales)
        {
            eventManager.ActualizarMision(misionAsociada);
        }
        else
        {
            eventManager.TerminaMision(misionAsociada);
        }

        eventManager.ActualizarMision(misionAsociada);
        Destroy(this.gameObject);
    }
    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnMouseEnter()
    {
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;

    }
}

