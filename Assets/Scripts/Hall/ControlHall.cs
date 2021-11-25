using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlHall : MonoBehaviour
{
    public GameObject TextoAccesoDenegado;
    public GameObject TextoAcceder;
    public GameObject PanelPuerta;
    public int numMundo;
    public string mundoDestino;

    private void Start()
    {
        PanelPuerta.SetActive(false);
        TextoAcceder.SetActive(false);
        TextoAccesoDenegado.SetActive(false);
    }
    public void Acceder()
    {
        Debug.Log("Acceder");
        switch (numMundo)
        {
            case 0:
                mundoDestino = "Jardin-01";
                cambioEscena();
                break;
            case 1:
                mundoDestino = "";
                cambioEscena();
                break;
            case 2:
                mundoDestino = "";
                cambioEscena();
                break;
            case 3:
                mundoDestino = "";
                cambioEscena();
                break;
            case 4:
                mundoDestino = "";
                cambioEscena();
                break;
            case 5:
                mundoDestino = "";
                cambioEscena();
                break;
            case 6:
                mundoDestino = "";
                cambioEscena();
                break;
        }

        
    }

    public void cambioEscena()
    {
        SceneManager.LoadScene(mundoDestino);
    }
}
