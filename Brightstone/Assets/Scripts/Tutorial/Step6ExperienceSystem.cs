using UnityEngine;

public class Step6ExperienceSystem : Step{
    [SerializeField] private GameObject experience;
    [SerializeField] private GameObject expArrow; 
    [SerializeField] private string[] initialTexts;

    private int textIndex = 0;

    public override void StepInitialize(){
        experience.SetActive(true);
        expArrow.SetActive(true);
        TextGenerator.Instance.Appear();
        TextGenerator.Instance.Show(initialTexts[textIndex]);
        GameManager.Instance.DisablePlayer();
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (textIndex < initialTexts.Length){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                    expArrow.SetActive(false);
                }
            }
            return;
        }
        
        finished = true;
    }
}
