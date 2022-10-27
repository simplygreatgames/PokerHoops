using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Interfaces : MonoBehaviour
    {
        public interface IInteractable
        {
            public void OnLeftClick(PlayerCoach inputOwner);
            public void OnRightClick(PlayerCoach inputOwner);
        }
    }
}
