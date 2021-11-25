using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestructor : MonoBehaviour
{

    public float daño;
    GameObject player;
    
    

    // Start is called before the first frame update
    void Start()
    {

        player = transform.root.gameObject;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destructible" && player.GetComponent<ControlNatxa>().golpeando)
        {
            if(other.GetComponent<ExplosionCaja>() != null)
            {
                other.GetComponent<ExplosionCaja>().Explotar();
            }
            
            
            
        }
    }
}
