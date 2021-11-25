using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTrofeos : MonoBehaviour
{
    Data data;
    public GameObject[] Trofeos;
    void Start()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();

        for(int i = 0; i < Trofeos.Length; i++)
        {
            if (data.DatosRanuras[data.ranuraActual].Coleccionables[i])
            {
                Trofeos[i].SetActive(true);
            }
        }
    }


}
