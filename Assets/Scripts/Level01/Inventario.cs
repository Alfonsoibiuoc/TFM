using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    //Data data;
    public bool inventarioVisible;
    public GameObject inventario;
    public ControlNatxa controlNatxa;
    public ControladorEscena control;
    public AudioSource audioCanvas;
    public PanelInformacion panelInfo;
    public GameObject botonI;

    private int numSlots;
    private int slotsUsados;

    public GameObject[] slots;

    public GameObject panelObjetos;

    private void Awake()
    {
        //data = GameObject.Find("DATA").GetComponent<Data>();
        //slots = data.DatosRanuras[data.ranuraActual].Slots;  
    }

    void Start()
    {
        
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        panelInfo = GameObject.Find("ControladorEscena").GetComponent<PanelInformacion>();

        inventarioVisible = false;

        //Obtenemos el número de slots que tiene el inventario (En este caso son 8)
        numSlots = panelObjetos.transform.childCount;
        

        //Creamos un array de los slots del inventario.
        slots = new GameObject[numSlots];

        ////Cargamos los datos guardados del inventario.
        ///
        /////------------------------------------------

        //Recorremos todos los slots del inventario para ver los slots que hay vacíos.
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = panelObjetos.transform.GetChild(i).gameObject;

            if(slots[i].GetComponent<Slots>().item == null)
            {
                slots[i].GetComponent<Slots>().vacio = true;
            }
        }


    }

    
    void Update()
    {
        if (control.playerActivo)
        {
            controlNatxa = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlNatxa>();
        }

        //Al pulsar la tecla "A" mostramos el inventario

        if (Input.GetKeyDown(KeyCode.A) && control.playerActivo)
        {
            audioCanvas.Play();
            if (controlNatxa.golpeando == false)
            {
                inventarioVisible = !inventarioVisible;
                controlNatxa.bloqueoMovimiento = !controlNatxa.bloqueoMovimiento;
            }
        }
        if (inventarioVisible)
        {
            inventario.SetActive(true);
            control.pausa = true;
            botonI.SetActive(false);
        }
        else
        {
            inventario.SetActive(false);
            if (!panelInfo.infoVisible)
            {
                control.pausa = false;
            }
            botonI.SetActive(true);
        }
    }

    //Añadimos un objeto al inventario
    public void AddItem(GameObject itemObject, int itemID,string itemTipo, string itemDescripcion, Sprite itemIcono)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //Buscamos un hueco vacío.
            if (slots[i].GetComponent<Slots>().vacio)
            {
                itemObject.GetComponent<Items>().recogido = true;
                slots[i].GetComponent<Slots>().item = itemObject;
                slots[i].GetComponent<Slots>().ID = itemID;
                slots[i].GetComponent<Slots>().tipo = itemTipo;
                slots[i].GetComponent<Slots>().codigo = itemDescripcion;
                slots[i].GetComponent<Slots>().icono = itemIcono;
                itemObject.GetComponent<Items>().numSlot = i;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slots>().UpdateSlot();

                slots[i].GetComponent<Slots>().vacio = false;
                return;
            }   
        }
    }
    
    //Borramos un objeto del inventario mediante su ID.
    public void RemoveItem(int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].GetComponent<Slots>().ID == itemID)
            {
                slots[i].GetComponent<Slots>().vacio = true;
                slots[i].GetComponent<Slots>().icono = null;
                slots[i].GetComponent<Slots>().ID = 0;
                slots[i].GetComponent<Slots>().tipo = null;
                slots[i].GetComponent<Slots>().codigo = null;
                slots[i].transform.GetChild(1).GetComponent<Items>().DestruirObjeto();
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;

                return;
            }
        }
    }
}
