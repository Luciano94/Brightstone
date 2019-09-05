using UnityEngine;

public class Arrow : MonoBehaviour{
    [SerializeField] private float speed;
    private Vector3 dir;

    private void Update(){
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void SetValues(EnemyStats enemyStats, Vector3 dir){
        GetComponentInChildren<ArrowTrigger>().enemyStats = enemyStats;
        this.dir = dir;
    }
}
