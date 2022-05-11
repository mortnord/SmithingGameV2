using UnityEngine;
public class Table : MonoBehaviour, IInteractor_Connector
{
    Object_Creation Generation_Object;
    GameObject blueprint_original_on_table;
    public GameObject fake_copy;
    public bool copied = true;
    // Start is called before the first frame update
    void Start()
    {
        blueprint_original_on_table = transform.GetChild(0).gameObject; //Finn child-objektet, vi setter det i editoren
        Generation_Object = Find_Components.Find_Object_Creation(); //Generation objektet
    }
    // Update is called once per frame
    void Update()
    {
        if (copied == true) //Denne brukes for � lage en ny kopi med engang vi har fjernet den f�rste kopien, da har vi alltid en kopi liggende som vi kan plukke opp
        {                  //Kopien ligger under orginalen, s� spiller vil ikke se den. 
            if (blueprint_original_on_table.GetComponent<Blueprint_Sword>())
            {
                copied = false;
                fake_copy = Generation_Object.Create_Blueprint_Sword(transform.position); //Posisjonen til kopien er lik posisjonen til bordet. 
            }
        }
    }
    public void Pickup(GameObject main_character) //Interface metode for � hente opp blueprint copies. 
    {
        main_character.GetComponent<MainCharacterStateManager>().Set_Item_In_Inventory(fake_copy);
        fake_copy = null;
        main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().transform.parent = null;
        copied = true;
        Return_Answer(main_character, true);
    }
    public void Drop_Off(GameObject main_character)
    {
        //Do nothing
    }
    public void Return_Answer(GameObject main_character, bool result) //Returnerer svar til dwarf character
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }
}
