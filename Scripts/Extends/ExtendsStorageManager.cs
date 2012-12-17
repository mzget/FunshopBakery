using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendsStorageManager : Mz_StorageManage
{	
	public void LoadCanSellGoodsListData ()
	{		
		int[] array = PlayerPrefsX.GetIntArray(Mz_StorageManage.SaveSlot + "cansellgoodslist");
        BakeryShop.NumberOfCansellItem.Clear();
		foreach (var item in array) {
			BakeryShop.NumberOfCansellItem.Add(item);
		}

		string[] arr_availabelCreams = PlayerPrefsX.GetStringArray(Mz_StorageManage.SaveSlot + KEY_AVAILABLE_CREAM);
		for (int i = 0; i < 3; i++) {
			CreamBeh.arr_CreamBehs[i] = string.Empty;
		}
		for (int i = 0; i < arr_availabelCreams.Length; i++) {
			CreamBeh.arr_CreamBehs[i] = arr_availabelCreams[i];	
		}
	}

    public void SaveCanSellGoodListData() {		
		int[] array_temp = BakeryShop.NumberOfCansellItem.ToArray();
		PlayerPrefsX.SetIntArray(Mz_StorageManage.SaveSlot + "cansellgoodslist", array_temp);        

		PlayerPrefsX.SetStringArray(Mz_StorageManage.SaveSlot + KEY_AVAILABLE_CREAM, CreamBeh.arr_CreamBehs);
    }

	public override void LoadSaveDataToGameStorage()
	{
		base.LoadSaveDataToGameStorage ();
				
		Mz_StorageManage.Username = PlayerPrefs.GetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_USERNAME);
		Mz_StorageManage.ShopName = PlayerPrefs.GetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_NAME);
		Mz_StorageManage.AvailableMoney = PlayerPrefs.GetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_MONEY);
		Mz_StorageManage.AccountBalance = PlayerPrefs.GetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ACCOUNTBALANCE);
		Mz_StorageManage.ShopLogo = PlayerPrefs.GetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO);
		Mz_StorageManage.ShopLogoColor = PlayerPrefs.GetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO_COLOR);
				
		Mz_StorageManage.Roof_id = PlayerPrefs.GetInt(SaveSlot + KEY_ROOF_ID);
		Mz_StorageManage.Awning_id = PlayerPrefs.GetInt(SaveSlot + KEY_AWNING_ID);
		Mz_StorageManage.Table_id = PlayerPrefs.GetInt(SaveSlot + KEY_TABLE_ID);
		Mz_StorageManage.Accessory_id = PlayerPrefs.GetInt(SaveSlot + KEY_ACCESSORY_ID);
		
		Mz_StorageManage.TK_clothe_id = PlayerPrefs.GetInt(SaveSlot + KEY_TK_CLOTHE_ID);
		Mz_StorageManage.TK_hat_id = PlayerPrefs.GetInt(SaveSlot + KEY_TK_HAT_ID);
		
		//@!-- Load Donation data.
		ConservationAnimals.Level = PlayerPrefs.GetInt(SaveSlot + KEY_CONSERVATION_ANIMAL_LV, 0);
		AIDSFoundation.Level = PlayerPrefs.GetInt(SaveSlot + KEY_AIDSFOUNDATION_LV, 0);
		LoveDogConsortium.Level = PlayerPrefs.GetInt(SaveSlot + KEY_LOVEDOGFOUNDATION_LV, 0);
		LoveKidsFoundation.Level = PlayerPrefs.GetInt(SaveSlot + KEY_LOVEKIDFOUNDATION_LV, 0);
		EcoFoundation.Level = PlayerPrefs.GetInt(SaveSlot + KEY_ECOFOUNDATION_LV, 0);
        GlobalWarmingOranization.Level = PlayerPrefs.GetInt(SaveSlot + KEY_GLOBALWARMING_LV, 0);

        this.LoadCanSellGoodsListData();
	}

    public override void SaveDataToPermanentMemory()
    {
        base.SaveDataToPermanentMemory();

        PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_USERNAME, Mz_StorageManage.Username);
        PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_NAME, Mz_StorageManage.ShopName);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_MONEY, Mz_StorageManage.AvailableMoney);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ACCOUNTBALANCE, Mz_StorageManage.AccountBalance);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO, Mz_StorageManage.ShopLogo);
		PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO_COLOR, Mz_StorageManage.ShopLogoColor);

		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ROOF_ID, Mz_StorageManage.Roof_id);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_AWNING_ID, Mz_StorageManage.Awning_id);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_TABLE_ID, Mz_StorageManage.Table_id);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ACCESSORY_ID, Mz_StorageManage.Accessory_id);

        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_TK_CLOTHE_ID, Mz_StorageManage.TK_clothe_id);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_TK_HAT_ID, Mz_StorageManage.TK_hat_id);

        //@!-- Donation data.
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_CONSERVATION_ANIMAL_LV, ConservationAnimals.Level);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_AIDSFOUNDATION_LV, AIDSFoundation.Level);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_LOVEDOGFOUNDATION_LV, LoveDogConsortium.Level);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_LOVEKIDFOUNDATION_LV, LoveKidsFoundation.Level);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ECOFOUNDATION_LV, EcoFoundation.Level);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_GLOBALWARMING_LV, GlobalWarmingOranization.Level);

		if(BakeryShop.NumberOfCansellItem.Count != 0)
			this.SaveCanSellGoodListData();
		
		PlayerPrefs.Save();
    }
}

