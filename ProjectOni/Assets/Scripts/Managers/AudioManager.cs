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

    [Header("Player")]
    [SerializeField] private AudioSource playerDeath;
    [SerializeField] private AudioSource[] playerAttack;
    private bool isAttack = false;
    [SerializeField] private AudioSource playerParry;

    [Header("Enemy")]
    [SerializeField] private AudioSource[] enemyAttack;
    private bool enemyIsAttack = false;

    [Header("Ingame")]
    [SerializeField] private AudioSource roomStart;
    [SerializeField] private AudioSource roomFinished;

    [Header("Menu")]
    [SerializeField] private AudioSource menuHit;

    [Header("Music")]
    [SerializeField] private AudioSource theme;

    public void Awake(){
        Invoke("StartTheme", 1.0f);
    }

    private void StartTheme(){
        theme.Play();
    }

    public void StopTheme(){
        theme.Stop();
    }

    public void MenuHit(){
        menuHit.Play();
    }

    public void PlayerAttack(){
        int attack = Random.Range(0, playerAttack.Length);
        playerAttack[attack].Play();
        playerAttack[attack].pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlayerDeath(){
        playerDeath.Play();
    }

    public void PlayerParry(){
        playerParry.Play();
    }

    public void EnemyAttack(){
        int attack = Random.Range(0, enemyAttack.Length);
        
        enemyAttack[attack].Play();
    }

    public void RoomStart(){
        roomStart.Play();
    }

    public void RoomFinished(){
        roomFinished.Play();
    }
}
