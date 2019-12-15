using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Combo{
    public List<int> combo;
}

[System.Serializable]
public struct ActionInfo{
    public Action action;
    public ColliderType colType;
    public Vector2 offset;
    public Vector2 size;
    public float radius;
}

public enum ColliderType{
    Box,
    Circle
}

public enum Stands
{
    Beatdown = 0,
    Thrust,
    Shuriken,
    Zone,
    Count,
    None
}

public class ComboManager : MonoBehaviour{
    [SerializeField]private List<Combo> Combos;
    private int comboIndex; //posicion del combo en ese momento
    [SerializeField]private List<int> activeCombos;//array con los combos que estan ejecutandose
    private ActionInfo currentActionInfo; //estado de la accion que se esta ejecutando en ese momento
    [SerializeField]private List<ActionInfo> actions; //acciones posibles
    [SerializeField]private StandsManager standsManager;
    private bool found = false;
    private bool isCombination = false;
    public Stands actualStand { get; private set; }
    private Stands lastStand;

    private void Awake() {
        activeCombos = new List<int>();
        comboIndex = 0;
        currentActionInfo.action = null;
        actualStand = lastStand = Stands.Beatdown;
        standsManager.ActivateStand(actualStand);
    }


    private void Update() {
        if(currentActionInfo.action != null && 
        !currentActionInfo.action.IsActive){
            comboIndex = 0;
            activeCombos.Clear();
            isCombination = false;
            currentActionInfo.action = null;
        }
    }

    public void ManageAction(Actions actionNumber){
        if(currentActionInfo.action == null){
            comboIndex = 0;
            //inicializa el arreglo de combos;
            for (int i = 0; i < Combos.Count; i++){
                if(Combos[i].combo[comboIndex] < actions.Count){
                    if(actions[Combos[i].combo[comboIndex]].action.actionName == actionNumber){   
                        activeCombos.Add(i);
                    }
                }
            }
            //se busca la accion
            if(activeCombos.Count > 0){
                currentActionInfo = actions[Combos[activeCombos[0]].combo[comboIndex]];

                HandleAction(currentActionInfo.action.actionName);
                CheckIfIsCombination(currentActionInfo.action.standToPlay);

                //se pone play a la accion
                int comboIndexValue = Combos[activeCombos[0]].combo[comboIndex];
                float finalIndex = (int)actualStand +
                                    (isCombination ? (int)Stands.Count : 0) +
                                    (comboIndexValue <= 9 ? comboIndexValue * 0.1f : comboIndexValue * 0.01f);
                
                currentActionInfo.action.StartAction(currentActionInfo, finalIndex);

                ManageSound(currentActionInfo.action.actionName, comboIndexValue);
            }
            
            //se inicializa el combo index
            comboIndex = 1;
        }else{
            if(activeCombos.Count > 0 && 
            comboIndex >= Combos[activeCombos[0]].combo.Count){

                comboIndex = 0;
                activeCombos.Clear();
                found = false;

            }else{
                found = false;
                //si esta en el tiempo de encadenar
                if (currentActionInfo.action.Fdata.State == ActionState.activeFrames) {
                    //coincide la action con la del comboindex
                    int index = 0;
                    for (int i = 0; i < activeCombos.Count; i++) {
                        if (actionNumber == actions[Combos[activeCombos[i]].combo[comboIndex]].action.actionName)
                        {
                            //if (actualStand == actions[Combos[activeCombos[i]].combo[comboIndex]].standToPlay) {
                            //    actualStand = actions[Combos[activeCombos[i]].combo[comboIndex]].standToPlay;
                                currentActionInfo.action.StopAction();
                                currentActionInfo = actions[Combos[activeCombos[i]].combo[comboIndex]];
                                //actualStand = currentAction.standToPlay;
                                HandleAction(currentActionInfo.action.actionName);
                                CheckIfIsCombination(currentActionInfo.action.standToPlay);
                                found = true;
                                index = i;
                                break;
                            //}
                        }
                    }
                    if(found){
                        //se pone play a esa accion.
                        int comboIndexValue = Combos[activeCombos[index]].combo[comboIndex];
                        float finalIndex = (int)actualStand +
                                           (isCombination ? (int)Stands.Count : 0) +
                                           (comboIndexValue <= 9 ? comboIndexValue * 0.1f : comboIndexValue * 0.01f);
                        
                        currentActionInfo.action.StartAction(currentActionInfo, finalIndex);
                        
                        ManageSound(currentActionInfo.action.actionName, comboIndexValue);
                    }
                        
                    //se quitan los que no coinciden.
                    for (int i = 0; i < activeCombos.Count; i++){
                        if(comboIndex == Combos[activeCombos[i]].combo.Count ||
                            actions[Combos[activeCombos[i]].combo[comboIndex]].action.actionName != actionNumber){               
                            activeCombos.RemoveAt(i);
                        }
                    }
                    //se aumenta el combo index.
                    comboIndex++;
                //sino
                }else{
                    //se limpia el arreglo de combos posibles.
                    comboIndex = 0;
                    activeCombos.Clear();
                }
            }
        }
    }

