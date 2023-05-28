using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform holdPosition;
    public Transform deliveryPoint;

    private Rigidbody2D rb;

    public GameObject heldObject;

    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public KeyCode dashKey = KeyCode.Space;
    private bool isDashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Desativar rotação automática do Rigidbody2D
    }

    void Update()
    {
        // Movimentação do jogador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * moveSpeed;

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveVertical, moveHorizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetKeyDown(dashKey) && !isDashing)
        {
            Vector2 dashDirection = new Vector2(moveHorizontal, moveVertical).normalized;
            StartCoroutine(Dash(dashDirection));
        }

        // Interação com objetos

        if (heldObject == null)
        {
            // Verifica se há algum objeto próximo para pegar
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
                heldObject.transform.position = new Vector3(11.5f,0,0);
                heldObject.transform.parent = null;
                heldObject = null;

                Debug.Log("Objeto entregue");
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