using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{

    public List<GameObject> Track_Objects = new List<GameObject>();
    Object_Creation Generation_Object;
    // Start is called before the first frame update
    void Start()
    {
        Generation_Object = Find_Components.Find_Object_Creation();
        Set_Position_Tracks();
    }

    private void Set_Position_Tracks()
    {
        print("Making tracks, choo choo");
        if(StaticData.amount_standard_tracks > 0 && StaticData.amount_standard_tracks < 11)
        {
            while(Track_Objects.Count <= StaticData.amount_standard_tracks)
            {
                Track_Objects.Add(Generation_Object.Create_Track_Object(gameObject));
            }
            for (int i = 0; i < Track_Objects.Count; i++)
            {
                Track_Objects[i].transform.position = new Vector3(-12.5f + (i), 9, 0); //Her setter vi posisjonen, enkel matte. 
            }
        }
        if (StaticData.amount_standard_tracks < Track_Objects.Count) //Hvis vi har mer energi-objekter en vi skal ha, så slett. 
        {
            Destroy(Track_Objects[Track_Objects.Count - 1]);
            Track_Objects.RemoveAt(Track_Objects.Count - 1);
        }
    }
}
