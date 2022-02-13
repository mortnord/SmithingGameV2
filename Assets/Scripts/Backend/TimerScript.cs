using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    public bool timer_Is_Running = false;
    public float minutes = 0;
    public float seconds = 0;
    public Text time_Text;
    public bool reset = false;
    public bool ekstra_mission_reset = false;
    public float reset_timer = 10;
    public float ekstra_mission_spawn = 30;

    // Start is called before the first frame update
    void Start()
    {

        timer_Is_Running = true; //Tiden ska begynne å gå
    }
    // Update is called once per frame
    void Update()
    {
        if (StaticData.time_Remaining > 0 && timer_Is_Running)
        {
            StaticData.time_Remaining -= Time.deltaTime; //Trekkes fra tiden tilsvarnede tiden brukt på en frame, gir en smooth 1 sekund per sekund. 
            calculate_time_left(StaticData.time_Remaining); //Beregne minutter og sekunder
        }
        else
        {
            StaticData.time_Remaining = 0; //Runde ned til null
            timer_Is_Running = false;
        }
        if (reset == false) //Minimumstiden mellom hver mission
        {
            reset_timer -= Time.deltaTime;
            if (reset_timer < 0)
            {
                reset = true;
                reset_timer = 10;
            }
        }
        if (ekstra_mission_reset == false) //Maksimumstiden mellom hver mission
        {
            ekstra_mission_spawn -= Time.deltaTime;
            if (ekstra_mission_spawn < 0)
            {
                reset = true;
                ekstra_mission_reset = true;
            }
        }
    }
    void calculate_time_left(float time_to_display)
    {
        minutes = Mathf.FloorToInt(time_to_display / 60);
        seconds = Mathf.FloorToInt(time_to_display % 60);
        time_Text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}