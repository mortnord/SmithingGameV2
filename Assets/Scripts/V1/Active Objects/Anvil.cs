using UnityEngine;
public class Anvil : MonoBehaviour, IInteractor_Connector, IInteract_Work
{
    Object_Creation Generation_Object;
    public GameObject Converted_Object; //Resultat
    public GameObject blueprint_copy;
    public GameObject object_to_be_destroyed; //Inn-objekt. 
    public bool convert = false; //Om ingoten skal converteres. 
    // Start is called before the first frame update
    void Start()
    {
        Generation_Object = Find_Components.Find_Object_Creation(); // Her finner vi generation objektet for ? generate stuff fra ingots
    }
    // Update is called once per frame
    void Update()
    {
        if (convert && blueprint_copy != null && object_to_be_destroyed != null) //Sjekk om vi i det hele tatt har en blueprint
        {
            if (blueprint_copy.GetComponent<Blueprint_Sword>()) //Hvis sverd blueprint.
            {
                Converted_Object = Generation_Object.Create_Sword(object_to_be_destroyed.GetComponent<Ingot>().Get_Quality(), transform.position); // Her caller vi create sword
                                                                                                                                          // med ingoten sin quality, og anvilen sin posisjon
                Reset(); //Reset anvilen tilbake til normal og destroy ingot objektet
            }
        }
        else
        {
            convert = false; //ingen blueprint copy, s? da mislykkes converteringen
        }
    }
    private void Reset() // Lett m?te ? resette anvilen p?. Destroyer forrige objekt, og setter convert tilbake til false
    {
        Destroy(object_to_be_destroyed);
        convert = false;
    }
    public void Pickup(GameObject main_character) //Interface metode for ? hente ut converted objekt f?rst, s? pr?ver den ? rydde opp i blueprint copien
    {
        if (Converted_Object != null) // Her sjekker vi om vi har laget ett objekt
        {
            main_character.GetComponent<MainCharacterStateManager>().Set_Item_In_Inventory(Converted_Object);
            Converted_Object = null;
            Return_Answer(main_character, true); //Returnerer svar at vi fikk plukket oppe noe
        }
        else if (blueprint_copy != null) //hvis ikke, hent opp blueprint copien
        {
            main_character.GetComponent<MainCharacterStateManager>().Set_Item_In_Inventory(blueprint_copy);
            blueprint_copy = null;
            Return_Answer(main_character, true); //Returnerer svar at vi fikk plukket oppe noe
        }
        else if (object_to_be_destroyed != null) //hvis ikke, hent r?materialet
        {
            main_character.GetComponent<MainCharacterStateManager>().Set_Item_In_Inventory(object_to_be_destroyed);
            object_to_be_destroyed = null;
            Return_Answer(main_character, true); //Returnerer svar at vi fikk plukket oppe noe
        }
    }
    public void Drop_Off(GameObject main_character) //Interface metode for ? legge inn objekt. 
    {
        if (main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().GetComponent<Ingot>() != null && object_to_be_destroyed == null) //Her legger vi inn ingots i anvilen
        {
            object_to_be_destroyed = main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory(); //Legger inn og flytter r?materiale p? ambolten
            main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().transform.position = gameObject.transform.position;
            Return_Answer(main_character, false); //Returnerer svar at vi fikk lagt fra oss noe. 
        }
        else if (main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().GetComponent<Blueprint_Sword>() != null && blueprint_copy == null) //Her kommer blueprints inn i anvilen
        {
            blueprint_copy = main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory(); 
            main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().transform.position = gameObject.transform.position;
            Return_Answer(main_character, false); //Returnerer svar at vi fikk lagt fra oss noe.
        }
    }
    public void Return_Answer(GameObject main_character, bool result) //Interface metode for ? returnere svar. 
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }
    public void Work(GameObject main_character) //Interface metode for ? gj?re arbeid, i dette tilfelle konvertere ett objekt. 
    {
        convert = true;
    }
}
