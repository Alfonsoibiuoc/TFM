using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float vidaInicial;
    public float vida;
    public bool muerto;
    public GameObject efectoGolpeado;
    public float tiempoResurreccion;


    void Start()
    {
        vida = vidaInicial;
        muerto = false;

    }

    public void Golpeado(float daño)
    {

        //float vidaAnterior = vida;
        vida -= daño;

        if (vida <= 0)
        {
            StartCoroutine(Muerto());
        }
        else
        {
            Instantiate(efectoGolpeado, transform.position, Quaternion.identity);

        }
    }

    IEnumerator Muerto()
    {

        muerto = true;
        yield return new WaitForSeconds(tiempoResurreccion);
        muerto = false;
        GetComponentInParent<Animator>().SetBool("Muerte", false);

        if(GetComponentInParent<EnemigoMovilIA>() != null)
        {
            GetComponentInParent<EnemigoMovilIA>().estado = 0;
        }
        vida = vidaInicial;
    }
}
