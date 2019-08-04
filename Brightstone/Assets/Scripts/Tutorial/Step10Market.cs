using UnityEngine;

public class Step10Market : Step{
    [SerializeField] private string[] initialTexts;
    [SerializeField] private string[] middleTexts;
    [SerializeField] private string[] finalTexts;
    [SerializeField] private GameObject Market;

    private ActiveRoom aR;
    private int textIndex = 0;
    private bool lifeBought = false;
    private bool firstDialogueFinished = false;
    private bool secondDialogueFinished = false;
    private bool hitMarket = false;
    private ExperienceTest experienceTest;

    public override void StepInitialize(){
        TextGenerator.Instance.Show(initialTexts[textIndex]);
        GameManager.Instance.DisablePlayer();
        experienceTest = GameManager.Instance.tutorialMarket.GetComponent<ExperienceTest>();
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

        if (!hitMarket){
            if (experienceTest.IsHit()){
                hitMarket = true;
                TextGenerator.Instance.Show(middleTexts[textIndex]);
                GameManager.Instance.DisablePlayer();
            }
            return;
        }
        if (!secondDialogueFinished){
            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < middleTexts.Length){
                    TextGenerator.Instance.Show(middleTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    textIndex = 0;
                    secondDialogueFinished = true;
                    //GameManager.Instance.EnablePlayer();
                }
            }
            return;
        }

        if (!lifeBought){
            lifeBought = GameManager.Instance.tutorialMarketComplete;
            if(lifeBought){
                TextGenerator.Instance.Show(finalTexts[textIndex]);
                GameManager.Instance.DisablePlayer();
            }
            return;
        }

        if (textIndex < finalTexts.Length){
            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < finalTexts.Length){
                    TextGenerator.Instance.Show(finalTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                    Market.SetActive(false);
                }
            }
            return;
        }
        finished = true;
    }
}
