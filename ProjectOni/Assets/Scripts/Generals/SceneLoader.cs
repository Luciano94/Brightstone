using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{
    [SerializeField] private Text loadingText;

    const int level1Index = 2;

    private void Start(){
        StartCoroutine(LoadSceneAsync());
    }
    private void Update(){
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    private IEnumerator LoadSceneAsync(){
        AsyncOperation async = SceneManager.LoadSceneAsync(level1Index);
        while (!async.isDone)
            yield return null;
    }
}
