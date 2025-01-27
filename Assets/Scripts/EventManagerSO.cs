using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event manager")]
public class EventManagerSO : ScriptableObject
{
    public event Action<MisionSO> OnNuevaMision;
    public event Action<MisionSO> OnActualizarMision;
    public event Action<MisionSO> OnTerminarMision;
    public void NuevaMision(MisionSO mision)
    {
        // Aquí lanzo la notificación (el evento) por si a alguien le interesa.
        // ?: Invocación SEGURA, se asegura de que hayan "suscriptores"
        OnNuevaMision?.Invoke(mision); // Notifico y digo qué misión es
    }

    public void ActualizarMision(MisionSO mision)
    {
        OnActualizarMision?.Invoke(mision);
    }

    public void TerminaMision(MisionSO mision)
    {
        OnTerminarMision?.Invoke(mision);
    }
}
