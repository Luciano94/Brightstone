using UnityEngine;
using UnityEngine.UI;

public class Step0Explanation : Step{
    [SerializeField] private float timeToStart = 1.0f;
    [SerializeField] private float timeToFinish = 2.0f;
    [SerializeField] private string[] initialTexts;

    private int textIndex = 0;
    private bool init = false;

    public override void StepInitialize(){
        Invoke("StartTuto", timeToStart);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (!init) return;
        
        if (textIndex < initialTexts.Length){
            GameManager.Instance.playerMovement.enabled = false;
            GameManager.Instance.playerCombat.enabled = false;
            GameManager.Instance.playerAnimations.enabled = false;

            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                    GameManager.Instance.playerAnimations.enabled = true;
                    GameManager.Instance.playerCombat.enabled = true;
                    GameManager.Instance.playerMovement.enabled = true;
                }
            }

            return;
        }

        timeToFinish -= Time.deltaTime;

        if (timeToFinish <= 0.0f)
            finished = true;
    }

    private void StartTuto(){
        init = true;
        TextGenerator.Instance.Show(initialTexts[textIndex]);
    }
}
