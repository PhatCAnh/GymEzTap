
using System;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    
    [SerializeField] private Button _btnPopupUpdate, _btnSell;

    [SerializeField] private GameObject _goPrefabPopupUpdate, _goPrefabPopupNot;

    public TextMeshProUGUI txtCoinCurrent, txtAllCoinCurrent;

    public GameController gameController => GameController.instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _btnPopupUpdate.onClick.AddListener(OnClickBtnUpdate);
        _btnSell.onClick.AddListener(OnClickBtnSell);

        txtCoinCurrent.text = $"{PlayerPrefs.GetFloat("earning", 0)}";
        txtAllCoinCurrent.text = $"{PlayerPrefs.GetFloat("coin", 0)}";
    }

    public void CallPopupNotEnough()
    {
        var go = Instantiate(_goPrefabPopupNot, transform);
        go.transform.localScale = Vector3.zero;

        go.transform.DOScale(Vector3.one, 0.3f);
        go.transform.DOScale(Vector3.zero, 0.3f).SetDelay(1f);
    }

    public void OnClickBtnUpdate()
    {
        Instantiate(_goPrefabPopupUpdate, transform);
    }

    public void OnClickBtnSell()
    {
        gameController.Sell();
        txtCoinCurrent.text = "0";
        txtAllCoinCurrent.text = $"{gameController.allCoin}";
    }
}
