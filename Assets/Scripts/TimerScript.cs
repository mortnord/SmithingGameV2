using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float time_Remaining = 10;
    public bool timer_Is_Running = false;
    public float minutes = 0;
    public float seconds = 0;
    public Text time_Text;
    // Start is called before the first frame update
    void Start()
    {
        time_Remaining = 720; //Tid i sekunder per lvl / minecart noe. 
        timer_Is_Running = true; //Tiden ska begynne å gå

    }

    // Update is called once per frame
    void Update()
    {
        if (time_Remaining > 0 && timer_Is_Running)
        {
            time_Remaining -= Time.deltaTime; //Trekkes fra tiden tilsvarnede tiden brukt på en frame, gir en smooth 1 sekund per sekund. 
            calculate_time_left(time_Remaining); //Beregne minutter og sekunder. 

        }
        else
        {
            time_Remaining = 0; //Runde ned til null
            timer_Is_Running = false; 
        }

    }
    void calculate_time_left(float time_to_display)
    {
        minutes = Mathf.FloorToInt(time_Remaining / 60);
        seconds = Mathf.FloorToInt(time_Remaining % 60);
        time_Text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}