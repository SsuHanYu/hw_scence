using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MH004Anim : MonoBehaviour
{
    [Header("撞暈時間")] [SerializeField] float StunnedTime = 1;
    [Header("旋轉時間")] [SerializeField] float RotateTime = 0.35f;//------------思涵add----------------
    [Header("旋轉碰撞半徑")] [SerializeField] float RotateRadius = 1.5f;
    [Header("旋轉碰撞圖層")] [SerializeField] LayerMask RotateLayer;
    [Header("旋轉的力道")] [SerializeField] float RotateForce = 500;
    public enum m_Key {Q,E,R,F,V,Alpha1,Alpha2,Alpha3,Mouse0,Mouse1 }//要設定的旋轉按鈕//------------思涵add----------------
    [Header("旋轉按鍵")] public m_Key Key ;//------------思涵add----------------
    public Animator Anim;
    public bool stand;//判斷是否在stand狀態//------------思涵add----------------
    public bool canmove;//------------思涵add----------------

    private void Start()
    {
        // Anim 得到動畫元件
        Anim = GetComponent<Animator>();
        stand = false;//------------思涵add----------------
        canmove=true;//------------思涵add----------------
    }

    public void SetSpeed(float Speed)
    {
        // 如果沒有按下Shift鍵速度為走路速度
        Speed = Mathf.Clamp(Speed, 0, .5f);

        // 如果按下Shift鍵 速度改為 跑步速度
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            Speed += .5f;

        // 執行跑步或走路動畫
        Anim.SetFloat("Speed", Speed);

    }

    // 撞暈行為
    public void StunnedBehavior()
    {
        // 開啟暈眩動畫
        Anim.SetTrigger("Stun");
        //------------思涵add---------------------------------------------------------------------start
        //結束旋轉動畫
        RotateBehavior(false);
        canmove = true;
    }   
    public void EndStanddBehavior()//stand動畫結束  要增加此事件(自動結束閒置)
    {
        // 結束閒置
        Anim.SetTrigger("End Stand");
        stand = false;
    }
    public void StandBehavior()
    {
        // 開啟閒置動畫
        Anim.SetTrigger("Stand");
        stand = true;
    }
    public  KeyCode Returnkey()
    {
        switch (Key)//判斷目前設定的按鈕是哪一顆，並回傳該按鈕
        {
         case m_Key.Q:
                return KeyCode.Q;
         case m_Key.E:
                return KeyCode.E;
         case m_Key.R:
                return KeyCode.R;
         case m_Key.F:
                return KeyCode.F;
         case m_Key.V:
                return KeyCode.V;
         case m_Key.Mouse0:
                return KeyCode.Mouse0;
         case m_Key.Mouse1:
                return KeyCode.Mouse1;
         case m_Key.Alpha1:
                return KeyCode.Alpha1;
         case m_Key.Alpha2:
                return KeyCode.Alpha2;
         case m_Key.Alpha3:
                return KeyCode.Alpha3;
         default:
                return KeyCode.None;
        }
    }
    public void RotateEvent2()
    {
       
        StartCoroutine(Rotatedelay(RotateTime));
    }
    IEnumerator Rotatedelay(float time)
    {
        canmove = false;
        // 等待 time秒 後執行下面
        yield return new WaitWhile(() => { return Anim.GetCurrentAnimatorStateInfo(0).IsName("Rotate"); });
        canmove = true;

    }
    //------------思涵add----------------------------------------------------------------------end

    // 暈眩事件
    public void StunnedEvent()
    {
        stand = false;//閒置時被敲到一樣結束閒置//------------思涵add----------------
        canmove = false;//------------思涵add----------------
        StartCoroutine(Recoverydelay(StunnedTime));
    }

    IEnumerator Recoverydelay(float time)
    {
        // 得到 HM004Move腳本
        MH004Move MoveElement = GetComponent<MH004Move>();
        // 關閉 HM004Move腳本
        MoveElement.enabled = false;
        // 關閉 HM004Anim腳本
        enabled = false;

        // 等待 time秒 後執行下面
        yield return new WaitForSeconds(time);
        canmove = true;//------------思涵add----------------
        // 開啟 HM004Move腳本
        MoveElement.enabled = true;
        // 開啟 HM004Anim腳本
        enabled = true;
    }

    // 旋轉動畫
    public void RotateBehavior(bool IsRotate)
    {
        // 如果IsRotate是true 開啟旋轉  不是時候 關閉旋轉
        Anim.SetBool("Rotate", IsRotate);
    }
    // 旋轉功能
    public void RotateEvent()
    {
        stand = false;//旋轉時一樣結束閒置  //------------思涵add--------------------------
        canmove = true;                     //------------思涵add--------------------------
        // 得到角色附近是RotateLayer圖層的碰撞器
        Collider[] collision = Physics.OverlapSphere(transform.position, RotateRadius, RotateLayer);

        // 跑過每個附近 RotateLayer圖層的碰撞器
        foreach (Collider i in collision)
        {
            // 得到RotateLayer圖層的碰撞器 的鋼體
            Rigidbody rb = i.GetComponent<Rigidbody>();
            // 如果他有剛體就執行碰撞
            if(rb != null)
            {
                rb.AddExplosionForce(RotateForce, transform.position, RotateRadius);
                var source = GetComponent<CinemachineImpulseSource>();
                source.GenerateImpulse(new Vector3(0.8f, 0f, 0.5f));
            }
        }
    }

    // 畫線顯示自訂碰撞範圍黃色參考輔助線
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, RotateRadius);
    }
}
