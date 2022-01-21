using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Export_Chute : MonoBehaviour
{
    Score Score_object;
    Mission_System mission_system_object;
    // Start is called before the first frame update
    public int transport_speed = 1;
    public List<GameObject> Stuff_to_transport = new List<GameObject>();
    void Start()
    {
        Score_object = Find_Components.find_score();
        mission_system_object = Find_Components.find_mission_system();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stuff_to_transport.Count > 0)
        {
            
            for (int i = 0; i < Stuff_to_transport.Count; i++) 
            {
                Stuff_to_transport[i].transform.position = new Vector3(Stuff_to_transport[i].transform.position.x + 0.05f * transport_speed * Time.deltaTime, Stuff_to_transport[i].transform.position.y);
                if (Stuff_to_transport[i].transform.position.x > 7.2f)
                {
                    mission_system_object.check_mission_success(Stuff_to_transport[i]);
                    Score_object.score += Stuff_to_transport[i].GetComponent<Sword>().value;
                    Destroy(Stuff_to_transport[i]);
                    Stuff_to_transport.Remove(Stuff_to_transport[i]);
                }
            }
        }
    }
}
