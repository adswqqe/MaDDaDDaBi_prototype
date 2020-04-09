using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 골드시스템 : MonoBehaviour
{
    public int gold;

    public Text 골드;

    // Start is called before the first frame update
    void Start()
    {
        골드.text = gold.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void goldadd (int add)
    {
        gold += add;
        골드.text = gold.ToString("0");
    }
    public void goldremove(int remove)
    {
        gold -= remove;
        골드.text = gold.ToString("0");
    }

}
