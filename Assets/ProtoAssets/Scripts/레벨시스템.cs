using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 레벨시스템 : MonoBehaviour
{
    int level = 1;

    public int exp, Reputation;

    public int[] nextexp;

    public Text 레벨, 경험치, 평판;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        레벨.text = "레벨 : " + level.ToString("0"); ;

        경험치.text = "경험치 : " + exp.ToString("0") + "/" + nextexp[level]; ;

        평판.text = "평판 : " + Reputation.ToString("0") + "점";
    }

     public void 경험치획득(int add)
    {
        exp += add;


        while (exp >= nextexp[level])
            level++;

    }
    public void 평판획득(int add)
    {
        Reputation += add;
    }
}
