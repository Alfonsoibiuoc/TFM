using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    ControlHall control;
    Data data;
    public int numMundo;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();
        control = GameObject.Find("ControladorEscena").GetComponent<ControlHall>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (data.DatosRanuras[data.ranuraActual].Mundos[numMundo])
            {
                control.TextoAcceder.SetActive(true);
                control.PanelPuerta.SetActive(true);
                control.numMundo = numMundo;
            }
            else
            {
                control.TextoAccesoDenegado.SetActive(true);
                control.PanelPuerta.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        control.PanelPuerta.SetActive(false);
        control.TextoAcceder.SetActive(false);
        control.TextoAccesoDenegado.SetActive(false);
        
    }

    
}
