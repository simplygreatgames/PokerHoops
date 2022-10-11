using UnityEngine;

namespace SimplyGreatGames.PokerHoops
{
    public class SeasonManager : MonoBehaviour
    {
        public static SeasonManager Instance;

        [Header("Settings")]
        [SerializeField] private GameObject seasonPrefab;

        public Season CurrentSeason { get; private set; }

        public void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        #region Season Management

        public void LoadNewSeason(SeasonSettings seasonSettings)
        {
            GameObject seasonObj = Instantiate(seasonPrefab, transform);
            CurrentSeason = seasonObj.GetComponent<Season>();

            CurrentSeason.SetSeasonSettings(seasonSettings);
        }

        #endregion 
    }
}
