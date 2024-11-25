using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRig;
    private Animator anima;
    private Vector2 movement;
    public float speed = 5f;
    
    //Controlar velocidade do player
    public float _playerSpeed;
    //Controlar direçao do player. Eixo X e Y
    private Vector2 _playerDirection; 
    
    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Captura das teclas
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Zera o movimento diagonal
        if (horizontal != 0) vertical = 0;

        // Atualiza o movimento
        movement = new Vector2(horizontal, vertical).normalized;

        // Atualiza o Animator
        //anima.SetFloat("Horizontal", movement.x);
        //anima.SetFloat("Vertical", movement.y);
        //anima.SetFloat("Speed", movement.sqrMagnitude);
        MovePlayer();
       
    }
    void FixedUpdate()
    {
        // Movimenta o personagem
       // transform.Translate(movement * speed * Time.fixedDeltaTime);
    }
    void MovePlayer()
    {
        // Captura os valores dos eixos
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Zera a diagonal: prioriza o movimento horizontal
        if (horizontal != 0) vertical = 0;

        // Aplica o movimento ao personagem
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement * _playerSpeed * Time.deltaTime;

        // Animação no eixo "X"
        if (horizontal > 0f)
        {
            anima.SetBool("walkRight", true);
            anima.SetBool("walkLeft", false);
            //transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (horizontal < 0f)
        {
            anima.SetBool("walkLeft", true);
            anima.SetBool("walkRight", false);
            //transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            anima.SetBool("walkRight", false);
            anima.SetBool("walkLeft", false);
        }

        // Animação no eixo "Y"
        if (vertical > 0f)
        {
            anima.SetBool("walkUp", true);
            anima.SetBool("walkDown", false);
        }
        else if (vertical < 0f)
        {
            anima.SetBool("walkDown", true);
            anima.SetBool("walkUp", false);
        }
        else
        {
            anima.SetBool("walkUp", false);
            anima.SetBool("walkDown", false);
        }
    }

    
}
