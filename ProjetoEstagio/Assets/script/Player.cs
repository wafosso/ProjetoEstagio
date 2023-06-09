using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform holdPosition;
    public Transform deliveryPoint;
    public GameObject medico;
    private Rigidbody2D rb;

    public GameObject heldObject;

    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public KeyCode dashKey = KeyCode.Space;
    private bool isDashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Desativar rota��o autom�tica do Rigidbody2D
    }

    void Update()
    {
        // Movimenta��o do jogador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * moveSpeed;



        if (movement.x < 0)
        {
            Debug.Log("62626262626262626262");
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movement.x > 0)
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;


        if (Input.GetKeyDown(dashKey) && !isDashing)
        {
            Vector2 dashDirection = new Vector2(moveHorizontal, moveVertical).normalized;
            StartCoroutine(Dash(dashDirection));
        }

        // Intera��o com objetos

        if (heldObject == null)
        {
            // Verifica se h� algum objeto pr�ximo para pegar
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Pickupable"))
                {
                    // Pega o objeto
                    heldObject = collider.gameObject;
                    heldObject.transform.parent = holdPosition.transform;
                    heldObject.transform.position = holdPosition.position;

                    Debug.Log("Objeto pego: " + heldObject.name);


                    break;
                }
            }
        }
        else
        {
            // Entrega o objeto
            float deliveryDistance = Vector2.Distance(transform.position, deliveryPoint.position);

            if (deliveryDistance <= 2f)
            {
                heldObject.transform.position = new Vector3(8f,0,0);
                heldObject.transform.parent = null;
                heldObject = null;

                Debug.Log("Objeto entregue");
     
                heldObject.tag = "Delivered";
            }
        }
    }

    private IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + direction * dashDistance;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            rb.velocity = direction * (dashDistance / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    public class Ingredient : MonoBehaviour
    {
        public int ingredientCount;
    }
}