using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{
    const int loadingIndex = 1;
    [SerializeField]Button playButton;

    private void Awake() {
        playButton.Select();
    }

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
