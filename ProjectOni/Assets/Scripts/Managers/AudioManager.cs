using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance {
        get {
            instance = FindObjectOfType<AudioManager>();
            if(instance == null) {
                GameObject go = new GameObject("AudioManager");
                instance = go.AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    [SerializeField]private AudioSource playerDeath;
    [SerializeField]private AudioSource[] playerAttack;
    private bool isAttack = false;
    [SerializeField]private AudioSource playerParry;
    [SerializeField]private AudioSource[] enemyAttack;
    private bool enemyIsAttack = false;
    [SerializeField]private AudioSource menuHit;

    public void MenuHit(){
        menuHit.Play();
    }

    public void PlayerAttack(){
        int attack = Random.Range(0, playerAttack.Length);
        if(!playerAttack[attack].isPlaying)
            playerAttack[attack].Play();
    }

    public void PlayerDeath(){
        playerDeath.Play();
    }

    public void PlayerParry(){
        playerParry.Play();
    }

    public void EnemyAttack(){
        int attack = Random.Range(0, enemyAttack.Length);
        if(!enemyAttack[attack].isPlaying)
            enemyAttack[attack].Play();
    }
}
