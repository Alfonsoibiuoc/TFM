using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlNatxo : MonoBehaviour
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
public string DialogoNo;
public string DialogoSi;
public GameObject panelDialogos;
ControladorEscena control;
public ControlNatxa controlNatxa;
public bool completado;





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

    panelDialogos.SetActive(true);
    imagenDialogo.sprite = avatar;
    textoDialogo.text = DialogoNo;
    yield return new WaitForSeconds(3);
    panelDialogos.SetActive(false);
}


public IEnumerator ResponderSi()
{

    completado = true;
    panelDialogos.SetActive(true);
    imagenDialogo.sprite = avatar;
    textoDialogo.text = DialogoSi;
    anim.SetBool("Drogado",true);
    objetoQueQuiere.SetActive(true);
    inventario.RemoveItem(itemID);
    yield return new WaitForSeconds(2);
    objetoQueDa.GetComponent<Items>().alcanzable = true;
    panelDialogos.SetActive(false);


}


public void mirarObjeto()
{
    lookAt = ObjetoAMirar;
}

private void OnTriggerStay(Collider other)
{
    if (!completado)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.transform.tag == "Player")
            {
                Player = other.transform.root.gameObject;
                if (controlNatxa.objetoActual != null)
                {
                    if (controlNatxa.objetoActual.tag == "Llave")
                    {
                        if (controlNatxa.objetoActual.GetComponent<Items>().codigo == codigo)
                        {
                            tieneObjeto = true;
                            //recibe el objeto
                            Debug.Log("recibe el objeto");
                            itemID = controlNatxa.objetoActual.GetComponent<Items>().ID;
                            StartCoroutine(ResponderSi());
                            Destroy(controlNatxa.objetoActual);
                            return;
                        }
                        else
                        {

                            //No recibe el objeto.
                            Debug.Log("No recibe el objeto.");
                            StartCoroutine(ResponderNo());
                            return;
                        }

                    }
                    else
                    {
                        //No recibe el objeto.
                        Debug.Log("No recibe el objeto.");
                        StartCoroutine(ResponderNo());
                        return;
                    }
                }
                else
                {
                    Debug.Log("No recibe el objeto.");
                    StartCoroutine(ResponderNo());
                    return;
                }

            }
        }
    }

}




}
