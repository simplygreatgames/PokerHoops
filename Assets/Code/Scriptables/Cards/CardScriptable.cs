using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    [CreateAssetMenu(fileName = "Card", menuName = "Scriptables/Cards/Card")]
    public class CardScriptable : ScriptableObject
    {
        public Enums.CardSuits Suit;
        public int Value;

        [Header("Art")]
        public Sprite SuitOverlay;
        public Sprite ValueOverlay;
        public Sprite ArtBackground;
        public Sprite FrameOverlay;
    }
}
