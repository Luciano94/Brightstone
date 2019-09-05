using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour{
    private static TextGenerator instance;

    public static TextGenerator Instance {
        get {
            instance = FindObjectOfType<TextGenerator>();
            if(instance == null) {
                GameObject go = new GameObject("TextGenerator");
                instance = go.AddComponent<TextGenerator>();
            }
            return instance;
        }
    }

    [Header("Background")]
    [SerializeField] private Animator anim;
    
    [Header("Space Txt")]
    [SerializeField] private Text pressSpaceTxt;
    [SerializeField] private float timeToAppear;

    private Text text;
    private float timeLeft;

    private void Awake(){
        text = GetComponent<Text>();
    }

    private void Update(){
        if (timeLeft > 0.0f && !pressSpaceTxt.enabled){
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0.0f)
                pressSpaceTxt.enabled = true;
        }
    }

    public void Show(string info){
        text.text = info;
    }

    public void Appear(){
        anim.SetTrigger("In");
        timeLeft = timeToAppear;
    }

    public void Hide(){
        anim.SetTrigger("Out");
        timeLeft = 0.0f;
        pressSpaceTxt.enabled = false;
    }
}
