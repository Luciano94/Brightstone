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

    private void Update() {
        Movement();
        Rotation();
    }

    private void Rotation(){
        rotH = Input.GetAxis("Horizontal");
        rotV = Input.GetAxis("Vertical") * -1;
       
        if(rotH != 0 || rotV != 0){
            var angle = Mathf.Atan2(rotH, rotV) * Mathf.Rad2Deg;
            angle = Mathf.Lerp(player.transform.eulerAngles.z , angle, rotSpeed * Time.time);
            sword.transform.rotation =  Quaternion.Euler(0,0,angle);
        }
    }

    private void Movement(){
        horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(horizontal,vertical,0);
    }
}
