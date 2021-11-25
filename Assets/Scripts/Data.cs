using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Globalization;

[Serializable]
public class Data : MonoBehaviour
{
    //Variable para no duplicar el Data entre escenas
    private static Data dataInstancia;

    //Datos a guardar por ranura
    public List<Ranura> DatosRanuras;

    public int ranuraActual;
    public int numMundos = 7;
    public int numColeccionables = 50;
    public int posicion = 0;

    //Mantener el objeto entre escenas
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (dataInstancia == null)
        {
            dataInstancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string Fecha()
    {
        //Calcular la fecho y hora actuales.
        DateTime fechalocal = DateTime.Now;
        DateTime fechaUTC = DateTime.UtcNow;
        String nombreRegion = "es-ES";
        var region = new CultureInfo(nombreRegion);
        String fechaHora = fechalocal.ToString(region);
        Debug.Log(fechaHora);
        return fechaHora;
    }

    [Serializable] 
    public class Ranura
    {
        public string FechaGuardado;
        public string Escena;
        public bool[] Mundos;
        public bool[] Coleccionables;
        

        public Ranura(string fechaGuardado, string escena, bool[] mundos, bool[] coleccionables)
        {
            FechaGuardado = fechaGuardado;
            Escena = escena;
            Mundos = mundos;
            Coleccionables = coleccionables;

        }
    }



    //Guardar/Cargar----------------------------------------------------------------------------------
    [Serializable]
    public class DatosAGuardar
    {
        public List<Ranura> DatosRanuras;
       
        public DatosAGuardar(List<Ranura> DatosRanura) 
        {
            this.DatosRanuras = DatosRanura;
            
        }
    }

    public void Guardar()
    { 
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DatosRanuras.dataNatxa");
                DatosAGuardar datos = new DatosAGuardar(DatosRanuras);
                datos.DatosRanuras = DatosRanuras;
                bf.Serialize(file, datos);
                file.Close(); 
    }

    public void Cargar()
    {
        if (File.Exists(Application.persistentDataPath + "/DatosRanuras.dataNatxa"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/DatosRanuras.dataNatxa", FileMode.Open);
            DatosAGuardar datos = (DatosAGuardar)bf.Deserialize(file);
            //Actualizamos las variables con los datos guardados
            DatosRanuras = datos.DatosRanuras;
            file.Close();
        }
        else
        {
            DatosRanuras = new List<Ranura>();
    }
    
    }
}
