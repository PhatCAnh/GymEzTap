namespace DefaultNamespace
{
	public class ItemSellData
	{
		public string id;
		public int price;
		public int value;

		public ItemSellData(string id, int price, int value)
		{
			this.id = id;
			this.price = price;
			this.value = value;
		}
	}
}