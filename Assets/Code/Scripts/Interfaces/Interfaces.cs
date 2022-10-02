using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class Interfaces : MonoBehaviour
    {
        public interface IInteractable
        {
            public void OnLeftClick();
            public void OnRightClick();
        }
    }
}
