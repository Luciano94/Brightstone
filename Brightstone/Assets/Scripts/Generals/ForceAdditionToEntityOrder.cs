using UnityEngine;

public class ForceAdditionToEntityOrder : MonoBehaviour{
    [SerializeField] private SpriteRenderer[] sprites;
    
    private void OnTriggerEnter2D(Collider2D other){
        foreach (SpriteRenderer sR in sprites)
            EntityOrderManager.Instance.AddEntity(sR);
    }

    private void OnTriggerExit2D(Collider2D other){
        foreach (SpriteRenderer sR in sprites)
            EntityOrderManager.Instance.RemoveEntity(sR);
    }
}