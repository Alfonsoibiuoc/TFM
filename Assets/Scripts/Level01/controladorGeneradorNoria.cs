using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorGeneradorNoria : MonoBehaviour
{
    public GameObject engranaje01;
    public GameObject engranaje02;
    public GameObject engranaje03;
    int numEngranajesColocados;
    public string codigo01;
    public string codigo02;
    public string codigo03;
    public int itemID;
    Inventario inventario;
    public Image imagenDialogo;
    public Text textoDialogo;
    public Sprite avatar;
    public string DialogoNo;
    public string Dialogo1Si;
    public string Dialogo2Si;
    public string Dialogo3Si;
    public GameObject panelDialogos;
    public GameObject boton;
    Material materialBoton;
    public GameObject Noria;
    Animator animNoria;
    bool completado;
    ControladorEscena control;
    public ControlNatxa controlNatxa;



    // Start is called before the first frame update
    void Start()
    {
        numEngranajesColocados = 0;
        inventario = GameObject.Find("ControladorEscena").GetComponent<Inventario>();
        materialBoton = boton.gameObject.GetComponent<Renderer>().material;
        completado = false;
        animNoria = Noria.GetComponent<Animator>();
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (control.playerActivo)
        {

            controlNatxa = GameObject.FindWithTag("Player").GetComponent<ControlNatxa>();

        }

        if (numEngranajesColocados >= 3){

            Debug.Log("GIRA");
            materialBoton.SetColor("_EmissionColor", Color.green);
            boton.gameObject.GetComponent<Renderer>().material = materialBoton;
            animNoria.SetBool("movimiento", true);
            completado = true;

        }
    }

    public IEnumerator EngranajeCorrecto(int numEngranaje)
    {
        //Activar panel dialogo
        panelDialogos.SetActive(true);
        imagenDialogo.sprite = avatar;
        switch (numEngranajesColocados)
        {
            case 1:
                textoDialogo.text = Dialogo1Si;
                break;
            case 2:
                textoDialogo.text = Dialogo2Si;
                break;
            case 3:
                textoDialogo.text = Dialogo3Si;
                break;
        }
        inventario.RemoveItem(itemID);
        yield return new WaitForSeconds(2);
        //Desactivar panel dialogo
        panelDialogos.SetActive(false);
    }

    public IEnumerator EngranajeIncorrecto()
    {
        //Activar panel dialogo
        panelDialogos.SetActive(true);
        imagenDialogo.sprite = avatar;
        textoDialogo.text = DialogoNo;
        //inventario.RemoveItem(itemID);
        yield return new WaitForSeconds(2);
        panelDialogos.SetActive(false);
        //Desactivar panel dialogo
    }

    private void OnTriggerStay(Collider other)
    {
        if (!completado)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.transform.tag == "Player")
                {
                    if (controlNatxa.objetoActual != null)
                    {
                        if (controlNatxa.objetoActual.tag == "Llave")
                        {
                            if (controlNatxa.objetoActual.GetComponent<Items>().codigo == codigo01)
                            {
                                itemID = controlNatxa.objetoActual.GetComponent<Items>().ID;
                                engranaje01.SetActive(true);
                                numEngranajesColocados++;
                                StartCoroutine(EngranajeCorrecto(numEngranajesColocados));
                                Destroy(controlNatxa.objetoActual);
                            }
                            else if (controlNatxa.objetoActual.GetComponent<Items>().codigo == codigo02)
                            {
                                itemID = controlNatxa.objetoActual.GetComponent<Items>().ID;
                                engranaje02.SetActive(true);
                                numEngranajesColocados++;
                                StartCoroutine(EngranajeCorrecto(numEngranajesColocados));
                                Destroy(controlNatxa.objetoActual);
                            }
                            else if (controlNatxa.objetoActual.GetComponent<Items>().codigo == codigo03)
                            {
                                itemID = controlNatxa.objetoActual.GetComponent<Items>().ID;
                                engranaje03.SetActive(true);
                                numEngranajesColocados++;
                                StartCoroutine(EngranajeCorrecto(numEngranajesColocados));
                                Destroy(controlNatxa.objetoActual);
                            }
                            else
                            {

                                Debug.Log("Necesitas un engranaje");
                                StartCoroutine(EngranajeIncorrecto());
                            }

                        }
                        else
                        {
                            Debug.Log("Necesitas un engranaje");
                            StartCoroutine(EngranajeIncorrecto());
                        }
                    }
                    else
                    {
                        Debug.Log("Necesitas un engranaje");
                        StartCoroutine(EngranajeIncorrecto());
                    }
                }
            }
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            if (!completado)
            {
                if (other.transform.tag == "Llave")
                {
                    Debug.Log(other.gameObject.GetComponent<Items>().codigo);
                    if (other.gameObject.GetComponent<Items>().codigo == codigo01)
                    {
                        itemID = other.gameObject.GetComponent<Items>().ID;
                        engranaje01.SetActive(true);
                        numEngranajesColocados++;
                        StartCoroutine(EngranajeCorrecto(numEngranajesColocados));
                        Destroy(other.gameObject);
                    }
                    else if (other.gameObject.GetComponent<Items>().codigo == codigo02)
                    {
                        itemID = other.gameObject.GetComponent<Items>().ID;
                        engranaje02.SetActive(true);
                        numEngranajesColocados++;
                        StartCoroutine(EngranajeCorrecto(numEngranajesColocados));
                        Destroy(other.gameObject);
                    }
                    else if (other.gameObject.GetComponent<Items>().codigo == codigo03)
                    {
                        itemID = other.gameObject.GetComponent<Items>().ID;
                        engranaje03.SetActive(true);
                        numEngranajesColocados++;
                        StartCoroutine(EngranajeCorrecto(numEngranajesColocados));
                        Destroy(other.gameObject);
                    }
                    else
                    {

                        Debug.Log("Necesitas un engranaje");
                        StartCoroutine(EngranajeIncorrecto());
                    }

                }
                else
                {
                    Debug.Log("Necesitas un engranaje");
                    StartCoroutine(EngranajeIncorrecto());
                }
            }
        }*/
    }

}
