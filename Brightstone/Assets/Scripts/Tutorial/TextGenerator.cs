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

    [SerializeField] private Image backgroundImg;
    private Text text;
    private Animator anim;

    private void Awake(){
        text = GetComponent<Text>();
        anim = backgroundImg.GetComponent<Animator>();
    }

    public void Show(string info){
        text.text = info;
    }

    public void Appear(){
        anim.SetTrigger("In");
    }

    public void Hide(){
        anim.SetTrigger("Out");
    }
}
