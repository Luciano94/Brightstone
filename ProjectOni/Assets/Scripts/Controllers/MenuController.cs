using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour{
    const int level1Index = 1;

    public void Play(){
        SceneManager.LoadScene(level1Index);
    }

    public void Exit(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
