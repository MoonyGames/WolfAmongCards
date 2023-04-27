using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OtherCard : Card
{
    [SerializeField]
    private CardData[] _cardDatas;

    protected override void OnEnable()
    {
        _data = _cardDatas[Random.Range(0, _cardDatas.Length)];

        base.OnEnable();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }
}
