using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step4SimpleAttack : Step{
    [SerializeField] private ComboManager comboManager;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private float minDistance;
    [SerializeField] private string[] initialTexts;
    [SerializeField] private string[] middleTexts;

    private PlayerCombat playerCombat;
    private int textIndex = 0;

    private bool firstDialogueFinished = false;
    private bool usedSimpleAttack = false;
    private bool usedStrongAttack = false;

    public override void StepInitialize(){
        playerCombat = GameManager.Instance.playerCombat;
        playerCombat.enabled = false;
        enemyStats.OnHit.AddListener(EnemyHit);
        GameManager.Instance.DisablePlayer();
        TextGenerator.Instance.Appear();
        TextGenerator.Instance.Show(initialTexts[textIndex]);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        float hpDiff = enemyStats.MaxLife() - enemyStats.Life;
        if (comboManager.ActualComboIndex() == 0 && hpDiff != 0)
            enemyStats.Life = -hpDiff;

        if (!firstDialogueFinished){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                    textIndex = 0;
                    firstDialogueFinished = true;
                }
            }
            return;
        }

        if (!usedSimpleAttack) return;

        if (textIndex < middleTexts.Length){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;
                if (textIndex < middleTexts.Length){
                    TextGenerator.Instance.Show(middleTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();

                    List<int> combo = new List<int>();
                    combo.Add(1);
                    comboManager.AddCombo(combo);
                }
            }
            return;
        }

        if (!usedStrongAttack) return;

        finished = true;
    }

    private void EnemyHit(){
        if (enemyStats.LastDamageRecieved() == 15.0f && !usedSimpleAttack){
            usedSimpleAttack = true;
            TextGenerator.Instance.Appear();
            TextGenerator.Instance.Show(middleTexts[textIndex]);
            GameManager.Instance.DisablePlayer();
        }

        if (enemyStats.LastDamageRecieved() == 40.0f && !usedStrongAttack){
            usedStrongAttack = true;
        }
    }
}
