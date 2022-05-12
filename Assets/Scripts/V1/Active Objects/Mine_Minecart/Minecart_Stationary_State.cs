using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart_Stationary_State : Minecart_Base_State, IInteractor_Connector
{
    Minecart_State_Manager minecart_object;

    public override void Drop_Off(GameObject main_character)
    {
        main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().transform.position = minecart_object.gameObject.transform.position;
        main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().SetActive(false);

        //Temp
        IncreaseTracksValue(main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().GetComponent<Common_Properties>().Get_Ore_Quality());

        StaticData.Transition_Ores.Add(main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().GetComponent<Common_Properties>().Get_Ore_Quality());
        StaticData.percent_ore_quality_transition.Add(main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().GetComponent<Ore>().Get_Percent_Ore_To_Ingot());
        Ores_in_tray.Add(main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory());
        Return_Answer(main_character, false); //returnere svar 

    }

    private void IncreaseTracksValue(Enumtypes.Ore_Quality ore_Quality)
    {
        if((int)ore_Quality == 0)
        {
            StaticData.amount_standard_tracks++;
        }

        if ((int)ore_Quality == 1)
        {
            StaticData.amount_standard_tracks+= 2;
        }
        if ((int)ore_Quality == 2)
        {
            StaticData.amount_standard_tracks+= 3;
        }

    }

    public override void EnterState(Minecart_State_Manager minecart, Rigidbody2D rb)
    {
        minecart_object = minecart;
        rb.velocity = new Vector2(0, 0);
    }

    public override void Pickup(GameObject main_character)
    {
        // Do nothing
    }

    public override void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    public override void UpdateState(Minecart_State_Manager minecart, Rigidbody2D rb)
    {

    }
}
