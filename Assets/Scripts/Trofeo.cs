using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trofeo : MonoBehaviour
{
    Data data;
    public int nTrofeo;
    public GameObject panelTrofeo;
    public GameObject efecto;

    void Start()
    {
        data = GameObject.Find("DATA").GetComponent<Data>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            data.DatosRanuras[data.ranuraActual].Coleccionables[nTrofeo] = true;
            data.Guardar();
            StartCoroutine(TrofeoRecogido());

        }
    }

    IEnumerator TrofeoRecogido()
    {
        panelTrofeo.SetActive(true);
        Instantiate(efecto);
        yield return new WaitForSeconds(4);
        panelTrofeo.SetActive(false);
        Destroy(efecto, 3);
        Destroy(this.gameObject);
    }
}
