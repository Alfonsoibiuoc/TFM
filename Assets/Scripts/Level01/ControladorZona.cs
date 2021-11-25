using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorZona : MonoBehaviour
{
    ControladorEscena control;
    public Cinemachine.CinemachineVirtualCamera CamaraZona;
    void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for(int i = 0; i < control.camaras.Length; i++)
            {
                if(control.camaras[i] == CamaraZona)
                {
                    control.camaras[i].enabled = true;
                    if(control.camaras[i].tag == "CamaraMovil")
                    {
                        control.camaras[i].Follow = control.posicionador;
                    }
                    else if (control.camaras[i].tag == "CamaraFija")
                    { 
                        control.camaras[i].LookAt = control.posicionador;
                    }
                    else
                    {
                        control.camaras[i].LookAt = null;
                        control.camaras[i].Follow = null;
                    }
                }
                else
                {
                    control.camaras[i].enabled = false;
                }
            }
        }
    }
}
