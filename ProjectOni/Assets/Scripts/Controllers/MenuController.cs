using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour{
    const int loadingIndex = 2;

    public void Play(){
        SceneManager.LoadScene(loadingIndex);
    }

    public void Exit(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
