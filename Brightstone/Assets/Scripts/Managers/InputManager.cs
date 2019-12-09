using UnityEngine;

public class InputManager : MonoBehaviour{
    static InputManager instance;
    private IInput input;

    private bool isConnected;

    void Awake(){
        if (Instance == this){
            DetectDevice();
            
            Cursor.visible = false;
            input = new InputPC();
        }
    }

    void Update(){
        DetectDevice();

        if (input.IsJoystick() && !isConnected){
            Cursor.visible = true;
            input = new InputPC();
        } else if (!input.IsJoystick() && isConnected){
            Cursor.visible = false;
            input = new InputXBox();
        }

        // Quizas tengamos que hacer un #if unity editor == true que sea de esta manera
        // Si es la build, que solo se pueda jugar con Joystick
    }

    public float GetVerticalAxis()      { return input.GetVerticalAxis();       }
    public float GetHorizontalAxis()    { return input.GetHorizontalAxis();     }
    public bool GetBasicAttackButton()  { return input.GetBasicAttackButton();  }
    public bool GetStrongAttackButton() { return input.GetStrongAttackButton(); }
    public bool GetInteractButton()     { return input.GetInteractButton();     }
    public bool GetPauseButton()        { return input.GetPauseButton();        }
    public bool GetActionButton()       { return input.GetActionButton();       }
    public bool GetRestartButton()      { return input.GetRestartButton();      }
    public bool GetPassButton()         { return input.GetPassButton();         }
    public bool GetActionZone()         {return input.GetActionZone();          }
    public bool GetActionShuriken()     {return input.GetActionShuriken();      }
    public bool GetActionBeatdown()     {return input.GetActionBeatdown();      }    
    public bool GetActionThrust()       {return input.GetActionThrust();        }
    public bool GetActionDash()         {return input.GetActionDash();          }
    public bool GetActionSimpleAttack() {return input.GetActionSimpleAttack();  }

    private void DetectDevice(){
        if (Input.GetJoystickNames().Length > 0){
            if(Input.GetJoystickNames().Length == 1 && Input.GetJoystickNames()[0].Length > 10){
                isConnected = true;
                //UIManager.Instance.UnshowPause();
                //Time.timeScale = 1.0f;
            }
            else{
                isConnected = false;
                //UIManager.Instance.ShowPause();
                //Time.timeScale = 0.0f;
            }
        }
        else{
            isConnected = false;
            //UIManager.Instance.ShowPause();
            //Time.timeScale = 0.0f;
        }

    }

    public bool IsConnected{
        get{return isConnected;}
    }

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