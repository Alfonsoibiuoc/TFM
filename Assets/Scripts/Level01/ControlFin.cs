using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlFin : MonoBehaviour
{
    Data data;
    public GameObject Fade;
    public string siguienteEscena;
    public bool nivelEspecial;

    private void Start()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.tag == "Player")
        {
            if (!nivelEspecial)
            {
                data.posicion = 0;
            }
            else
            {
                data.posicion++;
            }
            
            Fade.GetComponent<Animator>().SetTrigger("Fade");
            SceneManager.LoadScene(siguienteEscena);
        }
    }


    
}
