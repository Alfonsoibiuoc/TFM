using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirar : MonoBehaviour
{
    public Transform posicionador;
    ControladorEscena control;
    public Transform player;

    private void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
    }

    void Update()
    {
        if (control.playerActivo)
        {

            player = GameObject.FindWithTag("Player").transform;
            posicionador = player.transform.GetChild(1).GetChild(0);

        }

        if (posicionador != null)
        {
        
            transform.LookAt(posicionador.transform);
        }
            

        
    }
}
