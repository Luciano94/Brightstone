using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    [SerializeField]private float speed;
    [SerializeField]private float atckTime;
    [SerializeField]private GameObject weapon;
    [SerializeField]private GameObject player;

    private float actTime;
    private float horizontal;
    private float vertical;
    private float rotH;
    private float rotV;

    private void Awake() {
        actTime = atckTime;
    }

    private void Update() {
        Movement();
        Rotation();
        Actions();
    }

    private void Rotation(){
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical") * -1;

        var angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

        angle = Mathf.Lerp(player.transform.eulerAngles.z , angle, Time.time);
        player.transform.rotation =  Quaternion.Euler(0,0,angle);
    }
    private void Movement(){
        horizontal = Input.GetAxis("Horizontal")*speed*Time.deltaTime;
        vertical = Input.GetAxis("Vertical")*speed*Time.deltaTime;

        transform.Translate(horizontal,vertical,0);
    }

    private void Actions(){
        if(Input.GetButton("Fire1")){
            weapon.SetActive(true);
            actTime = 0.0f;
        }
        if(actTime > atckTime)
            weapon.SetActive(false);
        else
            actTime += Time.deltaTime; 
        
    }
}
