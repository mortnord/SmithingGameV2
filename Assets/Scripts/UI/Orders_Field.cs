using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orders_Field : MonoBehaviour
{
    Mission_System Mission_System_object;
    // Start is called before the first frame update
    void Start()
    {
        Mission_System_object = GetComponentInChildren<Mission_System>();
    }

    // Update is called once per frame
    void Update()
    {
        setMissionPosisions();
    }

    public void setMissionPosisions()
    {

        for (int i = 0; Mission_System_object.Missions_in_UI.Count > i; i++)
        {
            Mission_System_object.Missions_in_UI[i].transform.position = new Vector3(Mission_System_object.Missions_in_UI[i].transform.position.x + 0.05f * 1 * Time.deltaTime, Mission_System_object.Missions_in_UI[i].transform.position.y);
        }
    }
}
