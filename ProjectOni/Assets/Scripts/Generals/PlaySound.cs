using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]private AudioSource sound;


    public void Play(){
        sound.Play();
    }
}
