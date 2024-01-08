using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UILogic
{
    public abstract class FillableImage : MonoBehaviour
    {
        [SerializeField] protected Image _fillableImage;

        protected float _imageFillingDuration = 0.25f;
        
        protected void SetIconFill(float amount, bool withAnimation = false)
        {
            _fillableImage.DOComplete();
            if (withAnimation) _fillableImage.DOFillAmount(amount, _imageFillingDuration);
            else _fillableImage.fillAmount = amount;
        }
    }
}