using UnityEngine;

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

    [SerializeField]private GameObject startMenuCanvas;
    [SerializeField]private GameObject winMenuCanvas;
    [SerializeField]private GameObject loseMenuCanvas;

    private bool startMenu = false;

    public bool StartMenu{
        get{return startMenu;}
        set{
            startMenu = value;
            startMenuCanvas.SetActive(value);
        }
    }

    public bool WinMenuCanvas{
        set{winMenuCanvas.SetActive(value);}
    }

    public bool LoseMenuCanvas{
        set{loseMenuCanvas.SetActive(value);}
    }
}
