using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaMisiones : MonoBehaviour
{
    [SerializeField]
    private EventManagerSO eventManager;

    [SerializeField]
    private GameObject toggleMision;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnEnable()
    {
        eventManager.OnNuevaMision += ActivarToggleMision;
    }

    private void ActivarToggleMision()
    {
        toggleMision.SetActive(true);
    }
}
