using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlInicio : MonoBehaviour
{
    Data data;
    public int numRanuras = 3;
    public GameObject[] ranurasVacias;
    public GameObject[] ranurasConDatos;
    public GameObject panelConfirmacion;
    public int numEliminar;
    bool pantallainicio = true;
    public Animator animacionPanelInicio;

    void Start()
    {

        data = GameObject.Find("DATA").GetComponent<Data>();
        data.Cargar();

        switch (data.DatosRanuras.Count)
        {
            case 0:
                for (int i = 0; i < numRanuras; i++)
                {
                    ranurasVacias[i].SetActive(true);
                    ranurasConDatos[i].SetActive(false);
                }
                break;
            case 1:
                ranurasConDatos[0].SetActive(true);
                ranurasConDatos[1].SetActive(false);
                ranurasConDatos[2].SetActive(false);
                ranurasVacias[0].SetActive(false);
                ranurasVacias[0].SetActive(true);
                ranurasVacias[0].SetActive(true);
                ranurasConDatos[0].GetComponentInChildren<Text>().text = data.DatosRanuras[0].FechaGuardado + "\n" + data.DatosRanuras[0].Escena + "\n" + "\n" + "Continuar";
                break;
            case 2:
                ranurasConDatos[0].SetActive(true);
                ranurasConDatos[1].SetActive(true);
                ranurasConDatos[2].SetActive(false);
                ranurasVacias[0].SetActive(false);
                ranurasVacias[0].SetActive(false);
                ranurasVacias[0].SetActive(true);
                ranurasConDatos[0].GetComponentInChildren<Text>().text = data.DatosRanuras[0].FechaGuardado + "\n" + data.DatosRanuras[0].Escena + "\n" + "\n" + "Continuar";
                ranurasConDatos[1].GetComponentInChildren<Text>().text = data.DatosRanuras[1].FechaGuardado + "\n" + data.DatosRanuras[1].Escena + "\n" + "\n" + "Continuar";
                break;
            case 3:
                for (int i = 0; i < numRanuras; i++)
                {
                    ranurasVacias[i].SetActive(false);
                    ranurasConDatos[i].SetActive(true);
                }
                ranurasConDatos[0].GetComponentInChildren<Text>().text = data.DatosRanuras[0].FechaGuardado + "\n" + data.DatosRanuras[0].Escena + "\n" + "\n" + "Continuar";
                ranurasConDatos[1].GetComponentInChildren<Text>().text = data.DatosRanuras[1].FechaGuardado + "\n" + data.DatosRanuras[1].Escena + "\n" + "\n" + "Continuar";
                ranurasConDatos[2].GetComponentInChildren<Text>().text = data.DatosRanuras[2].FechaGuardado + "\n" + data.DatosRanuras[2].Escena + "\n" + "\n" + "Continuar";
                break;
        }
    }

    private void Update()
    {
        if (pantallainicio)
        {
            if (Input.anyKeyDown)
            {
                pantallainicio = false;
                animacionPanelInicio.SetBool("salir", true);
            }
        }
    }

    public void salir()
    {
        
            
                pantallainicio = true;
                animacionPanelInicio.SetBool("salir", false);
            
        
    }

    public void NuevaPartida1()
    {
        data.ranuraActual = 0;
        bool[] mundos = new bool[data.numMundos];
        bool[] coleccionables = new bool[data.numColeccionables];

        data.DatosRanuras.Add(new Data.Ranura(data.Fecha(), "Introduccion",mundos,coleccionables));
        data.Guardar();
        SceneManager.LoadScene("Introduccion");
    }
    public void NuevaPartida2()
    {
        data.ranuraActual = 1;
        bool[] mundos = new bool[data.numMundos];
        bool[] coleccionables = new bool[data.numColeccionables];
        data.DatosRanuras.Add(new Data.Ranura(data.Fecha(), "Introduccion", mundos, coleccionables));
        data.Guardar();
        SceneManager.LoadScene("Introduccion");
    }
    public void NuevaPartida3()
    {
        data.ranuraActual = 2;
        bool[] mundos = new bool[data.numMundos];
        bool[] coleccionables = new bool[data.numColeccionables];
        data.DatosRanuras.Add(new Data.Ranura(data.Fecha(), "Introduccion", mundos, coleccionables));
        data.Guardar();
        SceneManager.LoadScene("Introduccion");
    }

    public void cargar1()
    {
        data.ranuraActual = 0;
        SceneManager.LoadScene(data.DatosRanuras[0].Escena);
    }
    public void cargar2()
    {
        data.ranuraActual = 1;
        SceneManager.LoadScene(data.DatosRanuras[1].Escena);
    }
    public void cargar3()
    {
        data.ranuraActual = 2;
        SceneManager.LoadScene(data.DatosRanuras[2].Escena);
    }
    public void botonEliminar(int num)
    {
        numEliminar = num;
        panelConfirmacion.SetActive(true);
    }

    public void eliminar()
    {
        panelConfirmacion.SetActive(false);
        data.DatosRanuras.RemoveAt(numEliminar);
        data.Guardar();
        SceneManager.LoadScene("Inicio");
    }
    

    public void noEliminar()
    {
        panelConfirmacion.SetActive(false);
    }
    

}
