using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class CardSpawner : MonoBehaviour
    {
        public static CardSpawner Instance = null;

        [Header("Settings")]
        public GameObject CardPrefab;

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public GameObject SpawnCard(Transform parent, CardScriptable cardScriptable)
        {
            GameObject cardObj = Instantiate(CardPrefab, parent);
            Card card = cardObj.GetComponent<Card>();

            card.CardScriptable = cardScriptable;
            return cardObj;
        }
    }
}
