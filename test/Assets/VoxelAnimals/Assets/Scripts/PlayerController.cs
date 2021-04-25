using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 3;
    public float runSpeed = 6;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Animator anim;
    Rigidbody rb;
    public GameObject aim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ControllPlayer();
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.LookAt(aim.transform);
        }
    }

    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

       /* Vector3 camera = Camera.main.WorldToScreenPoint(transform.position);// 相機是世界的，世界到螢幕
        Vector3 pos = new Vector3(Input.mousePosition.x*50, Input.mousePosition.y*50, camera.z);
        aim.transform.position = Camera.main.ScreenToWorldPoint(pos);*/
        if (movement != Vector3.zero)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.05f);
            if (Input.GetKey(KeyCode.RightArrow))
            { 
                aim.transform.Translate(Vector3.right * 250* Time.deltaTime);

                if (aim.transform.localPosition.x >= 360)
                {
                    aim.transform.localPosition = new Vector3(360, aim.transform.localPosition.y, 0);
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            { 
                aim.transform.Translate(Vector3.left * 250* Time.deltaTime);
                if (aim.transform.localPosition.x <= -360)
                {
                    aim.transform.localPosition = new Vector3(-360, aim.transform.localPosition.y, 0);
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                aim.transform.Translate(Vector3.up * 250 * Time.deltaTime);
                if (aim.transform.localPosition.y >= 180)
                {
                    aim.transform.localPosition = new Vector3(aim.transform.localPosition.x, 180, 0);
                }

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                aim.transform.Translate(Vector3.down * 250 * Time.deltaTime);
                if (aim.transform.localPosition.y <= 0)
                {
                    aim.transform.localPosition = new Vector3(aim.transform.localPosition.x, 0, 0);
                }

            }

            if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
            anim.SetInteger("Walk", 1);
        }
        else {
            anim.SetInteger("Walk", 0);
        }

        // transform.Translate(movement * movementSpeed * Time.deltaTime/*, Space.World*/);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            /*transform.Translate(Vector3.forward * moveVertical * runSpeed * Time.deltaTime);//W S 上 下
            transform.Translate(Vector3.right * moveHorizontal * runSpeed * Time.deltaTime);//A D 左右*/
            playmove(runSpeed);
        }
        else
        {
            /* transform.Translate(Vector3.forward * moveVertical * movementSpeed * Time.deltaTime);//W S 上 下
             transform.Translate(Vector3.right * moveHorizontal * movementSpeed * Time.deltaTime);//A D 左右*/
            playmove(movementSpeed);
        }
        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
                rb.AddForce(0, jumpForce, 0);
                canJump = Time.time + timeBeforeNextJump;
                anim.SetTrigger("jump");
        }
    }
    public void playmove(float mspeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * mspeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back* mspeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * mspeed * Time.deltaTime);
            transform.Rotate(0, -1.5F, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right* mspeed * Time.deltaTime);
              transform.Rotate(0, 1.5F, 0);
        }
    }
}