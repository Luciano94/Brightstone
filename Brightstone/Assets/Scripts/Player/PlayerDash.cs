using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
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
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDashing = true;
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
}
