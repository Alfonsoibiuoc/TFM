using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosFondo : MonoBehaviour
{
    AudioSource sonidoFondo;
    public AudioClip[] sonidos;
    
    int sonidoElegido;
    int tiempoEntreSonidos;

    // Start is called before the first frame update
    void Start()
    {
        sonidoFondo = GetComponent<AudioSource>();
        StartCoroutine(LanzadorSonidosFondo());
    }

    IEnumerator LanzadorSonidosFondo()
    {
        tiempoEntreSonidos = Random.Range(4, 10);
        sonidoElegido = Random.Range(0, sonidos.Length);
        sonidoFondo.PlayOneShot(sonidos[sonidoElegido]);
        yield return new WaitForSeconds(tiempoEntreSonidos);
        StartCoroutine(LanzadorSonidosFondo());
    }
}
