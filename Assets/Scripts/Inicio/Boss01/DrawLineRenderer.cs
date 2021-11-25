using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineRenderer : MonoBehaviour
{
    public Transform Point1;
    Vector3 Point2;
    public Transform Point3;
    LineRenderer linerenderer;
    public float vertexCount = 12;
    public float AlturaMaxima = 50;
    public float anchoInicial = 1;
    public float anchoFinal = 1;

    public Texture[] texturas;
    public int animationStep;

    public float fps = 30f;
    private float contadorFPS;

    


    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        linerenderer.startWidth = anchoInicial;
        linerenderer.endWidth = anchoFinal;

        contadorFPS += Time.deltaTime;
        if (contadorFPS >= 1f / fps)
        {
            animationStep++;
            if(animationStep == texturas.Length - 1)
            {
                animationStep = 0;
            }
            linerenderer.material.SetTexture("_MainTex", texturas[animationStep]);

            contadorFPS = 0f;
        }

        Point2 = new Vector3((Point1.transform.position.x + Point3.transform.position.x)/2, AlturaMaxima, (Point1.transform.position.z + Point3.transform.position.z) / 2);

        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(Point1.position, Point2, ratio);
            var tangent2 = Vector3.Lerp(Point2, Point3.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        linerenderer.positionCount = pointList.Count;
        linerenderer.SetPositions(pointList.ToArray());
    }
}
