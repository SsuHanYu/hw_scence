using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MH004Move : MonoBehaviour
{
    [SerializeField] float WalkSpeed = 3;
    [SerializeField] float RunSpeed = 6;
    [SerializeField] LayerMask GroundLayer;
    float ForwardDirection;
    float allowPlayerRotation = .1f;
    //------------思涵add----------------start
    float m_time;//目前時間
    float targettime;//stand動畫的冷卻時間
    //------------思涵add----------------end


    Camera cam;
    CharacterController Controller;
    MH004Anim anim;

    Vector3 DesiredMoveDirection;
    float DesiredRotationSpeed = .1f;

    private void Start()
    {
        cam = Camera.main;
        Controller = GetComponent<CharacterController>();
        anim = GetComponent<MH004Anim>();
        targettime = UnityEngine.Random.Range(10, 20); //------------思涵add----------------

    }

    void Update()
    {
        /*------------思涵mark----------------start
        if (Input.GetMouseButtonDown(0))
        {
            anim.RotateBehavior(true);
        }
        else if (Input.GetMouseButtonUp(0))
            anim.RotateBehavior(false);
        ------------思涵mark----------------end*/


        if(anim.canmove) //判斷是否可以移動 //------------思涵add------------------------
            MoveBehavior();
        //------------思涵edit----------------start
        if (Input.GetKey(anim.Returnkey()))//取得目前設定的按鍵，並且播放旋轉動畫，且從GetMouseButtonDown改GetKey，讓玩家撞到箱子後也能維持旋轉的狀態
        {
            m_time = 0;//目前時間歸零
            anim.stand = false;
            anim.RotateBehavior(true);
        }
         if (Input.GetKeyUp(anim.Returnkey()))//結束旋轉動畫
        {
            anim.RotateBehavior(false);
            anim.canmove = true;
        }
        //------------思涵edit----------------end
    }

    void MoveBehavior()
    {
        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");
        // 計算 InputX + InputZ 的合力
        ForwardDirection = new Vector2(InputX, InputZ).sqrMagnitude;

        anim.SetSpeed(ForwardDirection);
        //------------思涵add--------------------------------------------------------------------------start
        anim.Anim.SetBool("stand",anim.stand);
        if (ForwardDirection == 0)//如果目前是待機時
        {
            m_time +=Time.deltaTime;
            if (m_time >= targettime)//如果目前時間大於等於stand的冷卻時間時
            {
                m_time = 0;//目前時間歸零
                targettime = UnityEngine.Random.Range(10,20);//重新設定冷卻時間
                anim.StandBehavior();
            }
        }
        else
        {          
            m_time = 0;//目前時間歸零
            anim.stand = false;
        }

        //------------思涵add--------------------------------------------------------------------------end
        if (ForwardDirection > allowPlayerRotation)
        {
            Vector3 camForward = cam.transform.forward;
            Vector3 camRight = cam.transform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            DesiredMoveDirection = camForward * InputZ + camRight * InputX;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DesiredMoveDirection), DesiredRotationSpeed);

            float Speed = SpeedJudgment();

            if (Physics.Raycast(transform.position, Vector3.down, .1f, GroundLayer))
                DesiredMoveDirection.y = 0;
            else
                DesiredMoveDirection.y = -2;
            Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

            Controller.Move(DesiredMoveDirection * Time.deltaTime * Speed);
        }
    }

    // 速度判斷
    float SpeedJudgment()
    {
        float m_Speed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
            m_Speed = RunSpeed;
        else
            m_Speed = WalkSpeed;

        return m_Speed;
    }

}
