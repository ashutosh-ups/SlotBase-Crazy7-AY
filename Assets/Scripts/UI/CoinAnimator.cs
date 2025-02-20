using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CoinAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] CoinList;
    [SerializeField]
    private GameObject AnimGameObject;

    [SerializeField]
    private Tween[] allCoinTween;


    [SerializeField] Transform CoinStartPos;
    [SerializeField] Transform CoinMiddlePos;
    [SerializeField] Transform CoinEndPos;
    [SerializeField] TMP_Text CoinAddTxt;

    private void Start()
    {
        foreach (var item in CoinList)
        {
            item.SetActive(false);
        }
       // StartCoroutine(CoinSpawn(5));
    }


    internal IEnumerator StartCoinAnimation(double WinAmount)
    {
        AnimGameObject.SetActive(true);
        yield return (CoinSpawn(WinAmount));
        foreach (var coin in CoinList)
        {
            coin.SetActive(false);
        }
       // CoinAddTxt.gameObject.SetActive(false);
        //AnimGameObject.SetActive(false);
    }

    internal void StopCoinAnimation()
    {
        foreach (var coin in CoinList)
        {
            coin.SetActive(false);
        }
        CoinAddTxt.gameObject.SetActive(false);
        AnimGameObject.SetActive(false);
    }
    IEnumerator CoinSpawn(double amount)
    {
        foreach (var coin in CoinList)
        {

            Vector2 StartPos = new Vector2(CoinStartPos.localPosition.x + Random.Range(-100, 100), CoinStartPos.localPosition.y + Random.Range(-10, 10));
            //coin.transform.DOLocalMove(StartPos,0.05f);
            coin.transform.localPosition = StartPos;
            coin.SetActive(true);

            yield return new WaitForSeconds(0.02f);
            //yield return new WaitForSeconds(Random.Range(0, 0.5f));
            Vector2 StartPos2 = new Vector2(CoinMiddlePos.localPosition.x + Random.Range(-50, 50), CoinMiddlePos.localPosition.y + Random.Range(-25, 25));
            coin.transform.DOLocalMove(StartPos2, Random.Range(0.25f, 0.5f)).SetEase(Ease.Linear);

           
            float WaitTime = Random.Range(1, 1.5f);
            if (WaitTime % 3 == 0)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.5f));
            }
            yield return null;


        }
        double initAmount = 0;
        CoinAddTxt.gameObject.SetActive(true);
        DOTween.To(() => initAmount, (val) => initAmount = val, amount, 0.5f).OnUpdate(() =>
        {
            if (CoinAddTxt) CoinAddTxt.text = "+"+initAmount.ToString("F3");
        });

        foreach (var coin in CoinList)
        {
            coin.transform.DOLocalMove(CoinEndPos.localPosition, Random.Range(0.1f, 0.2f)).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.05f);
            //coin.SetActive(false);
        }
    
    }
}
