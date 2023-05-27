using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMoviment : MonoBehaviour
{

    public Button[] stageSelect;

    public Vector2 car;
    public float carSpeed;

    [SerializeField]
    private Rigidbody2D rb;
    private bool stageUnlocked;


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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadLevel();
        }

    }

    private void OnTriggerEnter2D(Collider2D collisionMap)
    {
        if (collisionMap.tag == "Map" && Input.GetKeyDown(KeyCode.Return))
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        for (int stage = 0; stage < stageSelect.Length; stage++)
        {
            if (stage >= 0)
            {
                stageUnlocked = true;
                SceneManager.LoadScene(stage);

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
            else if (!stageUnlocked)
            {
                Debug.Log($"need to unlock first");
            }
        }
    }

}
