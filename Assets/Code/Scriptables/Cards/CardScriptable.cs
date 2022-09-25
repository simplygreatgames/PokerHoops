using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CreateAssetMenu(fileName = "Card", menuName = "Scriptables/Cards/Card")]
    public class CardScriptable : ScriptableObject
    {
        public Enums.CardSuits Suit;
        public int Value;
        public Sprite Sprite;
    }
}
