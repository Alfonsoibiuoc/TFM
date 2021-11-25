using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemigoMovilIA : MonoBehaviour
{
    Rigidbody rb;
    ControladorEscena control;
    EnemyHealth _health;
    Collider _collider;

    private int p_estado;
    public int estado;      //0-Paseando 1-Persiguiendo 2-Atacando

    public delegate void OnVariableChangeDelegate(int nuevoEstado);
    public event OnVariableChangeDelegate OnVariableChange;


    NavMeshAgent agent;
    Animator anim;
    AudioSource audioEnemigo;
    public Transform[] target;
    public int destino;
    public float distanciaDestino = 2;
    float distanciaPlayer;
    public float distanciaAlerta;
    public float distanciaAtaque;
    public Transform player;
    //public bool muerto = false;
    public float velocidadPerseguir;
    public float velocidadPaseo;
    public bool atacando;
    public AudioClip sonidoPaseando;
    public AudioClip sonidoPersiguiendo;
    public AudioClip sonidoAtacando;

    public bool muerteDefinitiva = false;




    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioEnemigo = GetComponent<AudioSource>();
        estado = 0;
        p_estado = 0;
        destino = CambiarDestino();
        _health = GetComponent<EnemyHealth>();
        atacando = false;
        rb = GetComponent<Rigidbody>();
        OnVariableChange += CambioDeEstado;
        
    }


    void Update()
    {
            if (control.playerActivo)
            {
                player = GameObject.FindWithTag("Player").transform;
            }

            if (!_health.muerto && !control.pausa && !muerteDefinitiva)
            {
                rb.isKinematic = false;
                agent.isStopped = false;
                _collider.enabled = true;
                if (player != null)
                {
                    distanciaPlayer = Vector3.Distance(player.position, transform.position);

                    if (distanciaPlayer < distanciaAlerta)
                    {
                        if (distanciaPlayer < distanciaAtaque)
                        {
                            if (!player.GetComponent<ControlNatxa>().eliminada)
                            {
                                //Ataque;
                                estado = 2;
                                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                                anim.SetTrigger("Atacar");
 
                            }
                            else
                            {
                                estado = 0;
                            }   
                        }
                        else
                        {
                            if (!player.GetComponent<ControlNatxa>().eliminada)
                            {
                                //Alerta
                                anim.SetBool("Perseguir", true);
                                agent.speed = velocidadPerseguir;
                                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                                estado = 1;
                        }
                            else
                            {
                                estado = 0;
                            }
                    }
                    }
                    else
                    {
                        //Pasear
                        anim.SetBool("Perseguir", false);
                        agent.speed = velocidadPaseo;
                        estado = 0;
                    }
                    switch (estado)
                    {
                        //Pasear
                        case 0:

                            float distanciaObjetivo = Vector3.Distance(target[destino].position, transform.position);
                            agent.isStopped = false;
                            agent.destination = target[destino].position;
                            if (distanciaObjetivo < distanciaDestino)
                            {
                                destino = CambiarDestino();
                            }
                        break;

                        //Perseguir
                        case 1:

                            agent.isStopped = false;
                            agent.destination = player.position;
                        break;

                        //Atacar
                        case 2:

                            agent.isStopped = true;
                        break;
                    }

                if (estado != p_estado && OnVariableChange != null)
                {
                    p_estado = estado;
                    OnVariableChange(estado);
                }

                }
                else
                {
                    //Pasear
                    anim.SetBool("Perseguir", false);
                    agent.speed = velocidadPaseo;
                    estado = 0;
                }
            }
            else if (!control.pausa && !muerteDefinitiva)
            {
                //MUERTO
                anim.SetBool("Muerte", true);
                rb.isKinematic = true;
                _collider.enabled = false;
                agent.isStopped = true;
                audioEnemigo.Stop();
                estado = 0;
            }
            else{

                rb.isKinematic = true;
                _collider.enabled = false;
                agent.isStopped = true;
                audioEnemigo.Stop();
                estado = 0;
        }


        

    }

    private void CambioDeEstado(int nuevoEstado)
    {
            switch (estado)
            {
                case 0:
                    audioEnemigo.clip = sonidoPaseando;
                    audioEnemigo.Play();
                    break;
                case 1:
                    audioEnemigo.clip = sonidoPersiguiendo;
                    audioEnemigo.Play();
                    break;
                case 2:
                    audioEnemigo.clip = sonidoAtacando;
                    audioEnemigo.Play();
                    break;
            }

    }

    int CambiarDestino()
    {
        int posicionDestino;
        posicionDestino = Random.Range(0, target.Length);
        while (posicionDestino == destino)
        {
            posicionDestino = Random.Range(0, target.Length);
        }
        return posicionDestino;
    }

    public void Ataque()
    {
        atacando = true;
    }

    public void Descanso()
    {
        atacando = false;
    }

    


    
    
}
