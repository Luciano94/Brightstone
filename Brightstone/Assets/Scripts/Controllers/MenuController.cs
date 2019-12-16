using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{
    const int loadingSceneIndex = 2;

    [SerializeField] Button playButton;
    [SerializeField] Text runTimeAvgTxt;
    [SerializeField] Text bestTimeTxt;
    [SerializeField] Text enemiesKilledTxt;
    [SerializeField] Text bossesKilledTxt;
    [SerializeField] Text totalExpTxt;
    [SerializeField] Text totalDmgDealtTxt;
    [SerializeField] Text totalDeathsTxt;
    [SerializeField] Text roomsDiscoveredTxt;
    [SerializeField] Text actualLevelTxt;

    [SerializeField] Text tutorialReset;
    [SerializeField] Animator MenuPanel;
    [SerializeField] Animator OptionsPanel;

    [Header("Buttons")]
    [SerializeField] Button statsButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button backButton;
    [SerializeField] Button optionsBackButton;
    [SerializeField] Button creditsBackButton;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer brightstoneImg;

    private bool reduceAlpha = false;

    private void Start(){
        playButton.Select();
        MenuPanel.Play("mainMenuIn");
        SoundManager.Instance.MenuOpen();
    }
    
    public void Play(){
        SceneManager.LoadScene(loadingSceneIndex);
    }

    private void Update() {
        CheckBrightstoneAlpha();
    }

    private void CheckBrightstoneAlpha(){
        if (reduceAlpha && brightstoneImg.color.a >= 0.1f){
            Color tempC = brightstoneImg.color;
            tempC.a -= Time.deltaTime;
            brightstoneImg.color = tempC;
        }
        else if (!reduceAlpha && brightstoneImg.color.a < 1.0f){
            Color tempC = brightstoneImg.color;
            tempC.a += Time.deltaTime;
            brightstoneImg.color = tempC;
        }
    }

    public void Stats(){
        reduceAlpha = true;

        RunSaver.LoadHistory();

        FillDataInTexts();

        backButton.Select();
    }

    public void Options(){
        optionsBackButton.Select();
    }

    public void Credits(){
        reduceAlpha = true;

        creditsBackButton.Select();
    }

    public void ResetStats(){
        RunSaver.ResetHistory();

        FillDataInTexts();
    }

    public void BackToOptions(){
        reduceAlpha = false;

        statsButton.Select();
    }

    public void BackToCredits(){
        reduceAlpha = false;

        creditsButton.Select();
    }

    public void BackToMenu(){
        optionsButton.Select();
    }

    private void FillDataInTexts(){
        HistoryDataManager.Data data = RunSaver.GetHistoryData();
        
        if (data.bestTime != float.MaxValue){
            string minutes = Mathf.Floor(data.bestTime / 60).ToString("00");
            string seconds = Mathf.Floor(data.bestTime % 60).ToString("00");
            bestTimeTxt.text = "" + minutes + ":" + seconds;
        }
        else
            bestTimeTxt.text = "-";

        enemiesKilledTxt.text = "" + data.enemiesKilled;
        bossesKilledTxt.text = "" + data.bossesKilled;
        totalExpTxt.text = "" + data.totalExp;
        totalDmgDealtTxt.text = "" + data.totalDamageDealt;
        totalDeathsTxt.text = "" + data.totalDeaths;
        roomsDiscoveredTxt.text = "" + data.roomsDiscovered;
        actualLevelTxt.text = "" + data.actualLevel;
    }

    public void Exit(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
