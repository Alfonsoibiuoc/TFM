using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumenLaberinto : MonoBehaviour
{
    ControladorEscena control;

    private void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            control.ActivarLowPass();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            control.DesactivarLowPass();
        }
        
    }
}
