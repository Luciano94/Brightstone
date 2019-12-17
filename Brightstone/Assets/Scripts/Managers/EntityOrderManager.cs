using System.Collections.Generic;
using UnityEngine;

public class EntityOrderManager : MonoBehaviour{
    private static EntityOrderManager instance;

    public static EntityOrderManager Instance{
        get {
            instance = FindObjectOfType<EntityOrderManager>();
            if(instance == null){
                GameObject go = new GameObject("EntityOrderManager");
                instance = go.AddComponent<EntityOrderManager>();
            }
            return instance;
        }
    }

    List<SpriteRenderer> entities = new List<SpriteRenderer>();
    List<SpriteRenderer> deadEntities = new List<SpriteRenderer>();

    void Update(){
        if (entities.Count == 1) return;

        int index = 0;

        do
        {
            for (int j = index; j < entities.Count - 1; j++)
            {
                if (entities[j].transform.position.y < entities[j + 1].transform.position.y){
                    SpriteRenderer temp = entities[j];
                    entities[j] = entities[j + 1];
                    entities[j + 1] = temp;
                }
            }
        } while (++index < entities.Count);

        index = 1;

        foreach (SpriteRenderer entity in entities){
            entity.sortingOrder = index;
            index += 2;
        }
    }

    public void AddEntity(SpriteRenderer spriteRenderer){
        entities.Add(spriteRenderer);
    }

    public void RemoveEntity(SpriteRenderer spriteRenderer){
        entities.Remove(spriteRenderer);
    }

    public void OnEntityDeath(SpriteRenderer spriteRenderer){
        entities.Remove(spriteRenderer);

        deadEntities.Add(spriteRenderer);

        UpdateDeadEntities();
    }

    void UpdateDeadEntities(){
        if (deadEntities.Count == 1) return;

        int index = 0;

        do
        {
            for (int j = index; j < deadEntities.Count - 1; j++)
            {
                if (deadEntities[j].transform.position.y < deadEntities[j + 1].transform.position.y){
                    SpriteRenderer temp = deadEntities[j];
                    deadEntities[j] = deadEntities[j + 1];
                    deadEntities[j + 1] = temp;
                }
            }
        } while (++index < deadEntities.Count);

        index = 1;

        foreach (SpriteRenderer entity in deadEntities){
            entity.sortingOrder = index;
            index += 2;
        }
    }
}