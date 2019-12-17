using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadd : MonoBehaviour
{
    void Awake() 
    { 
        DontDestroyOnLoad(gameObject); 
    } 
}
