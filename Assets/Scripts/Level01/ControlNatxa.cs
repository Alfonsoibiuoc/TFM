using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlNatxa : MonoBehaviour
{
    Data data;
    public ControladorEscena control;
    public Inventario inventario;
    Animator anim;
    Rigidbody rb;
    Collider _Collider;
    public float tiempo;
    public float distanciaGrounded;
    public float fuerzaSalto = 6f;
    public float fuerzaDobleSalto = 3f;
    public float multiplicadorCaida = 5f;
    public float multiplcadorSaltoPequeño = 2f;
    private LineRenderer lr;
    public Transform disparador;
    public Transform disparadorAgachado;
    public Transform disparadorEnPie;
    public ParticleSystem destelloDisparo;
    public float velocidadRotacion;
    public float velocidadMovimiento;
    public bool dobleSalto;
    public bool agachada;
    public float alturaRayo;
    public bool muerto;
    public GameObject efectoAgua;
    public GameObject efectoMuerte;
    public GameObject efectoAparecer;
    public bool grounded;
    public GameObject fuegoRayo;
    public GameObject marcaPosicion;
    public float rotacion;
    public GameObject marca01;
    public GameObject marca02;
    public GameObject marca03;
    public GameObject marca04;
    public float alturaSuelo;
    public float offsetAura;
    public bool rayoActivo;
    public bool asistenteDisparo;
    public float precision = 5f;
    public GameObject detectorEnemigos;
    public GameObject enemigoCercano;
    public GameObject destructibleCercano;
    public LayerMask mask = new LayerMask();
    public float dañoEnemigo;
    public bool golpeando;
    public bool bloqueoMovimiento;
    public bool tieneGolpeador;
    public bool tieneLlave;
    public bool tieneObjeto;
    public int vidaInicial;
    public int vidaActual;
    public int dañoPropio;
    public GameObject objetoActual;
    public Transform posicionManoDerecha;
    public LayerMask IgnorarRayo;
    AudioSource audioPlayer;
    public AudioClip[] audioSalto;
    public AudioClip[] audioGolpeada;
    public AudioClip audioGolpear;
    public AudioClip audioMorir;
    public AudioClip audioCaerAgua;
    public AudioClip audioAparecer;
    public AudioClip coger;
    public GameObject SonidoRayo;
    AudioSource audioRayo;
    bool golpeada;
    public bool eliminada;
    bool ahogada;



    private void Awake()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        inventario = GameObject.Find("ControladorEscena").GetComponent<Inventario>();
        
        bloqueoMovimiento = false;
    }

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioRayo = SonidoRayo.GetComponent<AudioSource>();
        audioPlayer.PlayOneShot(audioAparecer);
        detectorEnemigos = GameObject.Find("DetectorEnemigos");
        rayoActivo = false;
        golpeada = false;
        muerto = false;
        eliminada = false;
        ahogada = false;
        //rayoActivo = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        _Collider = GetComponent<CapsuleCollider>();
        //listaObjetosAlcanzados = new List<Collider>();
        golpeando = false;
        marcaPosicion.transform.localScale = (new Vector3(0.01f, 0.01f, 0.01f));
        tieneGolpeador = false;
        tieneLlave = false;
        vidaActual = vidaInicial;
    }

    
    void FixedUpdate()
    {
        if (!muerto)
        {
            GroundCheck(distanciaGrounded);
        }
    }

    void Update()
    {
        if (!muerto)
        {
            if (!bloqueoMovimiento)
            {
                Movimiento();
                Salto();
                Rayo();
                Agachada();
                MejorarSalto();
                ColocarMarcaPosicion();
                Golpear();
            }

            if(vidaActual <= 0)
            {
                if (!eliminada)
                {
                    StartCoroutine(Eliminada());
                }
                
            }

            //Debug.Log("Vida Player: " + vidaActual);  //Mostrar en GUI
        }   
    }

    void ColocarMarcaPosicion()
    {
        marcaPosicion.transform.Rotate(0, 0, 50 * Time.deltaTime);
        RaycastHit hit01;
        Physics.Raycast(_Collider.bounds.center, Vector3.down, out hit01);
        if (hit01.collider != null)
        {
            marca01.transform.position = new Vector3(marca01.transform.position.x, hit01.point.y + offsetAura, marca01.transform.position.z);
        }
        RaycastHit hit02;
        Physics.Raycast(_Collider.bounds.center, Vector3.down, out hit02);
        if (hit02.collider != null)
        {
            marca02.transform.position = new Vector3(marca02.transform.position.x, hit02.point.y + offsetAura, marca02.transform.position.z);
        }
        RaycastHit hit03;
        Physics.Raycast(_Collider.bounds.center, Vector3.down, out hit03);
        if (hit03.collider != null)
        {
            marca03.transform.position = new Vector3(marca03.transform.position.x, hit03.point.y + offsetAura, marca03.transform.position.z);
        }
        RaycastHit hit04;
        Physics.Raycast(_Collider.bounds.center, Vector3.down, out hit04);
        if (hit04.collider != null)
        {
            marca04.transform.position = new Vector3(marca04.transform.position.x, hit04.point.y + offsetAura, marca04.transform.position.z);
        }
    }

    void Movimiento()
    {
        Vector3 direccion = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 movimiento = Camera.main.transform.TransformDirection(direccion);
        movimiento.y = 0.0f;
        movimiento.Normalize();
        if (movimiento != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movimiento), Time.deltaTime * velocidadRotacion);
        }
        transform.Translate(movimiento * velocidadMovimiento * Time.deltaTime, Space.World);
        if ((direccion.x != 0 || direccion.z != 0))
        {
            anim.SetBool("Correr", true);
        }
        else
        {
            anim.SetBool("Correr", false);
        }
    }

    void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int sonidoSalto = Random.Range(0, audioSalto.Length);
            audioPlayer.PlayOneShot(audioSalto[sonidoSalto]);
            if (GroundCheck(distanciaGrounded))
            {
                rb.velocity = new Vector3(rb.velocity.x, fuerzaSalto, rb.velocity.z);
                dobleSalto = true;

            }else if (dobleSalto)
            {
                anim.SetTrigger("Voltereta");
                rb.velocity = new Vector3(rb.velocity.x, fuerzaDobleSalto, rb.velocity.z);
                dobleSalto = false; 
            }
        }

        //Modificamos el tamaño de la marca de la posición en función de la altura del Player.

        float cambioEscala = Mathf.Floor(transform.position.y - alturaSuelo) / 200;
        if(cambioEscala < 0.01f)
        {
            marcaPosicion.transform.localScale = (new Vector3(0.01f, 0.01f, 0.01f));
        }
        else if(cambioEscala > 0.025f)
        {
            marcaPosicion.transform.localScale = (new Vector3(0.025f, 0.025f, 0.025f));
        }
        else
        {
            marcaPosicion.transform.localScale = (new Vector3(cambioEscala, cambioEscala, cambioEscala));
        }
    }

    void MejorarSalto()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += new Vector3(rb.velocity.x, Physics.gravity.y * multiplicadorCaida * Time.deltaTime, rb.velocity.z);
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += new Vector3(rb.velocity.x, Physics.gravity.y * (multiplcadorSaltoPequeño - 1) * Time.deltaTime, rb.velocity.z);
        }
    }

    private bool GroundCheck(float distancia)
    {
        RaycastHit hit;
        Physics.Raycast(_Collider.bounds.center, Vector3.down, out hit, _Collider.bounds.extents.y + distancia);
        Color rayColor;
        if (hit.collider != null)
        {
            rayColor = Color.green;
            anim.SetBool("EnSuelo", true);
            grounded = true;
            alturaSuelo = hit.point.y;


        }
        else
        {
            rayColor = Color.red;
            anim.SetBool("EnSuelo", false);
            grounded = false;
        }
        Debug.DrawRay(_Collider.bounds.center, Vector3.down * (_Collider.bounds.extents.y + distancia), rayColor);
        return grounded;
    }

    void Golpear()
    {
        if (GroundCheck(distanciaGrounded) && !agachada)
        {
            if (Input.GetKey(KeyCode.Q) && tieneGolpeador)
            {
                audioPlayer.PlayOneShot(audioGolpear);
                bloqueoMovimiento = true;
                golpeando = true;
                anim.SetTrigger("Golpear");
            }
        }
    }

    public void DesbloquearMovimiento()
    {
        bloqueoMovimiento = false;
        golpeando = false;
    }

    void Rayo()
    {
        if (agachada)
        {
            disparador = disparadorAgachado;
        }
        else
        {
            disparador = disparadorEnPie;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            audioRayo.mute = false;
            rayoActivo = true;
            lr.enabled = true;
            anim.SetLayerWeight(1, 1);
            lr.SetPosition(0, disparador.position);
            RaycastHit hit;
            if (Physics.Raycast(disparador.position, disparador.forward, out hit,500f, mask))
            {
                if (hit.collider)
                {
                    lr.positionCount = 2;
                    //Situamos el "Detector de Enemigos" en la posición dónde a colisionado el rayo
                    detectorEnemigos.transform.position = hit.point;
                    //Obtenemos el enemigo más cercano desde el "Detector de Enemigos".
                    if(enemigoCercano != null)
                    {
                        
                        lr.SetPosition(1, enemigoCercano.transform.position);
                        //Quitamos Vida al enemigo.
                        enemigoCercano.GetComponent<EnemyHealth>().Golpeado(dañoEnemigo);   
                    }
                    else{
                        lr.SetPosition(1, hit.point);
                    }
                }
            }
            else
            {
                lr.SetPosition(1, disparador.forward * 5000);
            }
        }
        else
        {
            audioRayo.mute = true;
            anim.SetLayerWeight(1, 0);
            lr.enabled = false;
            rayoActivo = false;
            detectorEnemigos.transform.position = transform.position;
        }

    }

    void Agachada()
    {

     

        if (Input.GetKey(KeyCode.LeftShift))
        {
            agachada = true;
            anim.SetBool("Agachada", true);
            
        }
        else
        {
            agachada = false;
            anim.SetBool("Agachada", false);

           

        }
    }

    IEnumerator Eliminada()
    {
        eliminada = true;
        audioPlayer.PlayOneShot(audioMorir);
        if (efectoMuerte != null)
        {
            efectoMuerte.SetActive(true);
        }
        bloqueoMovimiento = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        StartCoroutine(control.InicioNivel());
        detectorEnemigos.GetComponent<DetectorEnemigos>().encontrado = false;
        control.playerActivo = false;
        bloqueoMovimiento = false;
    }

    IEnumerator Ahogada()
    {
        ahogada = true;
        audioPlayer.PlayOneShot(audioCaerAgua);
        efectoAgua.SetActive(true);
        bloqueoMovimiento = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        StartCoroutine(control.InicioNivel());
        detectorEnemigos.GetComponent<DetectorEnemigos>().encontrado = false;
        control.playerActivo = false;
        bloqueoMovimiento = false;
    }

    IEnumerator Golpeada()
    {
        if (!eliminada)
        {
            int sonidoGolpeada = Random.Range(0, audioGolpeada.Length);
            audioPlayer.PlayOneShot(audioGolpeada[sonidoGolpeada]);
            vidaActual -= dañoPropio;
            yield return new WaitForSeconds(0.5f);
            golpeada = false;
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Agua")
        {
            muerto = true;
            StartCoroutine(Ahogada());
        }
        Debug.Log(collision.transform.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "CheckPoint")
        {
            other.gameObject.SetActive(false);
            control.posicion++;
            data.posicion = control.posicion;
            

            
        }

        if (other.tag == "EnemigoMovil")
        {
            
                anim.SetLayerWeight(2, 0);
            
        }

        if (other.tag == "Enemigo")
        {
            
                anim.SetLayerWeight(2, 0);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Coger Un Objeto.

        if (Input.GetKeyDown(KeyCode.E))
        {
            

            if (other.tag == "Item")
            {
                if (other.transform.GetComponent<Items>().alcanzable)
                {
                    //Quitamos cualquier objeto que el player tenga en la mano.
                    int cantidadObjetos = posicionManoDerecha.transform.childCount;
                    for (int i = 0; i < cantidadObjetos; i++)
                    {
                        GameObject objeto;
                        objeto = posicionManoDerecha.transform.GetChild(i).gameObject;
                        if(objeto != null)
                        {
                            objeto.GetComponent<Items>().QuitarObjeto();
                        }
                    }
                    audioPlayer.PlayOneShot(coger);
                    GameObject itemRecogido = other.gameObject; 
                    Items item = itemRecogido.GetComponent<Items>();
                    inventario.AddItem(itemRecogido, item.ID, item.tipo, item.codigo, item.icono);
                    inventario.inventarioVisible = !inventario.inventarioVisible;
                    bloqueoMovimiento = true;
                    
                }
                else
                {
                    Debug.Log("Necesitas hacer el objeto alcanzable");
                }
            }
        }

        if (other.tag == "EnemigoMovil")
        {
            if (other.GetComponent<EnemigoMovilIA>().atacando)
            {
                anim.SetLayerWeight(2, 1);
                if (!golpeada)
                {
                    golpeada = true;
                    StartCoroutine(Golpeada());
                }
                
                
            }
            else
            {
                anim.SetLayerWeight(2, 0);
            }
        }

        if (other.tag == "Enemigo")
        {
            if (other.GetComponent<EnemigoQuietoIA>().atacando)
            {
                anim.SetLayerWeight(2, 1);
                if (!golpeada)
                {
                    golpeada = true;
                    StartCoroutine(Golpeada());
                }
            }
            else
            {
                anim.SetLayerWeight(2, 0);
            }
        }
    }
}
