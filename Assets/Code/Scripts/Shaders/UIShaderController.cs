using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SimplyGreatGames.PokerHoops
{
    public class UIShaderController : MonoBehaviour
    {
        public Image Image { get; private set; }

        [Header("Settings")]
        [Range(.1f, 1)] public float ShineDuration = .5f;

        #region Shader Properties

        private static string shineLocation = "_ShineLocation";

        #endregion

        #region Unity Methods & Initialization

        public void Awake()
        {
            InitializeMaterial();
        }

        private void InitializeMaterial()
        {
            Image = GetComponent<Image>();
            Image.material = new Material(Image.material);
        }

        #endregion

        #region Shader Methods

        public void OnTweenShineSignal() => TweenShine();
        public void TweenShine()
        {
            Image.material.SetFloat(shineLocation, 0f);
            Image.material.DOFloat(1, shineLocation, ShineDuration);
        }

        #endregion
    }
}
