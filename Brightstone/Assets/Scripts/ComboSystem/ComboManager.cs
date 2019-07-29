using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Combo
{
    public List<int> combo;
}

public class ComboManager : MonoBehaviour{
    [SerializeField]private List<Combo> Combos;
    private int comboIndex; //posicion del combo en ese momento
    [SerializeField]private List<int> activeCombos;//array con los combos que estan ejecutandose
    private Action currentAction; //estado de la accion que se esta ejecutando en ese momento
    [SerializeField]private Action[] actions; //acciones posibles
    private bool found = false;

    private void Awake() {
        activeCombos = new List<int>();
        comboIndex = 0;
        currentAction = null;
    }


    private void Update() {
        if(currentAction != null && 
        !currentAction.IsActive){
            comboIndex = 0;
            activeCombos.Clear();
            currentAction = null;
        }
    }

    public void ManageAction(Actions actionNumber){
        if(currentAction == null){
            comboIndex = 0;
            //inicializa el arreglo de combos;
            for (int i = 0; i < Combos.Count; i++){
                if(Combos[i].combo[0] == (int)actionNumber){                    
                    activeCombos.Add(i);
                }
            }
            //se busca la accion
            if(activeCombos.Count > 0){
                currentAction = actions[Combos[activeCombos[0]].combo[comboIndex]];
            //se pone play a la accion
                currentAction.StartAction(activeCombos[0] + comboIndex * 0.1f);
                AudioManager.Instance.PlayerAttack();
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
                if(currentAction.Fdata.State == ActionState.activeFrames){
                    //coincide la action con la del comboindex
                    for (int i = 0; i < activeCombos.Count; i++){
                        if((int)actionNumber == Combos[activeCombos[0]].combo[comboIndex]){
                            currentAction.StopAction();
                            currentAction = actions[Combos[activeCombos[0]].combo[comboIndex]];
                            found = true;
                            break;
                        }
                    }
                    if(found)
                        //se pone play a esa accion.
                        currentAction.StartAction(activeCombos[0] + comboIndex * 0.1f);
                        AudioManager.Instance.PlayerAttack();
                    //se quitan los que no coinciden.
                    for (int i = 0; i < activeCombos.Count; i++){
                        if(comboIndex == Combos[activeCombos[i]].combo.Count ||
                            Combos[activeCombos[i]].combo[comboIndex] != (int)actionNumber){               
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
}

    /* 
    
    

    private void Update() {
        if(activeCombos.Count > 0 && 
            comboIndex < comboMatrix.GetLength(1)){
            ManageCombo();
        }else{
            activeCombos.Clear();
            currentAction = null;
            comboIndex = 0;
        }
    }

    public void ManageCombo(){
        if(currentAction == null){
            currentAction = actions[comboMatrix[activeCombos[0],0]];
        }else{
            Debug.Log(comboIndex + "    " + currentAction.State);
            if(!currentAction.isActive){
                currentAction.StartAction();
            }else{
                if(currentAction.CanChain()){
                    currentAction = actions[comboMatrix[activeCombos[0],comboIndex]];
                    currentAction.ResetAction();
                    comboIndex++;
                }
                else{
                    currentAction.StopAction();
                    activeCombos.Clear();
                    currentAction = null;
                    comboIndex = 0;
                }
            }
        }
    }

    public void ManageAction(Actions actionNumber){
        if(currentAction == null){
            for (int i = 0; i < comboMatrix.GetLength(0); i++){
                if(comboMatrix[i,0] == (int)actionNumber){                    
                    activeCombos.Add(i);
                }
            }
        }else{
            for (int i = 0; i < comboMatrix.GetLength(0); i++){
                if(comboMatrix[i,comboIndex] != (int)actionNumber &&
                    currentAction.State == ActionState.activeFrames){                   
                    activeCombos.RemoveAt(i);
                }
            }
        }
    
    
    
    
    
    
    
    public void ManageAction(Actions actionNumber){
            if(activeCombos.Count > 0){
                for (int i = 0; i < comboMatrix.GetLength(0); i++){
                    if(comboMatrix[i,comboIndex] != (int)actionNumber &&
                        currentAction.State == ActionState.activeFrames){                   
                        activeCombos.RemoveAt(i);
                    }
                }
            }else{
                for (int i = 0; i < comboMatrix.GetLength(0); i++){
                    if(comboMatrix[i,0] == (int)actionNumber){                    
                        activeCombos.Add(i);
                    }
                }
            }
        ManageCombo();
    }

    private void Update() {
        if(currentAction.isActive){
            ManageCombo();
        }
    }

    private void ManageCombo(){
        if(activeCombos.Count>0){
            if(currentAction.CanChain()){
                currentAction.StopAction();
            }
            else{
                currentAction.StartAction();
            }
            currentAction = actions[comboMatrix[activeCombos[0], comboIndex]];
            comboIndex++;
        }
        if(comboIndex >= comboMatrix.GetLength(1)){
            activeCombos.Clear();
            comboIndex = 0;
        }
    }*/

      /*  
        if(activeCombos.Count>0){
            if(currentAction.CanChain()){
                currentAction.StopAction();
            }
            else{
                currentAction.StartAction();
            }
            currentAction = actions[comboMatrix[activeCombos[0], comboIndex]];
            comboIndex++;
        }
        if(comboIndex >= comboMatrix.GetLength(1)){
            activeCombos.Clear();
            comboIndex = 0;
        }
      
      
      
      
      
      
      if(activeCombos.Count > 0 && currentAction.State != ActionState.exitFrames){ //&& currentAction.CanChain()){
           Debug.Log(comboIndex);
            if(activeCombos[0] < actions.Length || currentAction.CanChain()){
                currentAction = actions[comboMatrix[activeCombos[0], comboIndex]];
                currentAction.StartAction();
                if(comboIndex+1 == comboMatrix.GetLength(1)){
                    activeCombos.Clear();
                    comboIndex = 0;
                }else{
                    comboIndex ++;
                }
            }
        }else{
            activeCombos.Clear();
            comboIndex = 0;
            currentAction = actions[0];
            Debug.Log("asd");
        }*/