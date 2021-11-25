using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosEnDestino : MonoBehaviour
{
    ControlBoss control;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControlBoss>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "EnemigoMovil")
        {
            
                if (!control.enemigosEnDestino.Contains(other.gameObject))
                {
                    control.enemigosEnDestino.Add(other.gameObject);
                }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "EnemigoMovil" || other.tag == "Enemigo")
        {
            control.enemigosEnDestino.Remove(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "EnemigoMovil" || other.tag == "Enemigo")
        {
            
                if (!control.enemigosEnDestino.Contains(other.gameObject))
                {
                    control.enemigosEnDestino.Add(other.gameObject);
                }
            
        }
    }


}
