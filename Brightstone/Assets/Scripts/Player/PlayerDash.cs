using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour{
    [Header("Dash Charges")]
    public int dashChrages = 2;
    public int actualDashCharges = 2;
    public float timeBetweenCharges = 5.0f;
    public float actualTimeBetweenCharges = 0.0f;

    [Header("Dash Parametres")]
    public float dashSpeed;
    public float startDashTime;
    private float dashTime;

    [Header("Dash HUD")]
    public Image[] dashChargesHud;
    public Sprite dash;
    public Sprite dashEmpty;


    [Header("Dash Particles")]
    private Rigidbody2D playerRB;
    private PlayerCombat playerCombat;
    private Vector2 mov;
    private bool isDashing = false;
    [SerializeField] private GhostTrail ghost;

    void Start(){
        playerRB = GetComponent<Rigidbody2D>();
        playerCombat = GetComponent<PlayerCombat>();
        dashTime = startDashTime;
        //dashParticles.Clear();
        //dashParticles.transform.position = transform.position;
        //dashParticles.enabled = false;
    }

    void Update(){
        DashCharges();
        HandlerDashUI();

        if (!isDashing){
            mov.x = InputManager.Instance.GetHorizontalAxis();
            mov.y = InputManager.Instance.GetVerticalAxis();
            mov = mov.normalized;
            if (InputManager.Instance.GetActionDash() && CanDash() && !GameManager.Instance.pause){
                isDashing = true;
                ghost.ShowGhost();
                FilterManager.SetChromaticAberration(true);
                GameManager.Instance.ShakerController.Shake(1.5f, 1.5f, 0.1f, 0.2f);
                playerCombat.Dash();
                SoundManager.Instance.PlayerDash();
                actualDashCharges--;
            }
        }
        else{
            if(dashTime <= 0){
                isDashing = false;
                FilterManager.SetChromaticAberration(false);
                dashTime = startDashTime;
                playerRB.velocity = Vector2.zero;
            }
            else{
                dashTime -= Time.deltaTime;
                playerRB.velocity = dashSpeed * mov;
            }
        }
    }

    private void DashCharges(){
        if(actualDashCharges < dashChrages){
            if(actualTimeBetweenCharges < timeBetweenCharges){
                actualTimeBetweenCharges += Time.deltaTime;
            }
            else{
                actualTimeBetweenCharges = 0.0f;
                actualDashCharges++;
            }
        }
    }

    private void HandlerDashUI(){
        switch (actualDashCharges){
            case 0:
                dashChargesHud[0].sprite = dashEmpty;
                dashChargesHud[1].sprite = dashEmpty;
                break;
            case 1:
                dashChargesHud[0].sprite = dash;
                dashChargesHud[1].sprite = dashEmpty;
                break;
            case 2:
                dashChargesHud[0].sprite = dash;
                dashChargesHud[1].sprite = dash;
                break;
            default:
                break;
        }
    }

    private bool CanDash(){
        return (actualDashCharges > 0 && mov != Vector2.zero);
    }
}
