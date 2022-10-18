namespace SimplyGreatGames.PokerHoops
{
    public class CPUCoach : Coach
    {
        public void Awake()
        {
            CoachID = gameObject.GetInstanceID();

            GetComponents();
            RegisterComponents();
        }


        private void GetComponents()
        {
            if (Hand == null)
                Hand = GetComponentInChildren<Hand>();
        }

        private void RegisterComponents()
        {
            Hand.RegisterHand(this);
        }
    }
}
