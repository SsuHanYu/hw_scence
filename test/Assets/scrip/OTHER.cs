using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OTHER : MonoBehaviour
{
    public GameObject DIALOG;
    public int Dialog_now;
    public TextMeshProUGUI talk;
    public TextMeshProUGUI name;
    public RawImage me;
    public RawImage he;
    // Start is called before the first frame update
    void Start()
    {
        Dialog_now = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (DIALOG.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        switch (Dialog_now)
        {
            case 0:
                talk.SetText("歡迎來到這個世界，請旅行者盡情探險吧~");
                name.SetText("神劍");
                me.color = new Color(0.4f,0.4f,0.4f);
                he.color=new Color(1,1,1);
                break;
            case 1:
                talk.SetText("好喔......");
                name.SetText("兔子");
               he.color = new Color(0.4f, 0.4f, 0.4f);
                me.color = new Color(1,1,1);
                break;
        }
            
    }
    public void CLOSEDIALOG()
    {
        if (Dialog_now < 1)
        {
            Dialog_now +=1;
        }
        else
        {
            DIALOG.SetActive(false);
            Dialog_now = 0;
        }
     
    }
}
