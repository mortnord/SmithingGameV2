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
        for (int i = 0; i < StaticData.Energy_mining_static; i++) //Her lager vi energy-objekter tilsvarende energi / 2. 
        {
            Energy_Objects.Add(Generation_Object.create_energy_indicator(gameObject));
            i++;
            
        }
        SetPosition_Energy(); //Vi setter posisjonen til energy-sprites
        calculateSprites(); //Beregner hvor mange som skal vises
        setPosition_Beers(); //Evnt øl / energibarer
    }

    public void calculateSprites()
    {
        int amount_of_objects = (int)Math.Ceiling(StaticData.Energy_mining_static/ 2d); //Vi vil ha halvparten rundet opp, dv.s 19 blir 9.5 som blir 10, for 10 objekter
        while (amount_of_objects < Energy_Objects.Count) //Her fjerner vi energi som ikke lengre skal tegnes, og setter de i en egen liste. 
        {
            Energy_Objects[Energy_Objects.Count - 1].SetActive(false); //Gjør usynlng
            Removed_Energy_objects.Add(Energy_Objects[Energy_Objects.Count - 1]); //Lag i backup list
            Energy_Objects.RemoveAt(Energy_Objects.Count - 1); //Fjern fra hovedlist

        }
        while(amount_of_objects > Energy_Objects.Count) //Her returnerer vi energien
        {
            Energy_Objects.Add(Removed_Energy_objects[Removed_Energy_objects.Count-1]); //legg i hovedlista
            Removed_Energy_objects[Removed_Energy_objects.Count-1].SetActive(true); //Gjør synlig
            Removed_Energy_objects.RemoveAt(Removed_Energy_objects.Count-1); //fjern fra backuplista

        }
        change_sprite(); //Her bytter vi om ytterste sprite ska vær en eller 2 pickaxes i symbol

    }
    public void SetPosition_Energy()
    {
        for (int i = 0; i < Energy_Objects.Count; i++)
        {
            Energy_Objects[i].transform.position = new Vector3(-12.5f + (i), 9, 0); //Her setter vi posisjonen, enkel matte. 
        }
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
                StaticData.Energy_mining_static++; //Legg til energi. 
                calculateSprites(); 
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
