using UnityEngine;
using UnityEngine.UI;

public class Step0Explanation : Step{
    [SerializeField] private float timeToStart = 1.0f;
    [SerializeField] private string[] initialTexts;

    private int textIndex = 0;
    private bool init = false;

    public override void StepInitialize(){
        GameManager.Instance.DisablePlayer();

        Invoke("StartTuto", timeToStart);
    }

    public override void StepFinished(){
        
    }

    public override void StepUpdate(){
        if (!init) return;
        
        if (textIndex < initialTexts.Length){
            if (Input.GetButtonDown("Fire1")){
                textIndex++;
                if (textIndex < initialTexts.Length){
                    TextGenerator.Instance.Show(initialTexts[textIndex]);
                }
                else{
                    TextGenerator.Instance.Hide();
                }
            }
            return;
        }

        finished = true;
    }

    private void StartTuto(){
        init = true;
        TextGenerator.Instance.Show(initialTexts[textIndex]);

        ActiveRoom aR = GameManager.Instance.activeRoom;
        aR.GetNodeExits().CloseDoors();
    }
}
