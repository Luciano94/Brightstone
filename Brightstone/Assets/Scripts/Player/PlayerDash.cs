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

    private Rigidbody2D playerRB;

    private Vector2 mov;
    private bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DashCharges();
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash())
            {
                isDashing = true;
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
            }
            else
            {
                mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
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

    private bool CanDash()
    {
        return actualDashCharges > 0;
    }
}
