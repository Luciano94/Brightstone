using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour{
    private static MenuManager instance;

    public static MenuManager Instance {
        get {
            instance = FindObjectOfType<MenuManager>();
            if(instance == null) {
                GameObject go = new GameObject("MenuManager");
                instance = go.AddComponent<MenuManager>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject winMenuCanvas;
    [SerializeField] private GameObject stateUI;
    [SerializeField] private Button buttonToSelect;
    [SerializeField] private float timeToActivateStateUi;
    [SerializeField] private float timeToRestartGame;

    private bool startMenu = false;

    public bool StartMenu{
        get { return startMenu; }
        set {
            startMenu = value;
            //startMenuCanvas.SetActive(value);
        }
    }

    public bool WinMenuCanvas{
        set {
            Invoke("ActivateWinCanvas", 2.75f);
        }
    }

    public bool LoseMenuCanvas{
        set {
            Invoke("ActivateLoseCanvas", 2.5f);
        }
    }

    private void ActivateWinCanvas(){
        UIManager.Instance.RunFinished();
        winMenuCanvas.SetActive(true);
        Invoke("ActivateStateUI", timeToActivateStateUi);
    }

    private void ActivateLoseCanvas(){
        UIManager.Instance.YouDieImgAppear();
        SoundManager.Instance.PostMortem();
        Invoke("RestartGame", timeToRestartGame);
    }

    private void ActivateStateUI(){
        stateUI.SetActive(true);
        buttonToSelect.Select();
    }

    private void RestartGame(){
        GameManager.Instance.Restart();
    }
}
