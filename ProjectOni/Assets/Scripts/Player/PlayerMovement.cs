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

    string[] joystickConnected;
    bool isConnected = false;

    private void DetectDivice() {
        if( Input.GetJoystickNames().Length > 0)
            isConnected = true;
    }

    private void Update() {
        DetectDivice();
        if(!GetComponent<PlayerCombat>().isAttack)
            Movement();
        Rotation();
    }

    private void Rotation(){
        if(isConnected){
            rotH = Input.GetAxis("Horizontal");
            rotV = Input.GetAxis("Vertical") * -1;
        }else{
            rot =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rot = rot.normalized;
            rotH = rot.x;
            rotV = rot.y * -1;
        }

        if(rotH != 0 || rotV != 0){
            var angle = Mathf.Atan2(rotH, rotV) * Mathf.Rad2Deg;
            angle = Mathf.Lerp(player.transform.eulerAngles.z , angle, rotSpeed * Time.time);
            sword.transform.rotation =  Quaternion.Euler(0,0,angle);
        }
    }

    private void Movement(){
        mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        mov *= (speed * Time.deltaTime);
        
        transform.Translate(mov);
    }
}
