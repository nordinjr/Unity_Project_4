using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GameObject target;
    public float runSpeed;
    public float turnSpeed;

    private float horizontal;
    private float vertical;
    private Rigidbody2D body;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown("space"))
        {
            TakeDamage(20);
        }
    }

    private void FixedUpdate()
    {
        body.AddForce(transform.up * vertical * runSpeed);
        transform.Rotate(Vector3.back * horizontal * turnSpeed);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }
}
