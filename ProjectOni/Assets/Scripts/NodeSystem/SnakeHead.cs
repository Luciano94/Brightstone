using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SnakeHead : MonoBehaviour{
    float nodeSize = 20;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.B)){
            SceneManager.LoadScene(0);
        }   
    }
}
