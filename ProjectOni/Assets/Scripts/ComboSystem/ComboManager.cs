using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class String{
    public List<int> actions;
}
[System.Serializable]
public class ComboString{
    public String[] Combo;
}

public class ComboManager : MonoBehaviour{
    private bool[] isComboActive;
    [SerializeField]private ComboString combos;
    [SerializeField]private Accion[] actions;
    private List<int> activesCombos;

    private void Start() {
        activesCombos = new List<int>();
        isComboActive = new bool[combos.Combo.Length];
        for (int i = 0; i < isComboActive.Length; i++)
            isComboActive[i] = false;
    }


}
