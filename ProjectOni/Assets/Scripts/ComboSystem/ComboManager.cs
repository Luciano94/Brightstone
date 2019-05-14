﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComboManager : MonoBehaviour{
    private int[,] comboMatrix; //matriz con todos los combos
    private int comboIndex; //posicion del combo en ese momento
    private List<int> activeCombos;//array con los combos que estan ejecutandose
    private Action currentAction; //estado de la accion que se esta ejecutando en ese momento
    [SerializeField]private Action[] actions; //acciones posibles

    private void Awake() {
        comboMatrix = new int[,]{{0,0,0},{0,1,0},{1,1,-1},{1,0,1}};
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
            //inicializa el arreglo de combos;
            for (int i = 0; i < comboMatrix.GetLength(0); i++){
                if(comboMatrix[i,0] == (int)actionNumber){                    
                    activeCombos.Add(i);
                }
            }
            //se busca la accion
            if(activeCombos.Count > 0){
                currentAction = actions[comboMatrix[activeCombos[0],comboIndex]];
            //se pone play a la accion
                currentAction.StartAction();
            }
            //se inicializa el combo index
            comboIndex = 1;
        }else{
            if(comboIndex >= comboMatrix.GetLength(1)){
                comboIndex = 0;
                activeCombos.Clear();
            }else{
                //si esta en el tiempo de encadenar
                if(currentAction.Fdata.State == ActionState.activeFrames){
                    //coincide la action con la del comboindex
                    for (int i = 0; i < activeCombos.Count; i++){
                        if((int)actionNumber == comboMatrix[activeCombos[0],comboIndex]){
                            currentAction.StopAction();
                            currentAction = actions[comboMatrix[activeCombos[0],comboIndex]];
                            break;
                        }
                    }
                    //se pone play a esa accion.
                    currentAction.StartAction();
                    //se quitan los que no coinciden.
                    for (int i = 0; i < activeCombos.Count; i++){
                        if(comboMatrix[activeCombos[i],comboIndex] != (int)actionNumber){               
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