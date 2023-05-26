using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoviment : MonoBehaviour
{

    public Vector2 car;
    public float carSpeed;

    [SerializeField]
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CarMove();
    }

    public void CarMove()
    {
        Vector2 moviment = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        car = moviment.normalized * carSpeed;
        rb.MovePosition(rb.position + car * Time.deltaTime);
    }
}
