using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    [SerializeField]private float speed;
    [SerializeField]private float rotSpeed;
    [SerializeField]private GameObject player;
    [SerializeField]private GameObject sword;
    
    private float horizontal;
    private float vertical;
    private float rotH;
    private float rotV;
    private Vector2 rot;
    private Vector2 mov;
    private Vector3 diffFromEnemy;
    private string[] joystickConnected;
    private bool isConnected = false;

    private void DetectDivice(){
        if( Input.GetJoystickNames().Length > 0){
            if(Input.GetJoystickNames().Length == 1 && Input.GetJoystickNames()[0].Length > 10)
                isConnected = true;
            else
                isConnected = false;
        }
        else
            isConnected = false;
    }

    public bool IsConnected{
        get{return isConnected;}
    }

    private void Update() {
        DetectDivice();

        if(GetComponent<PlayerCombat>().IsHit){
            MoveByHit();
            return;
        }
        
        if(!GetComponent<PlayerCombat>().isAttack &&
            !GetComponent<PlayerCombat>().isParry){
            Movement();
            Rotation();
        }
    }

    public Vector3 GetRotation{
        get{return sword.transform.eulerAngles;}
    }

    private void Rotation(){
        if(isConnected){
            rotH = Input.GetAxis("Horizontal");
            rotV = Input.GetAxis("Vertical") * -1;
        }else{
            rot =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rot = rot.normalized;
            rot.y *= -1;
            rotH = rot.x;
            rotV = rot.y;
        }

        if(rotH != 0 || rotV != 0){
            var angle = Mathf.Atan2(rotH, rotV) * Mathf.Rad2Deg;
            angle = Mathf.Lerp(player.transform.eulerAngles.z , angle, rotSpeed * Time.time);
            sword.transform.rotation =  Quaternion.Euler(0,0, angle);
        }
    }

    private void Movement(){
        mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        mov *= (speed * Time.deltaTime);
        
        transform.Translate(mov);
    }

    private void MoveByHit(){
        transform.Translate(-diffFromEnemy.normalized * speed * 0.3f * Time.deltaTime);
    }

    public void SetEnemyPos(Vector3 enemyPos){
        diffFromEnemy = enemyPos - transform.position;
    }
}
