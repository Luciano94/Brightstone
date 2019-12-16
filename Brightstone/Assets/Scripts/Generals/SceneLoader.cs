using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{

    private const bool TUTORIAL_IS_ON = false;
    [SerializeField] private Text loadingText;

    const int mainSceneIndex = 1;

    private void Start(){
        StartCoroutine(LoadSceneAsync());
    }
    private void Update(){
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    private IEnumerator LoadSceneAsync(){
        int sceneToLoad;
        
        sceneToLoad = mainSceneIndex;

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!async.isDone)
            yield return null;
    }
}
