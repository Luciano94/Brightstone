using UnityEngine;

public class InputManager : MonoBehaviour{
    static InputManager instance;
    IInput input;

    void Awake(){
        if (Instance == this){
            //DontDestroyOnLoad(gameObject);

            input = new InputPC();
        }
    }

    public float GetVerticalAxis()      { return input.GetVerticalAxis();       }
    public float GetHorizontalAxis()    { return input.GetHorizontalAxis();     }
    public bool GetBasicAttackButton()  { return input.GetBasicAttackButton();  }
    public bool GetStrongAttackButton() { return input.GetStrongAttackButton(); }
    public bool GetInteractButton()     { return input.GetInteractButton();     }
    public bool GetPauseButton()        { return input.GetPauseButton();        }
    public bool GetActionButton()       { return input.GetActionButton();       }
    public bool GetRestartButton()      { return input.GetRestartButton();      }

    static public InputManager Instance{
        get{
            if (!instance){
                instance = FindObjectOfType<InputManager>();
                if (!instance){
                    GameObject go = new GameObject("Managers");
                    instance = go.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }
}