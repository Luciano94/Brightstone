using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{
    const int synopsisIndex = 1;

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

    [Header("Stats")]
    [SerializeField] Button statsButton;
    [SerializeField] Button backButton;

    private int isTutorial;
    private float timeReset = -1.0f;

    private void Start(){
        Cursor.visible = false;

        playButton.Select();
        MenuPanel.Play("mainMenuIn");
        SoundManager.Instance.MenuOpen();
    }
    
    public void Play(){
        SceneManager.LoadScene(synopsisIndex);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.X)){
            PlayerPrefs.DeleteKey("isTutorial");
            timeReset = 2.0f;
        }
        ShowTutorialReset();
    }

    private void ShowTutorialReset(){
        if(timeReset > 0){
            timeReset -= Time.deltaTime;
            tutorialReset.enabled = true;
        }else{
            tutorialReset.enabled = false;
        }
    }

    public void Stats(){
        RunSaver.LoadHistory();

        FillDataInTexts();

        backButton.Select();
    }

    public void ResetStats(){
        RunSaver.ResetHistory();

        FillDataInTexts();
    }

    public void BackToMenu(){
        statsButton.Select();
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
