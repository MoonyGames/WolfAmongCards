using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WolfCard : Card
{
    protected override void OnMouseDown()
    {
        if (!_isFlipped && canBeChoosen)
        {
            _isFlipped = true;
            canBeChoosen = false;

            FlipAnimation();

            CardsPooler.Instance.Level++;

            ScoresCounter.Instance.AddScores(_data.Cost);

            StartCoroutine(DeactivateCardsByTimer(3f));
        }
    }

    private IEnumerator DeactivateCardsByTimer(float time)
    {
        yield return new WaitForSeconds(time);

        CardsPooler.Instance.DeactivateCards();
    }
}
