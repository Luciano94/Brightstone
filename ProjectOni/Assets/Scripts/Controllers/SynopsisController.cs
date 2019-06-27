using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SynopsisController : MonoBehaviour{
    [SerializeField] private RectTransform synopsisTxt;
    [SerializeField] private float txtSpeed;
    [SerializeField] private Button skipBtn;

    private const int loadingIndex = 3;

    private void Start(){
        skipBtn.Select();
    }

    private void Update(){
        if (synopsisTxt.anchoredPosition.y < -10.0f)
            synopsisTxt.Translate(0.0f, txtSpeed * Time.deltaTime, 0.0f);
    }

    public void StartRun(){
        SceneManager.LoadScene(loadingIndex);
    }
}
