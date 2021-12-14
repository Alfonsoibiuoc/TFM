using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelInformacion : MonoBehaviour
{
    public ControladorEscena control;
    public Inventario controlInventario;
    public ControlNatxa controlNatxa;
    public AudioSource audioCanvas;
    public bool infoVisible;
    public GameObject panelInfo;
    public GameObject panelInventario;
    

    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        controlInventario = GameObject.Find("ControladorEscena").GetComponent<Inventario>();
        infoVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (control.playerActivo)
        {
            controlNatxa = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlNatxa>();
        }
        if (Input.GetKeyDown(KeyCode.I) && control.playerActivo && !controlInventario.inventarioVisible)
        {
            audioCanvas.Play();
            if (controlNatxa.golpeando == false)
            {
                infoVisible = !infoVisible;
                controlNatxa.bloqueoMovimiento = !controlNatxa.bloqueoMovimiento;
            }
        }
        if (infoVisible)
        {
            panelInfo.SetActive(true);
            control.pausa = true;
            
        }
        else
        {
            panelInfo.SetActive(false);
            if (!controlInventario.inventarioVisible)
            {
                control.pausa = false;
            }
            

        }
    }
}
