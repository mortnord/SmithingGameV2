using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Energy_Objects = new List<GameObject>();
    public List<GameObject> Removed_Energy_objects = new List<GameObject>();
    public List<GameObject> Beers = new List<GameObject>();
    Object_Creation Generation_Object;
    void Start()
    {
        Generation_Object = Find_Components.find_Object_Creation();
        for (int i = 0; i < StaticData.Energy_mining_static; i++)
        {
            Energy_Objects.Add(Generation_Object.create_energy_indicator(gameObject));
            i++;
            
        }
        SetPosition_Energy();
        calculateSprites();
        setPosition_Beers();
    }

    public void calculateSprites()
    {
        int amount_of_objects = (int)Math.Ceiling(StaticData.Energy_mining_static/ 2d);
        while (amount_of_objects < Energy_Objects.Count)
        {
            Energy_Objects[Energy_Objects.Count - 1].SetActive(false);
            Removed_Energy_objects.Add(Energy_Objects[Energy_Objects.Count - 1]);
            Energy_Objects.RemoveAt(Energy_Objects.Count - 1);

        }
        while(amount_of_objects > Energy_Objects.Count)
        {
            Energy_Objects.Add(Removed_Energy_objects[Removed_Energy_objects.Count-1]);
            Removed_Energy_objects[Removed_Energy_objects.Count-1].SetActive(true);
            Removed_Energy_objects.RemoveAt(Removed_Energy_objects.Count-1);

        }
        change_sprite();

    }
    public void SetPosition_Energy()
    {
        for (int i = 0; i < Energy_Objects.Count; i++)
        {
            Energy_Objects[i].transform.position = new Vector3(-12.5f + (i), 9, 0);
        }
    }
    public void setPosition_Beers()
    {
        if(StaticData.amount_of_beer_static > 0)
        {
            while (Beers.Count < StaticData.amount_of_beer_static)
            {
                Beers.Add(Generation_Object.create_beer_object(gameObject));
            }
            for (int i = 0; i < Beers.Count; i++)
            {
                Beers[i].transform.position = new Vector3(-13 + (i), 7.95f, 0);
            }
        }
        if(StaticData.amount_of_beer_static < Beers.Count)
        {

            Destroy(Beers[Beers.Count - 1]);
            Beers.RemoveAt(Beers.Count-1);
            for (int i = 0; i < StaticData.Energy_value_beer; i++)
            {
                StaticData.Energy_mining_static++;
                calculateSprites();
            }
            
        }
    }
    public void change_sprite()
    {
        int last_object = Energy_Objects.Count - 1;
        if(StaticData.Energy_mining_static % 2 != 0)
        {
            Energy_Objects[last_object].GetComponent<Pickaxe>().spriteRenderer.sprite = Energy_Objects[last_object].GetComponent<Pickaxe>().spriteArray[1];
        }
        else
        {
            Energy_Objects[last_object].GetComponent<Pickaxe>().spriteRenderer.sprite = Energy_Objects[last_object].GetComponent<Pickaxe>().spriteArray[0];
        }
    }
}
