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
        SoundManager.Instance.SynopsisOpen();
    }

    private void Update(){
        if (synopsisTxt.anchoredPosition.y < -15.0f)
            synopsisTxt.Translate(0.0f, txtSpeed * Time.deltaTime, 0.0f);
    }

    public void StartRun(){
        SoundManager.Instance.StopSounds();
        SceneManager.LoadScene(loadingIndex);
    }
}
