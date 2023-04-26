using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    protected CardData _data;

    protected TextMeshPro _text;
    protected SpriteRenderer _sprite;

    protected MeshRenderer _meshRenderer;

    protected AudioSource _audioSource;

    [SerializeField]
    protected Color _selectedColor;

    protected void Awake()
    {
        _text = transform.GetChild(0).GetComponent<TextMeshPro>();
        _sprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        _meshRenderer = GetComponent<MeshRenderer>();
    }

    protected virtual void OnEnable()
    {
        _text.text = _data.CardName;
        _sprite.sprite = _data.Icon;

        AppearAnimation();

        Invoke(nameof(FlipAnimation), 2f);
    }

    protected void SelectedAnimation()
    {
        _meshRenderer.material.DOColor(_selectedColor, 0.5f);

        transform.DOMoveY(5f, 0.3f).SetEase(Ease.InOutQuad);
    }

    protected void DeselectedAnimation()
    {
        _meshRenderer.material.DOColor(Color.white, 0.5f);

        transform.DOMoveY(0f, 0.3f).SetEase(Ease.InOutQuad);
    }

    protected void FlipAnimation()
    {
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        transform.DOLocalRotate(new Vector3(0, 0, 180f), 0.5f).SetEase(Ease.InOutQuad).SetRelative();
    }

    protected void AppearAnimation()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(1f, 0.3f);
    }

    protected void DisappearAnimation()
    {
        transform.DOScale(0f, 0.3f);
    }

    protected void OnMouseEnter()
    {
        SelectedAnimation();
    }

    protected void OnMouseExit()
    {
        DeselectedAnimation();
    }

    protected virtual void OnMouseDown()
    {
        FlipAnimation();
    }
}
