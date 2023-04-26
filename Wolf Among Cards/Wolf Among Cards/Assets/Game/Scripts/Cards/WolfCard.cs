using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WolfCard : Card
{
    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        Debug.Log(_data.CardName + " " + _data.Cost.ToString());
    }
}
