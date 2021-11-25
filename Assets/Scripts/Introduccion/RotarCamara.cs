using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarCamara : MonoBehaviour
{
    float velocidad;
    ControlIntro control;

    private void Start()
    {
        control = GameObject.Find("ControladorEscena").GetComponent<ControlIntro>();
    }
    void Update()
    {
        velocidad = control.velocidadCamaraRotar;
        transform.Rotate(Vector3.up * velocidad * Time.deltaTime);
    }
}
