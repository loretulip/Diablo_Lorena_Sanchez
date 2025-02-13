using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seta : MonoBehaviour, IInteractuable
{
    [SerializeField]
    private EventManagerSO eventManager;

    [SerializeField]
    private MisionSO misionAsociada;

    private Outline outline;

    public void Interactuar(Transform interactuador)
    {
        misionAsociada.estadoActual++; //a un paso mas de completar la mision
        if (misionAsociada.estadoActual < misionAsociada.repeticionesTotales)
        {
            eventManager.ActualizarMision(misionAsociada);
        }
        else
        {
            eventManager.TerminarMision(misionAsociada);
        }

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
