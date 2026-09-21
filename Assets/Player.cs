using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;

public class Player : MonoBehaviour
{

    
    public float moveSpeed = 5f;
    Rigidbody2D rb;

    public GameObject End;
    public GameObject Key;
    public GameObject Door;
    bool hasKey = false;
    bool nearDoor=false;
    bool IsOpen=false;
    public SpriteRenderer DoorSprite;
    public Collider2D DoorCollider;
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 moveDir=new Vector2(h,v);

        if(moveDir.magnitude >1)
        {
            moveDir.Normalize();
            
        }
        rb.velocity = moveDir * moveSpeed;

    }

    void Update()
    {
        if(nearDoor &&!hasKey &&Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("需要钥匙");
        }



        if (nearDoor && hasKey && Input.GetKeyDown(KeyCode.E))
        {
            if (IsOpen == false)
            {
                OpenDoor();
                Debug.Log("开门");
            }

            else
            {
                CloseDoor();
                Debug.Log("关门");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }



    }
    void OpenDoor()
    {   if (DoorCollider == null || DoorSprite == null)
         return; 
        IsOpen=true;
        DoorCollider.enabled = false;
        DoorSprite .enabled = false;
        
    }

    void CloseDoor()
    {
        if (DoorCollider == null || DoorSprite == null)
         return;
        IsOpen = false;
        DoorCollider.enabled = true;
        DoorSprite.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            Debug.Log("拾取钥匙");
        }

        if(other.CompareTag("End"))
        {
            Debug.Log("你过关,按R可重新开始");
            Destroy (other.gameObject);
            
        }
        if (other.CompareTag("Door"))
        {
            nearDoor = true;
            Debug.Log("按E开门");
            

        }



    }
           private void OnTriggerExit2D(Collider2D other)
            {
                if (other.CompareTag("Door"))
                {
                    nearDoor = false;
                }
            }
        }





    

