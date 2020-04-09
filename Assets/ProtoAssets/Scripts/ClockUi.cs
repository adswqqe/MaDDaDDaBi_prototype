using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUi : MonoBehaviour
{
    private float day;
    private const float REAL_SECONDS_PER_INGAME_DAY = 60f;
    public Text timeText;

    private bool timestop;

    string hoursString;

    string minutesString;

    float dayNormalized;

    float hoursPerDay = 24f;

    float minutesPerHour = 60f;

    public GameObject 상점오픈;

    public GameObject 상점닫기;

    public GameObject 수입보고서;

    public GameObject 지나가는NPC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void 상점활성화()
    {
        timestop = false;
        상점오픈.SetActive(false);
        지나가는NPC.SetActive(true);
    }
    public void 상점비활성화()
    {
        상점오픈.SetActive(false);
    }
    public void 수입보고서비활성화()
    {
        수입보고서.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timestop)
        {
            day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

            dayNormalized = day % 1f;

            hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
             minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

            if (hoursString == "19" && minutesString == "00")
            {
                Debug.Log("오후 7시");

                상점닫기.SetActive(true);
            }

            if (hoursString == "21" && minutesString == "00")
            {
                Debug.Log("오후 9시");

                수입보고서.SetActive(true);

                지나가는NPC.SetActive(false);

                상점닫기.SetActive(false);
            }

            if (hoursString == "07" && minutesString == "00")
            {
                Debug.Log("오전 7시");
                timestop = true;

                상점오픈.SetActive(true);
            }
        }
        timeText.text = hoursString + ":" + minutesString;

    }
}
