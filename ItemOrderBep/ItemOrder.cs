using System;
using System.Collections;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace ItemOrder
{
	public class ItemOrder : MonoBehaviour
	{
		private void Start()
		{
			this.sequenceAllowed = true;
			Debug.Log("Item Order Loaded");
		}
		private void Update()
		{
			LocalUser firstLocalUser = LocalUserManager.GetFirstLocalUser();
			bool flag = firstLocalUser == null;
			if (!flag)
			{
				bool flag2 = Input.GetKeyDown(111) && this.sequenceAllowed;
				if (flag2)
				{
					foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
					{
						string displayName = playerCharacterMasterController.GetDisplayName();
						bool alive = playerCharacterMasterController.master.alive;
						if (alive)
						{
							Array values = Enum.GetValues(typeof(ItemTier));
							for (int i = 0; i < values.Length; i++)
							{
								ItemTier itemTier = (ItemTier)values.GetValue(i);
								bool flag3 = itemTier != 4 && itemTier != 3 && itemTier != 5;
								if (flag3)
								{
									int amountOfItemsInTier = this.getAmountOfItemsInTier(itemTier, playerCharacterMasterController.master);
									ItemIndex mostItemInTier = this.getMostItemInTier(itemTier, playerCharacterMasterController.master);
									this.removeAllItemsInTier(itemTier, playerCharacterMasterController.master);
									this.addItems(mostItemInTier, amountOfItemsInTier, playerCharacterMasterController.master);
								}
							}
							bool flag4 = this.notis;
							if (flag4)
							{
								bool isClient = playerCharacterMasterController.master.isClient;
								if (isClient)
								{
									Chat.AddMessage("You have been sequenced [I O]");
								}
								else
								{
									Chat.AddMessage(displayName + " has been sequenced [I O]");
								}
							}
						}
					}
					base.StartCoroutine(this.WaitAndDo());
				}
				else
				{
					bool flag5 = Input.GetKeyDown(111) && !this.sequenceAllowed;
					if (flag5)
					{
						bool flag6 = this.notis;
						if (flag6)
						{
							Chat.AddMessage("Hold on there Junior.");
						}
					}
				}
			}
		}

		private IEnumerator WaitAndDo()
		{
			this.sequenceAllowed = false;
			yield return new WaitForSeconds((float)this.delay);
			bool flag = this.notis && this.delay != 0;
			if (flag)
			{
				Chat.AddMessage("You can now sequence again [I O]");
			}
			Debug.Log("You can now sequence again [I O]");
			this.sequenceAllowed = true;
			yield break;
		}

		private int getAmountOfItemsInTier(ItemTier tier, CharacterMaster localUser)
		{
			return localUser.inventory.GetTotalItemCountOfTier(tier);
		}


		private int getAmountOfItem(ItemIndex item, CharacterMaster localUser)
		{
			return localUser.inventory.GetItemCount(item);
		}


		private void addItems(ItemIndex item, int amount, CharacterMaster localUser)
		{
			localUser.inventory.GiveItem(item, amount);
		}

		private void removeAllItemsInTier(ItemTier tier, CharacterMaster localUser)
		{
			switch (tier)
			{
			case 0:
			{
				List<ItemIndex> tier1ItemList = ItemCatalog.tier1ItemList;
				for (int i = 0; i < tier1ItemList.Count; i++)
				{
					int itemCount = localUser.inventory.GetItemCount(tier1ItemList[i]);
					bool flag = itemCount > 0;
					if (flag)
					{
						localUser.inventory.RemoveItem(tier1ItemList[i], itemCount);
						Debug.Log(itemCount + " -- " + tier1ItemList[i]);
					}
				}
				break;
			}
			case 1:
			{
				List<ItemIndex> tier2ItemList = ItemCatalog.tier2ItemList;
				for (int j = 0; j < tier2ItemList.Count; j++)
				{
					int itemCount2 = localUser.inventory.GetItemCount(tier2ItemList[j]);
					bool flag2 = itemCount2 > 0;
					if (flag2)
					{
						localUser.inventory.RemoveItem(tier2ItemList[j], itemCount2);
						Debug.Log(itemCount2 + " -- " + tier2ItemList[j]);
					}
				}
				break;
			}
			case 2:
			{
				List<ItemIndex> tier3ItemList = ItemCatalog.tier3ItemList;
				for (int k = 0; k < tier3ItemList.Count; k++)
				{
					int itemCount3 = localUser.inventory.GetItemCount(tier3ItemList[k]);
					bool flag3 = itemCount3 > 0;
					if (flag3)
					{
						localUser.inventory.RemoveItem(tier3ItemList[k], itemCount3);
						Debug.Log(itemCount3 + " -- " + tier3ItemList[k]);
					}
				}
				break;
			}
			}
		}


		private ItemIndex getMostItemInTier(ItemTier tier, CharacterMaster localUser)
		{
			List<ItemIndex> itemAcquisitionOrder = localUser.inventory.itemAcquisitionOrder;
			ItemIndex result = -1;
			int num = 0;
			for (int i = 0; i < itemAcquisitionOrder.Count; i++)
			{
				switch (tier)
				{
				case 0:
				{
					bool flag = ItemCatalog.tier1ItemList.Contains(itemAcquisitionOrder[i]);
					if (flag)
					{
						int itemCount = localUser.inventory.GetItemCount(itemAcquisitionOrder[i]);
						bool flag2 = itemCount > num;
						if (flag2)
						{
							num = itemCount;
							result = itemAcquisitionOrder[i];
						}
					}
					break;
				}
				case 1:
				{
					bool flag3 = ItemCatalog.tier2ItemList.Contains(itemAcquisitionOrder[i]);
					if (flag3)
					{
						int itemCount2 = localUser.inventory.GetItemCount(itemAcquisitionOrder[i]);
						bool flag4 = itemCount2 > num;
						if (flag4)
						{
							num = itemCount2;
							result = itemAcquisitionOrder[i];
						}
					}
					break;
				}
				case 2:
				{
					bool flag5 = ItemCatalog.tier3ItemList.Contains(itemAcquisitionOrder[i]);
					if (flag5)
					{
						int itemCount3 = localUser.inventory.GetItemCount(itemAcquisitionOrder[i]);
						bool flag6 = itemCount3 > num;
						if (flag6)
						{
							num = itemCount3;
							result = itemAcquisitionOrder[i];
						}
					}
					break;
				}
				}
			}
			return result;
		}

		private bool sequenceAllowed;

		public bool notis = First_Mod_Test.NotisOn;

		public int delay = First_Mod_Test.Delaytime;
	}
}
