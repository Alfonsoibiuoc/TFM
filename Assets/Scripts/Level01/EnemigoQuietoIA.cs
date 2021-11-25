using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoQuietoIA : MonoBehaviour
{
    ControladorEscena control;
    EnemyHealth _health;
    Collider _collider;
    public int estado;      //0-Idle 1-Mirando 2-Atacando
    Animator anim;
    float distanciaPlayer;
    public float distanciaAlerta;
    public float distanciaAtaque;
    public Transform player;
    //public bool muerto = false;
    public bool atacando;
    



    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        control = GameObject.Find("ControladorEscena").GetComponent<ControladorEscena>();
        anim = GetComponent<Animator>();
        estado = 0;
        _health = GetComponent<EnemyHealth>();
        atacando = false;
        
    }


    void Update()
    {
        
            if (control.playerActivo)
            {
                player = GameObject.FindWithTag("Player").transform;

            }



            if (!_health.muerto && !control.pausa)
            {
                _collider.enabled = true;
                if (player != null)
                {
                    distanciaPlayer = Vector3.Distance(player.position, transform.position);

                    if (distanciaPlayer < distanciaAlerta)
                    {
                        if (distanciaPlayer < distanciaAtaque)
                        {
                            //Ataque;
                            anim.SetTrigger("Ataque");
                            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

                        }
                        else
                        {
                            //Alerta
                            anim.SetBool("Alerta", true);
                            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                        }
                    }
                    else
                    {
                        anim.SetBool("Alerta", false);
                    }



                }


            }
            else if (!control.pausa)
            {
                //MUERTO
                anim.SetBool("Muerte", true);
                _collider.enabled = false;
            }
        else
        {

            
            _collider.enabled = false;
            
        }








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
