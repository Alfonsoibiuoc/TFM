using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectorEnemigos : MonoBehaviour
{
    public GameObject Player;
    public ControladorEscena control;
    public bool encontrado;

    public List<Collider> enemigosCercanos;
    public float distanciaMinima;
    public GameObject enemigoCercano;

    void Start()
    {

        enemigosCercanos = new List<Collider>();
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        encontrado = false;
    }

    void Update()
    {
         if (control.playerActivo && !encontrado)
                {

                    Player = GameObject.FindWithTag("Player");
                    encontrado = true;

                }

        if (encontrado)
        {

            for (int i = 0; i < enemigosCercanos.Count; i++)
            {
                float distanciaObjeto = Vector3.Distance(Player.transform.position, enemigosCercanos[i].bounds.center);
                
                if (distanciaMinima == 0)
                {
                    distanciaMinima = distanciaObjeto;
                }
                else
                {
                    if (distanciaObjeto < distanciaMinima)
                    {
                        distanciaMinima = distanciaObjeto;
                        enemigoCercano = enemigosCercanos[i].gameObject;
                    }
                }


            }

            if (enemigosCercanos.Count != 0)
            {
                Player.GetComponent<ControlNatxa>().enemigoCercano = enemigoCercano;
            }
            else
            {
                
                    Player.GetComponent<ControlNatxa>().enemigoCercano = null;
                
                
                distanciaMinima = 0;
            }

            if (!Player.GetComponent<ControlNatxa>().rayoActivo)
            {
                enemigosCercanos.Clear();
                enemigoCercano = null;
            }
        }
   
    }



    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "EnemigoMovil" || other.tag == "Enemigo")
        {
            if(other.GetComponent<EnemyHealth>().vida > 0)
            {
                if (!enemigosCercanos.Contains(other))
                {
                    enemigosCercanos.Add(other);
                }
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {


        if (other.tag == "EnemigoMovil" || other.tag == "Enemigo")
        {
            enemigosCercanos.Remove(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.tag == "EnemigoMovil" || other.tag == "Enemigo")
        {
            if (!enemigosCercanos.Contains(other))
            {
                enemigosCercanos.Add(other);
            }
        }
    }


}
