using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlIntro : MonoBehaviour
{
    Data data;
    AudioSource audio;
    public AudioSource audioDespertador;
    public AudioSource audioCharco;
    public AudioSource audiolocura;
    public AudioClip golpe;

    public float velocidadCamaraRotar;

    public Animator animatorFade;
    public Animator animatorNatxaDurmiendo;
    public Animator animatorDespertador;
    public Animator animatorArmario;
    public Animator animatorAndar;
    public GameObject Andar;
    public Cinemachine.CinemachineVirtualCamera[] camaras;
    public GameObject cereales;
    public GameObject natxaCaer;
    public GameObject salpicadurasAugua;
    public GameObject Text01;
    public GameObject Text02;
    public GameObject Text03;
    public GameObject Text04;
    public GameObject Text05;
    public GameObject Text06;
    public GameObject Text07;
    public GameObject luzNoche;
    public GameObject luzHabitacion;
    public GameObject luzCalle;
    

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();
        velocidadCamaraRotar = 5f;
        audio = GetComponent<AudioSource>();
        for(int i = 0; i < camaras.Length; i++)
        {
            camaras[i].enabled = false;
        }
        StartCoroutine(EventosEscena());

        luzNoche.SetActive(true);
        luzHabitacion.SetActive(false);
        luzCalle.SetActive(false);
    }

    IEnumerator EventosEscena()
    {
        //Durmiendo 01
        camaras[0].enabled = true;
        yield return new WaitForSeconds(2);
        audio.Play();
        yield return new WaitForSeconds(2);
        camaras[0].enabled = false;
        //Durmiendo 01
        camaras[1].enabled = true;
        yield return new WaitForSeconds(5);
        camaras[1].enabled = false;
        //Despertador
        camaras[2].enabled = true;
        yield return new WaitForSeconds(1);
        audioDespertador.Play();
        animatorDespertador.SetBool("Sonar", true);
        yield return new WaitForSeconds(1);
        luzHabitacion.SetActive(true);
        yield return new WaitForSeconds(1);
        camaras[2].enabled = false;
        //Levantarse
        animatorNatxaDurmiendo.SetBool("Levantarse", true);
        camaras[3].enabled = true;
        yield return new WaitForSeconds(1.5f);
        camaras[3].enabled = false;
        animatorFade.SetBool("Fade", true);
        //Texto1
        yield return new WaitForSeconds(1.5f);
        Text01.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Text01.SetActive(false);
        camaras[4].enabled = true;
        yield return new WaitForSeconds(0.5f);
        animatorFade.SetBool("Fade", false);
        //Vestirse
        yield return new WaitForSeconds(1);
        animatorArmario.SetBool("cerrar", true);
        yield return new WaitForSeconds(1.5f);
        camaras[4].enabled = false;
        //Espejo
        camaras[5].enabled = true;
        yield return new WaitForSeconds(1.5f);
        camaras[5].enabled = false;
        //Cereales
        camaras[6].enabled = true;
        cereales.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        camaras[6].enabled = false;
        cereales.SetActive(false);
        //Leer
        camaras[7].enabled = true;
        yield return new WaitForSeconds(1.5f);
        camaras[7].enabled = false;
        
        animatorFade.SetBool("Fade", true);
        //Texto2
        yield return new WaitForSeconds(1.5f);
        Text02.SetActive(true);
        luzNoche.SetActive(false);
        luzHabitacion.SetActive(false);
        
        yield return new WaitForSeconds(2f);
        Text02.SetActive(false);
        camaras[8].enabled = true;
        yield return new WaitForSeconds(0.5f);
        animatorFade.SetBool("Fade", false);
        luzCalle.SetActive(true);
        animatorAndar.SetBool("Andar", true);
        //Calle
        yield return new WaitForSeconds(4);
        camaras[8].enabled = false;
        camaras[9].enabled = true;
        yield return new WaitForSeconds(2);
        camaras[9].enabled = false;
        camaras[10].enabled = true;
        yield return new WaitForSeconds(2);
        camaras[10].enabled = false;
        camaras[11].enabled = true;
        yield return new WaitForSeconds(1);
        camaras[11].enabled = false;
        camaras[10].enabled = true;
        yield return new WaitForSeconds(1);
        camaras[10].enabled = false;
        camaras[11].enabled = true;
        Andar.SetActive(false);
        animatorFade.SetBool("Fade", true);
        //Texto3
        yield return new WaitForSeconds(1.5f);
        Text03.SetActive(true);
        yield return new WaitForSeconds(3f);
        Text03.SetActive(false);
        camaras[12].enabled = true;
        yield return new WaitForSeconds(0.5f);
        animatorFade.SetBool("Fade", false);
        natxaCaer.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        salpicadurasAugua.SetActive(true);
        audioCharco.Play();
        yield return new WaitForSeconds(2f);
        camaras[12].enabled = false;
        camaras[13].enabled = true;
        salpicadurasAugua.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        audio.clip = golpe;
        audio.pitch = 0.3f;
        audio.Play();
        yield return new WaitForSeconds(5f);
        animatorFade.SetBool("Fade", true);
        yield return new WaitForSeconds(3f);
        //Ida de olla
        audiolocura.Play();
        camaras[13].enabled = false;
        camaras[14].enabled = true;
        animatorFade.SetBool("Fade", false);
        yield return new WaitForSeconds(2f);
        velocidadCamaraRotar = 40;
        yield return new WaitForSeconds(1f);
        animatorFade.SetBool("Fade", true);
        yield return new WaitForSeconds(3f);
        animatorFade.SetBool("Fade", false);
        yield return new WaitForSeconds(2f);
        velocidadCamaraRotar = 80;
        yield return new WaitForSeconds(1f);
        animatorFade.SetBool("Fade", true);
        //Texto4
        yield return new WaitForSeconds(2f);
        Text04.SetActive(true);
        yield return new WaitForSeconds(4f);
        Text04.SetActive(false);
        Text05.SetActive(true);
        yield return new WaitForSeconds(4f);
        Text05.SetActive(false);
        Text06.SetActive(true);
        yield return new WaitForSeconds(4f);
        Text06.SetActive(false);
        Text07.SetActive(true);
        yield return new WaitForSeconds(4f);
        Text07.SetActive(false);
        yield return new WaitForSeconds(1f);
        data.DatosRanuras[data.ranuraActual].Mundos[0] = true;
        SceneManager.LoadScene("Jardin-01");
    }
    
}
