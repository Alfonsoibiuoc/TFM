using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlBoss : MonoBehaviour
{
    Data data;

    AudioSource sonido;
    public AudioSource audioFondo;
    public AudioClip sonidoNatxa;
    public AudioClip sonidoCarmen;
    public AudioClip sonidoExplosion;

    ControladorEscena control;
    public Animator bossAnimator;
    public Animator cameraAnimator;
    public Cinemachine.CinemachineVirtualCamera camaraInicio;
    public Cinemachine.CinemachineVirtualCamera camaraNatxa;
    public GameObject NatxaInicio;
    public GameObject PanelDialogos;
    public Text Texto;
    public Image Avatar;
    public Sprite ImagenCarmen;
    public Sprite ImagenNatxa;
    public string dialogo01;
    public string dialogo02;
    public string dialogo03;
    public string dialogo04;
    public string dialogo05;

    public GameObject lanzador;
    public Transform destino;
    public Transform[] posicionesDestino;
    int posicionDestino;
    bool lanzadorActivo;

    public GameObject explosionPrefab;
    public List<GameObject> enemigosEnDestino;
    public GameObject explosionPavoPrefab;

    public Transform tDestino;

    public int contadorEnemigos;
    bool final = false;
    public Animator animatorFade;


    void Start()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();
        sonido = GetComponent<AudioSource>();
        control = GetComponent<ControladorEscena>();
        enemigosEnDestino = new List<GameObject>();
        camaraNatxa.enabled = false;
        StartCoroutine(EventosInicio());
        lanzadorActivo = false;
        contadorEnemigos = control.enemigosMoviles.Length;

    }

    IEnumerator EventosInicio()
    {
        data.DatosRanuras[data.ranuraActual].FechaGuardado = data.Fecha();
        data.DatosRanuras[data.ranuraActual].Escena = SceneManager.GetActiveScene().name;
        data.Guardar();

        for (int i = 0; i < control.enemigosMoviles.Length; i++)
        {
            control.enemigosMoviles[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        Texto.text = dialogo01;
        Avatar.sprite = ImagenCarmen;
        cameraAnimator.SetTrigger("Inicio");
        yield return new WaitForSeconds(7);
        //Habla Carmen
        sonido.PlayOneShot(sonidoCarmen);
        bossAnimator.SetTrigger("Hablar");
        PanelDialogos.SetActive(true);
        NatxaInicio.SetActive(true);
        yield return new WaitForSeconds(4);
        //Habla Natxa
        sonido.PlayOneShot(sonidoNatxa);
        camaraInicio.enabled = false;
        camaraNatxa.enabled = true;
        Texto.text = dialogo02;
        Avatar.sprite = ImagenNatxa;
        yield return new WaitForSeconds(3);
        sonido.PlayOneShot(sonidoCarmen);
        camaraInicio.enabled = true;
        camaraNatxa.enabled = false;
        Texto.text = dialogo03;
        Avatar.sprite = ImagenCarmen;
        yield return new WaitForSeconds(3);
        sonido.PlayOneShot(sonidoNatxa);
        camaraInicio.enabled = false;
        camaraNatxa.enabled = true;
        Texto.text = dialogo04;
        Avatar.sprite = ImagenNatxa;
        yield return new WaitForSeconds(3);
        bossAnimator.SetBool("Luchar", true);
        camaraInicio.enabled = false;
        camaraNatxa.enabled = false;
        control.camaras[0].enabled = true;
        PanelDialogos.SetActive(false);
        Destroy(NatxaInicio);
        control.enabled = true;

    }

    private void Update()
    {
        if (control.playerActivo && contadorEnemigos > 0)
        {
            if (!lanzadorActivo)
            {
                posicionDestino = Random.Range(0, posicionesDestino.Length);
                activarLanzador();
            }
        }
        else if(final)
        {
            final = false;
            StartCoroutine(EventosFinal());
        }

        
    }

    void activarLanzador()
    {
        lanzadorActivo = true;
        destino.position = Vector3.Lerp(destino.position, posicionesDestino[posicionDestino].position, 1);
        lanzador.SetActive(true);
        StartCoroutine(mantenerLanzador());

    }

    IEnumerator mantenerLanzador()
    {
        tDestino.localScale = new Vector3(5f, 5f, 5f);
        yield return new WaitForSeconds(1);
        tDestino.localScale = Vector3.Lerp(tDestino.localScale, tDestino.localScale * 2, Time.deltaTime*15);
        yield return new WaitForSeconds(1);
        tDestino.localScale = Vector3.Lerp(tDestino.localScale, tDestino.localScale * 2, Time.deltaTime * 15);
        yield return new WaitForSeconds(1);
        tDestino.localScale = Vector3.Lerp(tDestino.localScale, tDestino.localScale * 2f, Time.deltaTime * 15);
        yield return new WaitForSeconds(1);
        tDestino.localScale = Vector3.Lerp(tDestino.localScale, tDestino.localScale * 2f, Time.deltaTime * 15);
        yield return new WaitForSeconds(1);
        tDestino.localScale = Vector3.Lerp(tDestino.localScale, tDestino.localScale * 2f, Time.deltaTime * 15);
        yield return new WaitForSeconds(1);
        GameObject explosion = Instantiate(explosionPrefab,destino.position, posicionesDestino[posicionDestino].rotation);
        sonido.PlayOneShot(sonidoExplosion);

        if (enemigosEnDestino.Count > 0)
        {
            for(int i = 0; i < enemigosEnDestino.Count; i++)
            {
                if (enemigosEnDestino[i].GetComponent<EnemyHealth>().vida <= 0)
                {
                    enemigosEnDestino[i].GetComponent<EnemigoMovilIA>().muerteDefinitiva = true;
                    enemigosEnDestino[i].SetActive(false);
                    GameObject explosionPavo = Instantiate(explosionPavoPrefab, enemigosEnDestino[i].transform.position, enemigosEnDestino[i].transform.rotation);
                    Destroy(explosionPavo, 3);
                    enemigosEnDestino[i].GetComponent<Collider>().enabled = false;
                    enemigosEnDestino.Remove(enemigosEnDestino[i]);
                    contadorEnemigos--;
                    if(contadorEnemigos <= 0)
                    {
                        final = true;
                    }
                }
            }
        }
        lanzador.SetActive(false);
        enemigosEnDestino.Clear();
        yield return new WaitForSeconds(2);
        lanzadorActivo = false;
        Destroy(explosion);
    }


    IEnumerator EventosFinal()
    {
        sonido.PlayOneShot(sonidoCarmen);
        camaraInicio.enabled = true;
        camaraNatxa.enabled = false;
        Texto.text = dialogo05;
        Avatar.sprite = ImagenCarmen;
        bossAnimator.SetBool("Final", true);
        yield return new WaitForSeconds(5);
        animatorFade.SetBool("Fade", true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Hall");
    }







    }
