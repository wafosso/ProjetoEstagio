using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMoviment : MonoBehaviour
{
    public int[] stageIndex;

    public Vector2 car;
    public float carSpeed;
    public bool stage1 = false, stage2 = false;
    public bool stageUnlock = false;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
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
        float horizontalMoviment = Input.GetAxis("Horizontal");
        float verticalMoviment = Input.GetAxis("Vertical");
        Vector2 moviment = new Vector2(horizontalMoviment, verticalMoviment);
        rb.velocity = moviment * carSpeed;

        if (moviment != Vector2.zero)
        {
            float angle = Mathf.Atan2(horizontalMoviment, verticalMoviment) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Debug.Log("collided stage 1");
            enterStageMap = true;
            stage1 = true;
        }

        if (collision.gameObject.CompareTag("Map2"))
        {
            Debug.Log("collided stage 2");
            enterStageMap = true;
            stage2 = false;
            
            if(stageUnlock == false)
            {
                Debug.Log("Unlock this stage");
            }
            else if (stageUnlock == true)
            {
                stage2 = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enterStageMap = false;
    }


    public void EnterStage()
    {
        if (enterStageMap && stage1 && Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel1();
        }

        else if (enterStageMap && stage2 && Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel2();
        }
    }

    public void LoadLevel1()
    {
        Debug.Log("enter");
        int levelnum1 = stageIndex[0];
        SceneManager.LoadScene(levelnum1);
    }

    public void LoadLevel2()
    {
        Debug.Log("enter2");
        int levelnum2 = stageIndex[1];
        SceneManager.LoadScene(levelnum2);
    }

}
