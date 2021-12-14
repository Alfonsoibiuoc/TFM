using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    ControlHall control;
    Data data;
    public int numMundo;
    public Button buton;

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
                buton.interactable = true;
            }
            else
            {
                control.TextoAccesoDenegado.SetActive(true);
                control.PanelPuerta.SetActive(true);
                buton.interactable = false;
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
