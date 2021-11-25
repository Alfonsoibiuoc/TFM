using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorRuperto : MonoBehaviour
{
    Inventario inventario;
    Animator anim;
    AudioSource audioNFC;
    public AudioClip AudioSi;
    public AudioClip AudioNo;
    public string codigo;
    public GameObject objetoQueDa;
    public GameObject objetoQueQuiere;
    ControlNPC controlNPC;
    public int itemID;
    public bool tieneObjeto;
    GameObject Player;
    public Transform ObjetoAMirar;
    public Transform lookAt;
    Vector3 posicionObjeto;
    public Image imagenDialogo;
    public Text textoDialogo;
    public Sprite avatar;
    public string[] DialogosNo;
    public string DialogoSi;
    public GameObject panelDialogos;
    ControladorEscena control;
    public ControlNatxa controlNatxa;
    public bool completado;
    public int vecesVisitado = 0;
    public int visitasTotales;
    public GameObject efectoDesaparecer;
    public bool hablando = false;

    void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        completado = false;
        inventario = GameObject.Find("ControladorEscena").GetComponent<Inventario>();
        audioNFC = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        objetoQueQuiere.SetActive(false);
        controlNPC = GetComponent<ControlNPC>();
        tieneObjeto = false;
        lookAt = ObjetoAMirar;

        visitasTotales = DialogosNo.Length;
    }

    private void Update()
    {
        if (control.playerActivo)
        {

            controlNatxa = GameObject.FindWithTag("Player").GetComponent<ControlNatxa>();

        }
        posicionObjeto = lookAt.position - transform.position;
        posicionObjeto.y = 0;
        var rotacion = Quaternion.LookRotation(posicionObjeto);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime);
    }




    public IEnumerator ResponderNo()
    {
        Debug.Log("DentroNO");
        //audioNFC.PlayOneShot(AudioNo);
        panelDialogos.SetActive(true);
        imagenDialogo.sprite = avatar;
        textoDialogo.text = DialogosNo[vecesVisitado];
        lookAt = Player.transform;
        anim.SetTrigger("Hablar");
        yield return new WaitForSeconds(4);
        lookAt = ObjetoAMirar.transform;
        panelDialogos.SetActive(false);
        hablando = false;
    }


    public IEnumerator ResponderSi()
    {
        //audioNFC.PlayOneShot(AudioSi);
        completado = true;
        panelDialogos.SetActive(true);
        imagenDialogo.sprite = avatar;
        textoDialogo.text = DialogoSi;
        lookAt = Player.transform;
        anim.SetTrigger("ResponderSi");
        //objetoQueQuiere.SetActive(true);
        //inventario.RemoveItem(itemID);
        yield return new WaitForSeconds(2);
        lookAt = ObjetoAMirar.transform;
        objetoQueDa.GetComponent<Items>().alcanzable = true;
        panelDialogos.SetActive(false);
        Destroy(this.gameObject, 0.2f);
        efectoDesaparecer.SetActive(true);
        


    }


    public void mirarObjeto()
    {
        lookAt = ObjetoAMirar;
    }

    private void OnTriggerStay(Collider other)
    {

            if (Input.GetKeyDown(KeyCode.E) && !hablando)
            {
                
                if (other.transform.tag == "Player")
                {
                    
                    Player = other.transform.root.gameObject;
                    lookAt = Player.transform;

                    if (vecesVisitado < visitasTotales && hablando == false)
                    {
                    hablando = true;
                  
                    
                        vecesVisitado++;
                        StartCoroutine(ResponderNo());
                }
                    else
                    {
                        StartCoroutine(ResponderSi());
                }

                }
            }
        

    }




}
