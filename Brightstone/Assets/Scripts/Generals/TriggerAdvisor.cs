using UnityEngine;
using UnityEngine.Events;

public class TriggerAdvisor : MonoBehaviour{
    [HideInInspector][SerializeField] UnityEvent onTrigger;

    private void OnTriggerEnter2D(Collider2D other){
        OnTrigger.Invoke();
    }

    public UnityEvent OnTrigger
    {
        get { return onTrigger; }
    }
}
