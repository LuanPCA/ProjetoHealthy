using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colidiu com: " + collision.gameObject.name);
    }

}
