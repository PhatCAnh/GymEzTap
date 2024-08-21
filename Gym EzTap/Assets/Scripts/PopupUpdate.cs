using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupUpdate : MonoBehaviour
{
    [SerializeField] private Button _btnLiftSpeed;
    [SerializeField] private Button _btnEarning;
    [SerializeField] private Button _btnAutoLiftSpeed;
    [SerializeField] private Button _btnAdsLiftSpeed;
    [SerializeField] private Button _btnAdsEarning;
    [SerializeField] private Button _btnAdsAutoLiftSpeed, _btnClose;

    [SerializeField] private TextMeshProUGUI _txtValueLiftSpeed, _txtValueEarning, _txtValueAuto;
    
    [SerializeField] private TextMeshProUGUI _txtGoldLiftSpeed, _txtGoldEarning, _txtGoldAuto;

    [SerializeField] private GameObject _goMainContent;
    
    private GameController GameController = GameController.instance;

    private void Start()
    {
        _txtValueLiftSpeed.text = $"{(float)Math.Round(GameController.valueLiftSpeed, 2)}";
        _txtValueEarning.text = $"{(float)Math.Round(GameController.valueEarning, 2)}";
        _txtValueAuto.text = $"{(float)Math.Round(GameController.valueAutoLiftSpeed, 2)}";
        
        if(GameController.levelLiftSpeed >= 10)
        {
            _btnLiftSpeed.interactable = false;
            _txtGoldLiftSpeed.text = "MAX";
        }
        else
        {
            _btnLiftSpeed.onClick.AddListener(OnClickBtnLiftSpeed);
        }

        if(GameController.levelEarning >= 10)
        {
            _btnEarning.interactable = false;
            _txtGoldEarning.text = "MAX";
        }
        else
        {
            _btnEarning.onClick.AddListener(OnClickBtnEarning);
        }

        if(GameController.levelAutoLiftSpeed >= 10)
        {
            _btnAutoLiftSpeed.interactable = false;
            _txtGoldAuto.text = "MAX";
        }
        else
        {
            _btnAutoLiftSpeed.onClick.AddListener(OnClickAutoLiftSpeed);
        }
        
        _btnClose.onClick.AddListener(Close);
        
        _goMainContent.transform.localScale = Vector3.zero;

        _goMainContent.transform.DOScale(Vector3.one, 0.15f);
    }

    private void Close()
    {
        _goMainContent.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnClickBtnLiftSpeed()
    {
        GameController.UpdateLiftSpeed();
        if(GameController.levelLiftSpeed >= 10)
        {
            _btnLiftSpeed.interactable = false;
            _txtGoldLiftSpeed.text = "MAX";
        }
        _txtValueLiftSpeed.text = $"{(float)Math.Round(GameController.valueLiftSpeed, 2)}";
    }

    private void OnClickBtnEarning()
    {
        GameController.UpdateEarning();
        if(GameController.levelEarning >= 10)
        {
            _btnEarning.interactable = false;
            _txtGoldEarning.text = "MAX";
        }
        _txtValueEarning.text = $"{(float)Math.Round(GameController.valueEarning, 2)}";

    }

    private void OnClickAutoLiftSpeed()
    {
        GameController.UpdateAutoLiftSpeed();
        if(GameController.levelAutoLiftSpeed >= 10)
        {
            _btnAutoLiftSpeed.interactable = false;
            _txtGoldAuto.text = "MAX";
        }
        _txtValueAuto.text = $"{(float)Math.Round(GameController.valueAutoLiftSpeed, 2)}";
    }
}
