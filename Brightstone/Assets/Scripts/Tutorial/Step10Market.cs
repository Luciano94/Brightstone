using UnityEngine;

public class Step10Market : Step{
    [SerializeField] private string[] initialTexts;
    [SerializeField] private string[] middleTexts;
    [SerializeField] private string[] finalTexts;
    [SerializeField] private GameObject Market;
    [SerializeField] private GameObject nodeExitRight;
    [SerializeField] private GameObject lifeArrow;
    [SerializeField] private GameObject damageArrow;
    [SerializeField] private GameObject lifeArrowRightPanel;
    [SerializeField] private GameObject damageArrowRightPanel;
    [SerializeField] private GameObject buyArrow;
    [SerializeField] private GameObject confirmArrow;

    private ActiveRoom aR;
    private int textIndex = 0;
    private bool lifeBought = false;
    private bool firstDialogueFinished = false;
    private bool secondDialogueFinished = false;
    private bool hitMarket = false;
    private ExperienceTest experienceTest;

    public override void StepInitialize(){
        experienceTest = GameManager.Instance.tutorialMarket.GetComponent<ExperienceTest>();
        GameManager.Instance.DisablePlayer();
        TextGenerator.Instance.Appear();
        TextGenerator.Instance.Show(initialTexts[textIndex]);
    }

    public override void StepFinished(){
    }

    public override void StepUpdate(){
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

        if (!hitMarket){
            if (experienceTest.IsHit()){
                hitMarket = true;
                TextGenerator.Instance.Appear();
                TextGenerator.Instance.Show(middleTexts[textIndex]);
                GameManager.Instance.DisablePlayer();
            }
            return;
        }
        if (!secondDialogueFinished){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;

                if (textIndex == 1){
                    lifeArrow.SetActive(true);
                    damageArrow.SetActive(true);
                }
                if (textIndex == 4){
                    lifeArrow.SetActive(false);
                    damageArrow.SetActive(false);
                    lifeArrowRightPanel.SetActive(true);
                    damageArrowRightPanel.SetActive(true);
                }
                if (textIndex == 5){
                    lifeArrowRightPanel.SetActive(false);
                    damageArrowRightPanel.SetActive(false);
                }

                if (textIndex < middleTexts.Length){
                    TextGenerator.Instance.Show(middleTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    textIndex = 0;
                    secondDialogueFinished = true;
                    buyArrow.SetActive(true);
                    confirmArrow.SetActive(true);
                    //GameManager.Instance.EnablePlayer();
                }
            }
            return;
        }

        if (!lifeBought){
            lifeBought = GameManager.Instance.tutorialMarketComplete;
            if(lifeBought){
                buyArrow.SetActive(false);
                confirmArrow.SetActive(false);
                TextGenerator.Instance.Appear();
                TextGenerator.Instance.Show(finalTexts[textIndex]);
                GameManager.Instance.DisablePlayer();
            }
            return;
        }

        if (textIndex < finalTexts.Length){
            if (InputManager.Instance.GetPassButton()){
                textIndex++;
                if (textIndex < finalTexts.Length){
                    TextGenerator.Instance.Show(finalTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.EnablePlayer();
                    Market.SetActive(false);
                    nodeExitRight.SetActive(false);
                }
            }
            return;
        }
        finished = true;
    }
}
