using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaMisiones : MonoBehaviour
{
    [SerializeField]
    private EventManagerSO eventManager;


    [SerializeField]
    private ToggleMision[] toggleMision;

    private void OnEnable()
    {
        //PARA UTILIZAR LOS EVENTOS TENEMOS QUE SUSCRIBIRNOS A ELLOS
        eventManager.OnNuevaMision += ActivarToggleMision;
        eventManager.OnActualizarMision += ActualizarToggle;
        eventManager.OnTerminarMision += CerrarToggle;
    }

    private void ActualizarToggle(MisionSO mision)
    {
        toggleMision[mision.indiceMision].TextoMision.text = mision.ordenInicial;
        toggleMision[mision.indiceMision].TextoMision.text += "(" + mision.estadoActual + "/" + mision.repeticionesTotales + ")";
    }

    private void ActivarToggleMision(MisionSO mision)
    {
        toggleMision[mision.indiceMision].TextoMision.text = mision.ordenInicial;

        if (mision.repetir)
        {
            toggleMision[mision.indiceMision].TextoMision.text += "(" + mision.estadoActual + "/" + mision.repeticionesTotales + ")";
        }

        toggleMision[mision.indiceMision].gameObject.SetActive(true);
    }

    private void CerrarToggle(MisionSO mision)
    {
        toggleMision[mision.indiceMision].Toggle.isOn = true;
        toggleMision[mision.indiceMision].TextoMision.text = mision.ordenFinal;

    }


    //PASOS PARA LOS EVENTOS
    //1.Creo a alguien que gestione las misiones/eventos del juego.
    //2.Todos los que esten interesados en las "noticias" o "llamadas" se "suscriben".
    //3.En cada objeto/clase/etc que este suscrita le anhado el comportamiento especifico que va a seguir.


}
