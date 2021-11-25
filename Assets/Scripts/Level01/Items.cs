using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public ControladorEscena control;
    public ControlNatxa controlNatxa;

    public int ID;
    public string tipo; //(Llave, Objeto, Golpeador)
    public string codigo;
    public Sprite icono;
    public int numSlot;
    public bool recogido;   //Lo ha cogido y está en el inventario
    public bool equipado;   //Lo lleva en la mano
    public bool alcanzable; //poner al false si necesitamos hacer algo antes de cogerlo.
    public bool inicio;
    public GameObject objeto;
    public Transform posicionManoDerecha;
    public GameObject interactuador;

    private void Start()
    {
        inicio = true;
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();  
    }

    private void Update()
    {
        

        if (equipado)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                QuitarObjeto();
                controlNatxa.tieneLlave = false;
                controlNatxa.tieneGolpeador = false;
                controlNatxa.objetoActual = null;
            }
            
        }
   
    }

    public void QuitarObjeto()
    {
        controlNatxa = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlNatxa>();
        switch (tipo)
        {
            case "Llave":
                controlNatxa.tieneLlave = false;
                break;
            case "Objeto":
                controlNatxa.tieneGolpeador = false;
                break;
            case "Golpeador":
                controlNatxa.tieneGolpeador = false;
                break;
        }
        equipado = false;
        gameObject.SetActive(false);  
    }

    public void AsignarObjeto()
    {
        controlNatxa = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlNatxa>();
        
        posicionManoDerecha = GameObject.FindWithTag("posManoDerecha").transform;
        int cantidadObjetos = posicionManoDerecha.transform.childCount;

        for (int i = 0; i < cantidadObjetos; i++)
        {
            if (posicionManoDerecha.GetChild(i).gameObject.GetComponent<Items>().ID == ID)
            {
                objeto = posicionManoDerecha.transform.GetChild(i).gameObject;
                controlNatxa.objetoActual = objeto;
            }
        }
    }

    public void itemUso()
    {
        

        if (objeto != null)
                {
                    objeto.SetActive(true);
                    objeto.GetComponent<Items>().equipado = true;
                    controlNatxa.objetoActual = objeto;


        }
        else
            {
            
            AsignarObjeto();
            objeto.SetActive(true);
            objeto.GetComponent<Items>().equipado = true;
            }
    }

    public void DestruirObjeto()
    {
        Destroy(this.gameObject);
    }

}
