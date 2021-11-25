using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPuerta : MonoBehaviour
{
    public string codigo;
    Animator anim;
    AudioSource audioPuerta;
    public AudioClip AbrirPuerta;
    public AudioClip PuertaCerrada;
    public int itemID;
    Inventario inventario;
    public Image imagenDialogo;
    public Text textoDialogo;
    public Sprite avatar;
    public string DialogoNo;
    public string DialogoSi;
    public GameObject panelDialogos;
    public List<Collider> llavesCercanas;
    ControladorEscena control;
    public ControlNatxa controlNatxa;
    public bool abierta;


    void Start()
    {
        anim = GetComponent<Animator>();
        audioPuerta = GetComponent<AudioSource>();
        inventario = GameObject.Find("ControladorEscena").GetComponent<Inventario>();
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        abierta = false;
    }

    private void Update()
    {
        if (control.playerActivo)
        {

            controlNatxa = GameObject.FindWithTag("Player").GetComponent<ControlNatxa>();

        }
    }

    public IEnumerator LlaveCorrecta()
    {
        //Activar panel dialogo
        panelDialogos.SetActive(true);
        imagenDialogo.sprite = avatar;
        textoDialogo.text = DialogoSi;
        audioPuerta.PlayOneShot(AbrirPuerta);
        anim.SetBool("Abierta", true);
        inventario.RemoveItem(itemID);
        yield return new WaitForSeconds(2);

        //Desactivar panel dialogo
        panelDialogos.SetActive(false);
    }

    public IEnumerator LlaveInCorrecta()
    {
        //Activar panel dialogo
        panelDialogos.SetActive(true);
        imagenDialogo.sprite = avatar;
        textoDialogo.text = DialogoNo;
        audioPuerta.PlayOneShot(PuertaCerrada);
        //inventario.RemoveItem(itemID);
        yield return new WaitForSeconds(2);
        panelDialogos.SetActive(false);
        //Desactivar panel dialogo
    }


    private void OnTriggerStay(Collider other)
    {
        if (!abierta)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.transform.tag == "Player")
                {
                    if(controlNatxa.objetoActual != null)
                    {
                        if (controlNatxa.objetoActual.tag == "Llave")
                        {
                            if (controlNatxa.objetoActual.GetComponent<Items>().codigo == codigo)
                            {

                                itemID = controlNatxa.objetoActual.GetComponent<Items>().ID;
                                StartCoroutine(LlaveCorrecta());
                                Destroy(controlNatxa.objetoActual);

                            }
                            else
                            {

                                StartCoroutine(LlaveInCorrecta());
                                Debug.Log("Necesitas la llave correcta");
                            }
                        }
                        else
                        {
                            StartCoroutine(LlaveInCorrecta());
                            Debug.Log("Necesitas la llave correcta");
                        }
                    }
                    else
                    {
                        StartCoroutine(LlaveInCorrecta());
                        Debug.Log("Necesitas la llave correcta");
                    }

                }
            }
        }
        


    }




    }
