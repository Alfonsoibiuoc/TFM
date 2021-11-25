using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMovement : MonoBehaviour
{
    //Rigidbody rb;
    public List<Vector3> Puntos;
    public LineRenderer linea;
    public Vector3 SiguientePosicion;
    public int posicionArray;
    public float tiempo = 0.01f;
    public float direccion;

    private void Start()
    {
        posicionArray = 0;
        for (int i = 0; i < linea.positionCount; i++)
        {
            Puntos.Add(linea.GetPosition(i));
        }
        SiguientePosicion = Puntos[posicionArray];
    }

    private void Update()
    {
        Mover();  
    }

    void Mover()
    {
        direccion = Input.GetAxisRaw("Horizontal");

        if (direccion > 0)
        {
            if (posicionArray < Puntos.Count - 1)
            {
                    posicionArray++;
                    transform.LookAt(Puntos[posicionArray + 1]);
            } 
        }
        if (direccion < 0)
        {
            if (posicionArray > 0)
            {    
                    posicionArray--;
                    transform.LookAt(Puntos[posicionArray - 1]);                  
            }  
        }

        transform.position = Vector3.Lerp(transform.position, Puntos[posicionArray], tiempo);
    }  
}
