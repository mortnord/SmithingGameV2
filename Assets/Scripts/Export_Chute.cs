using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Export_Chute : MonoBehaviour
{
    Score Score_object;
    // Start is called before the first frame update
    public int transport_speed = 1;
    public List<GameObject> Stuff_to_transport = new List<GameObject>();
    void Start()
    {
        Score_object = Find_Components.find_score();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stuff_to_transport.Count > 0)
        {
            /*
            foreach (GameObject Object_To_transport in Stuff_to_transport)
            {
                Object_To_transport.transform.position = new Vector3(Object_To_transport.transform.position.x  + 0.05f * transport_speed * Time.deltaTime, Object_To_transport.transform.position.y);

                if (Object_To_transport.transform.position.x > 8)
                {
                    Score_object.score += Object_To_transport.GetComponent<Sword>().value;
                    Stuff_to_transport.Remove(Object_To_transport);
                    Destroy(Object_To_transport);
                }
            }*/
            for (int i = 0; i < Stuff_to_transport.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
            {
                Stuff_to_transport[i].transform.position = new Vector3(Stuff_to_transport[i].transform.position.x + 0.05f * transport_speed * Time.deltaTime, Stuff_to_transport[i].transform.position.y);
                if (Stuff_to_transport[i].transform.position.x > 7.2f)
                {
                    Score_object.score += Stuff_to_transport[i].GetComponent<Sword>().value;
                    Destroy(Stuff_to_transport[i]);
                    Stuff_to_transport.Remove(Stuff_to_transport[i]);
                }
            }
        }
    }
}
