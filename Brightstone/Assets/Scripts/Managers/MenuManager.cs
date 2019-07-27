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

    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject winMenuCanvas;
    [SerializeField] private GameObject loseMenuCanvas;
    [SerializeField] private GameObject stateUI;
    [SerializeField] private Button buttonToSelect;
    [SerializeField] private float timeToActivateStateUi;

    private bool startMenu = false;

    public bool StartMenu{
        get { return startMenu; }
        set {
            startMenu = value;
            startMenuCanvas.SetActive(value);
        }
    }

    public bool WinMenuCanvas{
        set {
            winMenuCanvas.SetActive(value);
            Invoke("ActivateStateUI", timeToActivateStateUi);
        }
    }

    public bool LoseMenuCanvas{
        set {
            loseMenuCanvas.SetActive(value);
            Invoke("ActivateStateUI", timeToActivateStateUi);
        }
    }

    private void ActivateStateUI(){
        stateUI.SetActive(true);
        buttonToSelect.Select();
    }
}
