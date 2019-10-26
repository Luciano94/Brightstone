using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
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
    public GameObject[] dashChargesHud;

    [Header("Dash Particles")]
    public TrailRenderer dashParticles;

    private Rigidbody2D playerRB;

    private Vector2 mov;
    private bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        dashParticles.Clear();
        dashParticles.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DashCharges();
        HandlerDashUI();

        if (!isDashing)
        {
            mov.x = Input.GetAxis("Horizontal");
            mov.y = Input.GetAxis("Vertical");
            mov = mov.normalized;
            if (Input.GetButtonDown("Dash") && CanDash())
            {
                isDashing = true;
                dashParticles.Clear();
                dashParticles.enabled = true;
                actualDashCharges--;
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                isDashing = false;
                dashTime = startDashTime;
                playerRB.velocity = Vector2.zero;
                dashParticles.enabled = false;
                dashParticles.Clear();

            }
            else
            {
                dashTime -= Time.deltaTime;
                playerRB.velocity = dashSpeed * mov;
            }
        }
    }

    private void DashCharges()
    {
        if(actualDashCharges < dashChrages)
        {
            if(actualTimeBetweenCharges < timeBetweenCharges)
            {
                actualTimeBetweenCharges += Time.deltaTime;
            }
            else
            {
                actualTimeBetweenCharges = 0.0f;
                actualDashCharges++;
            }
        }
    }

    private void HandlerDashUI()
    {
        switch (actualDashCharges)
        {
            case 0:
                dashChargesHud[0].SetActive(false);
                dashChargesHud[1].SetActive(false);
                break;
            case 1:
                dashChargesHud[0].SetActive(true);
                dashChargesHud[1].SetActive(false);
                break;
            case 2:
                dashChargesHud[0].SetActive(true);
                dashChargesHud[1].SetActive(true);
                break;
            default:
                break;
        }
    }

    private bool CanDash()
    {
        return (actualDashCharges > 0 && mov != Vector2.zero);
    }
}
