using UnityEngine;

[CreateAssetMenu(fileName = "New CardData", menuName = "Card Data", order = 51)]
public class CardData : ScriptableObject
{
    [SerializeField]
    private string _cardName;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private int _cost;

    public string CardName { get { return _cardName; } }
    public Sprite Icon { get { return _icon; } }
    public int Cost { get { return _cost; } }
}
