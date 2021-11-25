using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuador : MonoBehaviour
{
    public bool tieneObjeto;
    public GameObject objetoQueDa;
    public GameObject objetoQueQuiere;
    ControlNPC controlNPC;
    


    void Start()
    {
        objetoQueQuiere.SetActive(false);
        tieneObjeto = false;
        controlNPC = GetComponent<ControlNPC>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void interaccion()
    {
        if (tieneObjeto)
        {
            controlNPC.ResponderSi();
            objetoQueDa.GetComponent<Items>().alcanzable = true;
        }
        else
        {
            //Habla
            Debug.Log("Necesito el plano");
            controlNPC.ResponderNo();
            

        }
    }




}
