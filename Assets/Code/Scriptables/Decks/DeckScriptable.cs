using System.Collections.Generic;
using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CreateAssetMenu(fileName = "Deck", menuName = "Scriptables/Cards/Deck")]
    public class DeckScriptable : ScriptableObject
    {
        public CardScriptable[] Cards;
    }
}