    private bool CheckIfIsCombination(Stands standToPlay){
        if (!isCombination && lastStand != standToPlay && comboIndex > 0)
            isCombination = true;
        
        return isCombination;
    }

    public void AddCombo(List<int> newCombo){
        Combo combo;
        combo.combo = newCombo;
        Combos.Add(combo);
    }

    public void ChangeCombo(int comboIndex, List<int> newCombo){
        Combo combo;
        combo.combo = newCombo;
        Combos[comboIndex] = combo;
    }

    public int ActualComboIndex(){
        return comboIndex;
    }

    private void HandleAction(Actions actionNumber){
        lastStand = actualStand;

        switch (actionNumber){
            case Actions.X:
                actualStand = Stands.Beatdown;
                break;
            case Actions.Y:
                actualStand = Stands.Thrust;
                break;
            case Actions.B:
                actualStand = Stands.Shuriken;
                break;
            case Actions.A:
                actualStand = Stands.Zone;
                break;
        }
        standsManager.ActivateStand(actualStand);
    }

    private void ManageSound(Actions actionNumber, int comboIndex){
        switch (actionNumber){
            case Actions.X:
                if (!isCombination){
                    if (comboIndex < 4)
                        SoundManager.Instance.PlayerAttackX();
                    else
                        SoundManager.Instance.PlayerAttackXx5();
                }
                else if (comboIndex == 31)
                    SoundManager.Instance.PlayerComboB1();
                else if (comboIndex == 32)
                    SoundManager.Instance.PlayerComboB2();
                else if (comboIndex == 33)
                    SoundManager.Instance.PlayerComboB3();
                else if (comboIndex == 34)
                    SoundManager.Instance.PlayerComboB4();
                else if (comboIndex == 35)
                    SoundManager.Instance.PlayerComboB5();
                break;
            case Actions.Y:
                if (!isCombination){
                    if (comboIndex < 9)
                        SoundManager.Instance.PlayerAttackY();
                    else
                        SoundManager.Instance.PlayerAttackYx5();
                }
                break;
            case Actions.B:
                if (!isCombination)
                    SoundManager.Instance.PlayerAttackB();
                break;
            case Actions.A:
                if (!isCombination){
                    if (comboIndex < 17)
                        SoundManager.Instance.PlayerAttackA();
                    else
                        SoundManager.Instance.PlayerAttackAx7();
                }
                else if (comboIndex == 25)
                    SoundManager.Instance.PlayerComboA1();
                else if (comboIndex == 26)
                    SoundManager.Instance.PlayerComboA2();
                else if (comboIndex == 27)
                    SoundManager.Instance.PlayerComboA3();
                else if (comboIndex == 28)
                    SoundManager.Instance.PlayerComboA4();
                break;
        }
    }
}