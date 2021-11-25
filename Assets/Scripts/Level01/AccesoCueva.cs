using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccesoCueva : MonoBehaviour
{
    public string codigo;
    public int itemID;
    Inventario inventario;
    public Image imagenDialogo;
    public Text textoDialogo;
    public Sprite avatar;
    public string DialogoNo;
    public GameObject panelDialogos;
    public List<Collider> llavesCercanas;
    ControladorEscena control;
    public ControlNatxa controlNatxa;
    public bool abierta;
    public GameObject Fade;


    void Start()
    {
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

    public void TieneObjeto()
    {

            Fade.GetComponent<Animator>().SetTrigger("Fade");
            StartCoroutine(cambioEscena());
        
    }

    public IEnumerator NoTieneObjeto()
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

    IEnumerator cambioEscena()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Jardin-Cueva");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!abierta)
        {
            
                if (other.transform.tag == "Player")
                {
                    if (controlNatxa.objetoActual != null)
                    {
                        if (controlNatxa.objetoActual.tag == "Llave")
                        {
                            if (controlNatxa.objetoActual.GetComponent<Items>().codigo == codigo)
                            {

                                itemID = controlNatxa.objetoActual.GetComponent<Items>().ID;
                                TieneObjeto();
                                //Destroy(controlNatxa.objetoActual);

                            }
                            else
                            {

                                StartCoroutine(NoTieneObjeto());
                                Debug.Log("Necesitas la linterna");
                            }
                        }
                        else
                        {
                            StartCoroutine(NoTieneObjeto());
                            Debug.Log("Necesitas la linterna");
                        }
                    }
                    else
                    {
                        StartCoroutine(NoTieneObjeto());
                        Debug.Log("Necesitas la linterna");
                    }

                }
            
        }



    }




}

