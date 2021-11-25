using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorEscena : MonoBehaviour
{
    Data data;
    public AudioMixerSnapshot Normal;
    public AudioMixerSnapshot LowPass;

    public bool pausa;
    public GameObject prefabPlayer;
    public GameObject player;
    public Transform posicionador;
    public int posicion;
    public Transform[] posicionesInicio;
    public GameObject prefabEfectoAparecer;
    public GameObject efectoAparecer;
    public bool playerActivo;
    public Cinemachine.CinemachineVirtualCamera[] camaras;
    public GameObject[] enemigosMoviles;
    public bool nivelEspecial;

    private void Awake()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();
    }

    private void Start()
    {
        data.DatosRanuras[data.ranuraActual].FechaGuardado = data.Fecha();
        data.DatosRanuras[data.ranuraActual].Escena = SceneManager.GetActiveScene().name;
        data.Guardar();

        pausa = false;
        playerActivo = false;
        StartCoroutine(InicioNivel());

        for (int i = 0; i < camaras.Length; i++)
        {
            camaras[i].enabled = false;
        }

        if (!nivelEspecial)
        {
            posicion = data.posicion;
        }
        else
        {
            posicion = 0;
        }
        
    }

    public IEnumerator InicioNivel()
    {
        for(int i = 0; i< camaras.Length; i++)
        {
            camaras[i].m_LookAt = posicionesInicio[posicion];
            //camaras[i].m_Follow = posicionesInicio[posicion];
        }
        for(int i = 0; i < enemigosMoviles.Length; i++)
        {
            if(!enemigosMoviles[i].GetComponent<EnemigoMovilIA>().muerteDefinitiva)
            {
                enemigosMoviles[i].SetActive(true);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        Reaparecer();
        
    }

   
    public void Reaparecer()
    {
        if(player != null)
        {
            Destroy(player.gameObject);
            playerActivo = false;

        }
        efectoAparecer = Instantiate(prefabEfectoAparecer, posicionesInicio[posicion].position, Quaternion.identity); 
        player = Instantiate(prefabPlayer, posicionesInicio[posicion].position, Quaternion.identity);
        playerActivo = true;
        posicionador = player.transform.GetChild(3);

        for (int i = 0; i < camaras.Length; i++)
        {
            camaras[i].m_LookAt = posicionador.transform;
            //camaras[i].m_Follow = posicionador.transform;
        }

        /*c_VirtualCamera.m_LookAt = posicionador.transform;
        c_VirtualCamera.m_Follow = posicionador.transform;*/

    }

    public void ActivarLowPass()
    {
        LowPass.TransitionTo(2.0f);
    }
    public void DesactivarLowPass()
    {
        Normal.TransitionTo(2.0f);
    }
}
