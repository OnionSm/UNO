using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public void DistributeCard(List<GameObject> _list_player, List<Transform> _deck)
    {
        if (_deck.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < 7; i++)
        {
            for (int player_number = 0; i < _list_player.Count; player_number++)
            {
                Transform card = _deck[0];
                _deck.Remove(card);
                _list_player[player_number].GetComponent<IDrawable>()._list_card_in_hand.Add(card);
            }
        }
    }
}
