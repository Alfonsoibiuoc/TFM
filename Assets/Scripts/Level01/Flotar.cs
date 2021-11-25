using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Flotar : MonoBehaviour
{
    public float nivelAgua = 0.0f;
    public float umbralFlotacion = 2.0f;
    public float Densidad = 0.125f;
    public float fuerzaHaciaAbajo = 4.0f;
    private float fuerza;
    private Vector3 fuerzaFlotacion;

    void FixedUpdate()
    {
        fuerza = 1.0f - ((transform.position.y - nivelAgua) / umbralFlotacion);
        if(fuerza > 0.0f)
        {
            fuerzaFlotacion = -Physics.gravity * GetComponent<Rigidbody>().mass * (fuerza - GetComponent<Rigidbody>().velocity.y * Densidad);
            fuerzaFlotacion += new Vector3(0.0f, -fuerzaHaciaAbajo * GetComponent<Rigidbody>().mass, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(fuerzaFlotacion, transform.position);
        }
    }   
}
