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

    private void Awake(){
        text = GetComponent<Text>();
    }

    public void Show(string info){
        backgroundImg.enabled = true;
        text.enabled = true;
        text.text = info;
    }

    public void Hide(){
        text.text = "";
        text.enabled = false;
        backgroundImg.enabled = false;
    }
}
