using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event manager")]
public class EventManagerSO : ScriptableObject
{
    public event Action OnNuevaMision;
    public void NuevaMision()
    {
        // Aqu� lanzo la notificaci�n (el evento) por si a alguien le interesa.
        OnNuevaMision.Invoke();
    }
}
