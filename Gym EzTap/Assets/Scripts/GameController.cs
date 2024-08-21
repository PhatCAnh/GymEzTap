using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
namespace DefaultNamespace
{
	public class GameController : MonoBehaviour
	{
		public static GameController instance;
		
		private Dictionary<string, ItemSellData> _dicDataSell;
		public List<ItemUpdate> listDataLiftSpeed;
		public List<ItemUpdate> listDataEarning;
		public List<ItemUpdate> listDataAutoLiftSpeed;

		public int levelLiftSpeed;

		public int levelEarning;

		public int levelAutoLiftSpeed;
		
		public float valueLiftSpeed;

		public float valueEarning;

		public float valueAutoLiftSpeed;

		private float _currentCoin;

		public float allCoin;

		public List<ItemSellData> listBought = new List<ItemSellData>();

		private MainCharacter MainCharacter => MainCharacter.instance;
		
		
		private void Awake()
		{
			instance = this;
			
			levelLiftSpeed = PlayerPrefs.GetInt("level_LiftSpeed", 0);
			levelEarning = PlayerPrefs.GetInt("level_Earning", 0);
			levelAutoLiftSpeed = PlayerPrefs.GetInt("level_AutoLiftSpeed", 0);

			_currentCoin = PlayerPrefs.GetFloat("earning", 0);
			allCoin = PlayerPrefs.GetFloat("coin", 0);
			
			
			
			

			_dicDataSell = new Dictionary<string, ItemSellData>
			{
				{"Sell1", new ItemSellData("Sell1", 0, 1)},
				{"Sell2", new ItemSellData("Sell2", 10, 2)},
				{"Sell3", new ItemSellData("Sell3", 20, 3)},
				{"Sell4", new ItemSellData("Sell4", 45, 5)},
				{"Sell5", new ItemSellData("Sell5", 75, 8)},
				{"Sell6", new ItemSellData("Sell6", 120, 13)},
				{"Sell7", new ItemSellData("Sell7", 1000, 21)},
				{"Sell8", new ItemSellData("Sell8", 1000, 34)},
				{"Sell9", new ItemSellData("Sell9", 1000, 55)},
				{"Sell10", new ItemSellData("Sell10", 1000, 89)},
				{"Sell11", new ItemSellData("Sell11", 1000, 144)},
			};
			
			listDataLiftSpeed = new List<ItemUpdate>
			{
				new ItemUpdate(0, 1),
				new ItemUpdate(150, 0.95f),
				new ItemUpdate(1500, 0.9f),
				new ItemUpdate(4500, 0.85f),
				new ItemUpdate(13500, 0.8f),
				new ItemUpdate(40500, 0.75f),
				new ItemUpdate(121500, 0.7f),
				new ItemUpdate(364500, 0.65f),
				new ItemUpdate(1093500, 0.6f),
				new ItemUpdate(3280500, 0.55f),
				new ItemUpdate(9841500, 0.5f),
			};
			
			listDataAutoLiftSpeed = new List<ItemUpdate>
			{
				new ItemUpdate(0, 1),
				new ItemUpdate(150, 0.95f),
				new ItemUpdate(1500, 0.9f),
				new ItemUpdate(4500, 0.85f),
				new ItemUpdate(13500, 0.8f),
				new ItemUpdate(40500, 0.75f),
				new ItemUpdate(121500, 0.7f),
				new ItemUpdate(364500, 0.65f),
				new ItemUpdate(1093500, 0.6f),
				new ItemUpdate(3280500, 0.55f),
				new ItemUpdate(9841500, 0.5f),
			};
			
			listDataEarning = new List<ItemUpdate>
			{
				new ItemUpdate(0, 1),
				new ItemUpdate(150, 1.05f),
				new ItemUpdate(1500, 1.1f),
				new ItemUpdate(4500, 1.15f),
				new ItemUpdate(13500, 1.2f),
				new ItemUpdate(40500, 1.25f),
				new ItemUpdate(121500, 1.3f),
				new ItemUpdate(364500, 1.35f),
				new ItemUpdate(1093500, 1.4f),
				new ItemUpdate(3280500, 1.45f),
				new ItemUpdate(9841500, 1.5f),
			};
			
			valueLiftSpeed = listDataLiftSpeed[levelLiftSpeed].value;
			valueEarning = listDataEarning[levelEarning].value;
			valueAutoLiftSpeed = listDataAutoLiftSpeed[levelAutoLiftSpeed].value;
			
			if(!PlayerPrefs.HasKey("ItemSell"))
			{
				PlayerPrefs.SetString("ItemSell", "Sell1,");
			}

			var itemBought = PlayerPrefs.GetString("ItemSell").Split(",");

			foreach(var item in itemBought)
			{
				if(item == "")
				{
					continue;
				}
				
				var data = GetDataSell(item);
				
				listBought.Add(data);
			}
		}

		public ItemSellData GetDataSell(string id)
		{
			return _dicDataSell[id];
		}

		public void UpdateCoin(float value)
		{
			_currentCoin += (float)Math.Round(value, 2);
			MainUI.instance.txtCoinCurrent.text = $"{_currentCoin}";
			PlayerPrefs.SetFloat("earning", _currentCoin);
		}

		public void UpdateLiftSpeed()
		{
			levelLiftSpeed++;
			valueLiftSpeed = listDataLiftSpeed[levelLiftSpeed].value;
			PlayerPrefs.SetInt("level_LiftSpeed", levelLiftSpeed);
			MainCharacter.UpdateLiftSpeed(valueLiftSpeed);
			
			Debug.Log("Updated level lift speed: " + levelLiftSpeed);
		}
		
		public void UpdateEarning()
		{
			levelEarning++;
			valueEarning = listDataEarning[levelEarning].value;
			PlayerPrefs.SetInt("level_Earning", levelEarning);
			MainCharacter.UpdateEarning(valueEarning);
			
			Debug.Log("Updated level earning: " + levelEarning);
		}
		
		public void UpdateAutoLiftSpeed()
		{
			levelAutoLiftSpeed++;
			valueAutoLiftSpeed = listDataAutoLiftSpeed[levelAutoLiftSpeed].value;
			PlayerPrefs.SetInt("level_AutoLiftSpeed", levelAutoLiftSpeed);
			MainCharacter.UpdateAutoLiftSpeed(valueAutoLiftSpeed);
			
			Debug.Log("Updated level auto lift speed: " + levelAutoLiftSpeed);
		}

		public void Sell()
		{
			allCoin += _currentCoin;
			_currentCoin = 0;
			PlayerPrefs.SetFloat("earning", _currentCoin);
			MainCharacter.instance._head.DOScale(Vector3.one, 0.25f);
			PlayerPrefs.SetFloat("coin", allCoin);
		}

		public void BuyItem(ItemSellData data)
		{
			allCoin -= data.price;
			MainCharacter.itemEarning = data.value;
			PlayerPrefs.SetString("ItemSell", PlayerPrefs.GetString("ItemSell") + $"{data.id},");
			MainUI.instance.txtAllCoinCurrent.text = $"{allCoin}";
			PlayerPrefs.SetFloat("coin", allCoin);
		}
	}
}