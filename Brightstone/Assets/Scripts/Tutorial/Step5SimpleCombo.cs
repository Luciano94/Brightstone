using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step5SimpleCombo : Step{
    [SerializeField] private ComboManager comboManager;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private string[] initialTexts;
    [SerializeField] private string[] middleTexts;

    private int textIndex = 0;
    private bool enemyDeath = false;
    private bool firstDialogueFinished = false;

    public override void StepInitialize(){
        enemyStats.OnDeath.AddListener(EnemyDeath);

        List<int> simpleCombo = new List<int>();
        simpleCombo.Add(0);
        simpleCombo.Add(0);
        simpleCombo.Add(0);
        simpleCombo.Add(0);
        comboManager.ChangeCombo(0, simpleCombo);

        TextGenerator.Instance.Show(initialTexts[textIndex]);
        GameManager.Instance.DisablePlayer();
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (!firstDialogueFinished){
            if (Input.GetButtonDown("Fire1")){
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

        if (!enemyDeath){
            float hpDiff = enemyStats.MaxLife() - enemyStats.Life;
            if (comboManager.ActualComboIndex() == 0 && hpDiff != 0){
                enemyStats.Life = -hpDiff;
            }
            return;
        }

        if (textIndex < middleTexts.Length){
            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < middleTexts.Length){
                    TextGenerator.Instance.Show(middleTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                }
            }
            return;
        }

        finished = true;
    }

    private void EnemyDeath(){
        enemyDeath = true;
        TextGenerator.Instance.Show(middleTexts[textIndex]);
        GameManager.Instance.DisablePlayer();
    }
}