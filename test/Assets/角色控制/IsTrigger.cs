using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTrigger : MonoBehaviour
{
    // 箱子鋼體
    public Rigidbody CubeRig;

    private void OnTriggerEnter(Collider other)
    {
        // 如果進入的是玩家
        if (other.tag == "Player")
            //就讓箱子掉落
            CubeRig.useGravity = true;
    }
}
