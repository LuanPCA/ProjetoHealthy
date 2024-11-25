using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public Text speech;
    public Text actorNameText;

    [Header("Settings")]
    //Adiciona velocidade ao texto;
    public float typingSpeed; 
    private string[] sentences;
    private int index;

    public void Speech(Sprite p, string[] txt, string actorName){
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence(){
        foreach (char letter in sentences[index].ToCharArray()){
            speech.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void NextSentence(){
        if(speech.text == sentences[index]){
            //Ainda tem textos
            if(index < sentences.Length -1){
                index++;
                speech.text = "";
                StartCoroutine(TypeSentence());
            }
            //Quando acaba os textos
            else{
                speech.text = "";
                index = 0;
                dialogueObj.SetActive(false);
            }
        }
    }

}
