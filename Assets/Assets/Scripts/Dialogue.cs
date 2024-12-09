using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Dialogue : MonoBehaviour
{
   public Sprite profile;
   public string[] speech;
   public string actorName;
   public float radius;
   public LayerMask playerLayer;

   public DialogueControl dc;

   bool onRadius;

   private void Start(){
    //dc = FindAnyObjectByType<DialogueControl>();
   
   }

   private void FixedUpdate(){
    Interact();

   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space) && onRadius){
         dc.Speech(profile, speech, actorName);
      }
   }

   public void Interact(){

    Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

    if(hit != null){
      onRadius = true;
    }
    else{
      onRadius = false;
    }

   }

   private void OnDrawGizmosSelected()
   {
      Gizmos.DrawWireSphere(transform.position, radius);
   }
}

//Input.GetKeyDown(KeyCode.Space)