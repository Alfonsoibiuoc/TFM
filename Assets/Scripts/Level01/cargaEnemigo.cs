using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargaEnemigo : MonoBehaviour
{
    EnemyHealth health;
    public GameObject carga;
    bool cargaSuelta;
    public float fuerzaImpulso;
    Vector3 impulso;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        cargaSuelta = false;
        impulso = new Vector3(0, fuerzaImpulso, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (health.muerto && !cargaSuelta)
        {
            cargaSuelta = true;
            BoxCollider collider = carga.AddComponent<BoxCollider>();
            
            //collider.size = new Vector3(2, 0.3f, 2);
            carga.GetComponent<SphereCollider>().enabled = true;
            carga.AddComponent<Rigidbody>();
            Rigidbody rb = carga.GetComponent<Rigidbody>();
            rb.AddForce(impulso, ForceMode.Impulse);
            carga.transform.parent = null;
            carga.GetComponent<Items>().alcanzable = true;
            
        }
    }
}
