using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossCards : MonoBehaviour
{
    public static TossCards instance = null;
    public List<Card> Cards = new List<Card>();

    private List<int> _cardsIndexes = new List<int>();

    private void Awake() { instance = this; }

    private void UpdateLength()
    {
        for(int i = 0; i < Cards.Count; i++)
            _cardsIndexes.Add(i);

        Debug.Log(Cards.Count);
    }

    public void Toss(int times)
    {
        UpdateLength();

        for(int i = 0; i < times; i++)
        {
            for(int y = 0; y < Cards.Count; i++)
            {
                int randomNumber = Random.Range(0, _cardsIndexes.Count);

                Cards[y].MoveTo(Cards[_cardsIndexes[randomNumber]].transform.position);

                _cardsIndexes.RemoveAt(randomNumber);
            }
        }

        Cards.Clear();
        _cardsIndexes.Clear();
    }
}
