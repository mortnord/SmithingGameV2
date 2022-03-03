using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Energy_Objects = new List<GameObject>();
    public List<GameObject> Beers = new List<GameObject>();
    Object_Creation Generation_Object;

    int amount_of_energy_objects;
    void Start()
    {
        amount_of_energy_objects = (int)Math.Ceiling(StaticData.Energy_mining_static / 2d);
        Generation_Object = Find_Components.find_Object_Creation();
        
        SetPosition_Energy(); //Vi setter posisjonen til energy-sprites
        setPosition_Beers(); //Evnt øl / energibarer
    }

    public void SetPosition_Energy() //Her setter vi energien til posisjon og mengde
    {

        amount_of_energy_objects = (int)Math.Ceiling(StaticData.Energy_mining_static / 2d); //Finn ut hvor mange vi trenger
        if (StaticData.Energy_mining_static > 0) //Så lenge vi har energi da..
        {
            while (Energy_Objects.Count <= amount_of_energy_objects) //vis vi skal ha mer objekter enn vi har, så lag og legg til
            {
                Energy_Objects.Add(Generation_Object.create_energy_indicator(gameObject));
                StaticData.Amount_of_energy_objects++;
            }
            for (int i = 0; i < Energy_Objects.Count; i++)
            {
                Energy_Objects[i].transform.position = new Vector3(-12.5f + (i), 9, 0); //Her setter vi posisjonen, enkel matte. 
            }
        }
        if(StaticData.Amount_of_energy_objects >= amount_of_energy_objects) //Hvis vi har mer energi-objekter en vi skal ha, så slett. 
        {
            Destroy(Energy_Objects[Energy_Objects.Count - 1]);
            Energy_Objects.RemoveAt(Energy_Objects.Count-1);
        }
        change_sprite();

    }
    public void setPosition_Beers()
    {
        if(StaticData.amount_of_beer_static > 0) //Først gjør kun ting vis vi har mer enn 0 øl. 
        {
            while (Beers.Count < StaticData.amount_of_beer_static) //Vis vi skal ha øl, men har ingen, lag og legg i lista. 
            {
                Beers.Add(Generation_Object.create_beer_object(gameObject));
            }
            for (int i = 0; i < Beers.Count; i++) //Så setter vi posisjonen til objektene. 
            {
                Beers[i].transform.position = new Vector3(-13 + (i), 7.95f, 0);
            }
        }
        if(StaticData.amount_of_beer_static < Beers.Count) //Fjern vis vi ikke skal ha øl. 
        {

            Destroy(Beers[Beers.Count - 1]);
            Beers.RemoveAt(Beers.Count-1);
            for (int i = 0; i < StaticData.Energy_value_beer; i++) //Gi energi tilsvarnede energien vi har. 
            {
                if(StaticData.Energy_mining_static < 20)
                {
                    StaticData.Energy_mining_static++; //Legg til energi. 
                    SetPosition_Energy();
                    
                }
            }
        }
    }
    public void change_sprite() //Denne sjekker om vi ska ha 1 eller 2 pickaxes. 
    {
        int last_object = Energy_Objects.Count - 1;
        if(StaticData.Energy_mining_static % 2 != 0 && StaticData.Energy_mining_static > 0) //Vis vi har mer enn 0, men ett oddetall skal en pickaxe vises
        {
            Energy_Objects[last_object].GetComponent<Pickaxe>().spriteRenderer.sprite = Energy_Objects[last_object].GetComponent<Pickaxe>().spriteArray[1];
        }
        else if(StaticData.Energy_mining_static > 0) //hvis vi har mer enn 0, men ikke ett oddetall skal 2 vises. 
        {
            Energy_Objects[last_object].GetComponent<Pickaxe>().spriteRenderer.sprite = Energy_Objects[last_object].GetComponent<Pickaxe>().spriteArray[0];
        }
    }
}
