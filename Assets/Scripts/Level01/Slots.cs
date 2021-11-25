using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public int ID;
    public string tipo;
    public string codigo;
    public Inventario inventario;
    public ControlNatxa controlNatxa;
    public ControladorEscena control;
    public AudioSource audioCanvas;
    
    public bool vacio;
    public Sprite icono;

    public Transform slotIconoObjeto;

    private void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        inventario = GameObject.Find("ControladorEscena").GetComponent<Inventario>();
        
        slotIconoObjeto = transform.GetChild(0);
    }

    private void Update()
    {
        if (control.playerActivo)
        {
            controlNatxa = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlNatxa>();
        }
    }

    public void UpdateSlot()
    {
        slotIconoObjeto.GetComponent<Image>().sprite = icono;

    }

    public void UsarItem()
    {
        switch (tipo)
        {
            case "Llave":
                
                controlNatxa.tieneLlave = true;
                controlNatxa.tieneGolpeador = false;


                break;
            case "Utensilio":
                
                controlNatxa.tieneGolpeador = true;
                controlNatxa.tieneLlave = false;


                break;
        }
        controlNatxa.bloqueoMovimiento = false;
        item.GetComponent<Items>().itemUso();

        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        audioCanvas.Play();
        inventario.inventarioVisible = !inventario.inventarioVisible;

        //Quitamos cualquier objeto que el player tenga en la mano.
        int cantidadObjetos = controlNatxa.posicionManoDerecha.transform.childCount;
        for (int i = 0; i < cantidadObjetos; i++)
        {
            GameObject objeto;
            objeto = controlNatxa.posicionManoDerecha.transform.GetChild(i).gameObject;
            if (objeto != null)
            {
                objeto.GetComponent<Items>().QuitarObjeto();
            }
        }

        UsarItem();
        

    }
}
