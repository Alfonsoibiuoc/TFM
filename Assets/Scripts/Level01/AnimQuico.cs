using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimQuico : MonoBehaviour
{
    Animator anim;
    ControlNPC control;
    void Start()
    {
        anim = GetComponent<Animator>();
        control = GetComponent<ControlNPC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (control.completado)
        {
            anim.SetBool("Relajado", true);
        }
        else
        {
            anim.SetBool("Relajado", false);
        }
    }
}
