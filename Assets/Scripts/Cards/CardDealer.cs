using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{

    public void DistributeCard(List<GameObject> _list_player, List<CardConfig> _card_in_deck_remain)
    {
        if (_card_in_deck_remain.Count <= 0)
        {
            return;
        }
        for (int player_number = 0; player_number < _list_player.Count; player_number++)
        {
            /*Debug.Log(_list_player[player_number]);*/
            _list_player[player_number].gameObject.GetComponent<IDrawable>().Draw(7);
        }
    }


}
