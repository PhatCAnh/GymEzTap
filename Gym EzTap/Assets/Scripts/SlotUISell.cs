using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace DefaultNamespace
{
	public class SlotUISell : MonoBehaviour
	{
		[SerializeField] private Button _btn;
		
		[SerializeField] private string _id;

		[SerializeField] private TextMeshProUGUI _txtCoin, _txtValue;

		private ItemSellData _data;

		private void Start()
		{
			_data = GameController.instance.GetDataSell(_id);
			
			if(GameController.instance.listBought.Contains(_data))
			{
				gameObject.SetActive(false);
				return;
			}
			
			_btn.onClick.AddListener(ClickedButton);

			_txtCoin.text = $"{_data.price}";

			_txtValue.text = $"{_data.value}";
		}

		private void ClickedButton()
		{
			if(GameController.instance.allCoin >= _data.price)
			{
				GameController.instance.BuyItem(_data);
				gameObject.SetActive(false);
			}
			else
			{
				MainUI.instance.CallPopupNotEnough();
			}
		}
	}
}