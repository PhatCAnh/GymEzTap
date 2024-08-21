
using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    public static MainCharacter instance;
    
    [SerializeField] private Transform _skin;

    [SerializeField] public Transform _head;

    [SerializeField] private Cooldown _cdClicked;

    [SerializeField] private Slider _sliderCdClick;
    
    [SerializeField] private Slider _sliderCdAutoClick;

    [SerializeField] private Cooldown _cdAutoClick;
    
    private float _timeAutoClick;
    
    private float _timeClicked;

    private float _earning;

    public int itemEarning;
    
    private GameController GameController => GameController.instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _timeClicked = GameController.valueLiftSpeed;
        _timeAutoClick = GameController.valueAutoLiftSpeed;
        _earning = GameController.valueEarning;

        itemEarning = GameController.instance.listBought.LastOrDefault()!.value;
        
        _cdClicked = new Cooldown(_timeClicked);
        _cdAutoClick = new Cooldown(_timeAutoClick); 
        _sliderCdClick.maxValue = _timeClicked;
        _sliderCdAutoClick.maxValue = _timeAutoClick;
    }

    private void Update()
    {
        var time = Time.deltaTime;
        if(_cdClicked.isFinished)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Clicked();
            }
            else if(_cdAutoClick.isFinished)
            {
                Clicked();
            }
            else
            {
                _cdAutoClick.Update(time);
                _sliderCdAutoClick.value += time;
            }
        }
        else
        {
            _cdClicked.Update(time);
            _sliderCdClick.value += time;
        }
    }

    public void Clicked()
    {
        //fix it
        
        //chua lam cai earning
        var value = itemEarning * _earning;
        
        GameController.UpdateCoin(value);
        
        _head.localScale += value * 0.01f * Vector3.one;
        _sliderCdClick.value = 0;
        _sliderCdAutoClick.value = 0;
        _cdClicked.Restart(_timeClicked);
        _cdAutoClick.Restart(_timeAutoClick);
    }

    public void UpdateLiftSpeed(float value)
    {
        _timeClicked = value;
        _sliderCdClick.maxValue = _timeClicked;
    }
    
    public void UpdateEarning(float value)
    {
        _earning = value;
    }
    
    public void UpdateAutoLiftSpeed(float value)
    {
        _timeAutoClick = value;
        _sliderCdAutoClick.maxValue = _timeAutoClick;
    }
}
