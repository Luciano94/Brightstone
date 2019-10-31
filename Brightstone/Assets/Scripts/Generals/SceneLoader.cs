using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{

    private const bool TUTORIAL_IS_ON = false;
    [SerializeField] private Text loadingText;

    const int level1Index = 2;
    const int tutorialIndex = 4;

    private void Start(){
        StartCoroutine(LoadSceneAsync());
    }
    private void Update(){
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    private IEnumerator LoadSceneAsync(){
        int sceneToLoad;
        int isTutorial = PlayerPrefs.GetInt("isTutorial", -1);
#if TUTORIAL_IS_ON
        if(isTutorial == -1){
            PlayerPrefs.SetInt("isTutorial", 1);
            sceneToLoad = tutorialIndex;
        }else{
            sceneToLoad = level1Index;
        }
#else
        sceneToLoad = level1Index;
#endif
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!async.isDone)
            yield return null;
    }
}
