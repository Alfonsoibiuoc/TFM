using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCaja : MonoBehaviour
{
    AudioSource audioCaja;

    public GameObject efectoGolpeado;
    public float tiempoDestruccion;
    public GameObject ObjetoEnInterior;
    public bool destruido;
    BoxCollider colliderCaja;



    public List<GameObject> Childs;



    void Start()
    {
        audioCaja = GetComponent<AudioSource>();
        destruido = false;
        colliderCaja = GetComponent<BoxCollider>();
        Childs = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Childs.Add(transform.GetChild(i).gameObject);
        }

        
    }



    public void Explotar()
    {
        if (!destruido)
        {
            audioCaja.Play();
            colliderCaja.enabled = false;
            destruido = true;
            Instantiate(efectoGolpeado,transform.position, Quaternion.identity);
            for (int i = 0; i < Childs.Count; i++)
            {
                Childs[i].AddComponent<Rigidbody>();
                Destroy(Childs[i], tiempoDestruccion);
            }
            GameObject objeto = Instantiate(ObjetoEnInterior, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            objeto.GetComponent<Items>().alcanzable = true;
        }
        else
        {
            Debug.Log("Ya está destruido");
        }
        
    }


}
