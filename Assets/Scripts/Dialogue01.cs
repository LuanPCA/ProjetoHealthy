using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue01 : MonoBehaviour
{
    public string[] speechTxt;
    public LayerMask playerLayer;
    private DialogueController dc;
    public float radious; 
    public Vector3 offset;
    bool onRadious;

    public void Start()
    {
        dc = FindObjectOfType<DialogueController>();
    }

    public void FixedUpdate()
    {
        Interect();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onRadious)
        {
            dc.Speech(speechTxt);
        }
    }

    public void Interect()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,radious, playerLayer);

        if (hit != null)
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + offset ,radious);
    }
}
