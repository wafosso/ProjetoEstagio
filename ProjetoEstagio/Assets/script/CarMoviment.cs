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
    private bool stageUnlocked = false;
    private bool enterStageMap = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CarMove();
        EnterStage();
    }

    public void CarMove()
    {
        Vector2 moviment = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        car = moviment.normalized * carSpeed;
        rb.MovePosition(rb.position + car * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Map")
        {
            Debug.Log("collided");
            enterStageMap = true;
        }
    }

    public void EnterStage()
    {
        if (enterStageMap && Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        /*
        for (int stage = 0; stage < stageSelect.Length; stage++)
        {
            bool isUnlocked = IsStageUnlocked(stage);
            stageSelect[stage].interactable = isUnlocked;
            stageSelect[stage].gameObject.SetActive(isUnlocked);

            if (isUnlocked)
            {

            }

        }
        */

        

    }

}
