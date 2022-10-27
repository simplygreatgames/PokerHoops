using UnityEngine;
using DG.Tweening;

namespace SimplyGreatGames.PokerHoops
{
    public class Card : MonoBehaviour, Interfaces.IInteractable
    {
        [Header("Run Time Data")]
        [SerializeField] private CardScriptable cardScriptable;
        public CardScriptable CardScriptable
        {
            get { return cardScriptable; }
            set
            {
                cardScriptable = value;

                if (CardScriptable != null) InitializeCard();
                else DeinitializeCard();
            }
        }

        [SerializeField] private CardBackScriptable cardBackScriptable;
        public CardBackScriptable CardBackScriptable
        {
            get { return cardBackScriptable; }
            set
            {
                cardBackScriptable = value;
                SetCardArt();
            }
        }

        [SerializeField] private bool isToBeDiscarded = false;
        public bool IsToBeDiscarded { get => isToBeDiscarded; private set => isToBeDiscarded = value; }
        [SerializeField] private Coach currentOwner = null;
        public Coach CurrentOwner { get => currentOwner; private set => currentOwner = value; }

        [SerializeField] private int currentSlot = -1;
        public int CurrentSlot { get => currentSlot; private set => currentSlot = value; }

        [Header("Components")]
        [SerializeField] private Animator animator;
        public Animator Animator { get { return animator; } private set { animator = value; } }

        [Header("Sprites")]
        [SerializeField] private SpriteRenderer suitSprite;
        public SpriteRenderer SuitSprite { get { return suitSprite; } private set { suitSprite = value; } }

        [SerializeField] private SpriteRenderer valueSprite;
        public SpriteRenderer ValueSprite { get { return valueSprite; } private set { valueSprite = value; } }

        [SerializeField] private SpriteRenderer frameSprite;
        public SpriteRenderer FrameSprite { get { return frameSprite; } private set { frameSprite = value; } }

        [SerializeField] private SpriteRenderer artSprite;
        public SpriteRenderer ArtSprite { get { return artSprite; } private set { artSprite = value; } }

        [SerializeField] private SpriteRenderer backSprite;
        public SpriteRenderer BackSprite { get { return backSprite; } private set { backSprite = value; } }


        public Enums.CardSuits Suit { get; private set; }
        public int Value { get; private set; }

        private bool isFacing = true;

        #region Unity Methods

        public void Awake()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            if (animator == null)
                Animator = GetComponent<Animator>();

            if (artSprite == null)
                ArtSprite = GetComponentInChildren<SpriteRenderer>();
        }

        #endregion

        #region Owner Methods

        public void RegisterOwner(Coach newOwner, int slot)
        {
            CurrentOwner = newOwner;
            CurrentSlot = slot;
            IsToBeDiscarded = false;
        }

        public void DeregisterOwner()
        {
            CurrentOwner = null;
            CurrentSlot = -1;
        }

        public void DeclareForDiscard(bool isToBeDiscarded) => IsToBeDiscarded = isToBeDiscarded;

        #endregion

        #region Card Scriptable Methods

        public void InitializeCard()
        {
            SetCardArt();
            SetCardSuit();
            SetCardValue();
        }
        public void DeinitializeCard()
        {
            SuitSprite.sprite = null;
            ValueSprite.sprite = null;
            FrameSprite.sprite = null;
            ArtSprite.sprite = null;
            BackSprite.sprite = null;
        }

        private void SetCardArt()
        {
            if (CardScriptable != null)
            {
                SuitSprite.sprite = CardScriptable.SuitOverlay;
                ValueSprite.sprite = CardScriptable.ValueOverlay;
                FrameSprite.sprite = CardScriptable.FrameOverlay;
                ArtSprite.sprite = CardScriptable.ArtBackground;
            }

            if (CardBackScriptable != null)
                BackSprite.sprite = CardBackScriptable.Sprite;
        }

        private void SetCardSuit()
        {
            if (CardScriptable == null)
            {
                Debug.LogWarning("Card Scriptable not set on " + gameObject.name);
                return;
            }

            Suit = CardScriptable.Suit;
        }

        private void SetCardValue()
        {
            if (CardScriptable == null)
            {
                Debug.LogWarning("Card Scriptable not set on " + gameObject.name);
                return;
            }

            Value = CardScriptable.Value;
        }

        #endregion

        #region Movement Animations

        public void FlipCard()
        {
            isFacing = !isFacing;
            Animator.SetBool("IsFacing", isFacing);
        }

        #endregion

        #region IInteractable Interface

        public void OnLeftClick(PlayerCoach inputOwner)
        {
            Debug.Log("Coach with ID: " + inputOwner.CoachID + " Left Clicked: " + CardScriptable.name);

            inputOwner.CardController.OnCardLeftClicked(this);
        }

        public void OnRightClick(PlayerCoach inputOwner)
        {
            Debug.Log("Coach with ID: " + inputOwner.CoachID + " Right Clicked: " + CardScriptable.name);

            inputOwner.CardController.OnCardRightClicked(this);

        }

        #endregion
    }
}
