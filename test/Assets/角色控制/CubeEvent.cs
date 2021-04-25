using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEvent : MonoBehaviour
{
    // 進入碰撞器執行
    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰到的是玩家
        if (collision.transform.tag == "Player")
            // 播放玩家暈眩動畫
            collision.transform.GetComponent<MH004Anim>().StunnedBehavior();
    }
}
