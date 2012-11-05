using UnityEngine;
using System.Collections;

public class Mz_StorageManage
{
    #region Standard storage game data.

    //<!-- Save Game Slot.
    public static int SaveSlot = 0;
    //<!-- User Name.
    public static string Username = "";

    #endregion
    
    public static string ShopName;
    public static int AvailableMoney = 500;
    public static int InBankMoney = 500;
	public static int ShopLogo = 0;
	public static string ShopLogoColor = "Blue";

	public static int Roof_id = 255;
	public static int Awning_id = 255;
	public static int Table_id = 0;
	public static int Accessory_id = 0;

	/// <summary>
	/// Storage data key.
	/// </summary>
	public const string KEY_USERNAME = "USERNAME";
	public const string KEY_SHOP_NAME = "SHOP_NAME";
	public const string KEY_MONEY = "MONEY";
	public const string KEY_SHOP_LOGO = "SHOP_LOGO";
	public const string KEY_SHOP_LOGO_COLOR = "SHOP_LOGO_COLOR";

	public const string KEY_ROOF_ID = "ROOF_ID";
	public const string KEY_AWNING_ID = "AWNING_ID";
	public const string KEY_TABLE_ID = "TABLE_ID";
	public const string KEY_ACCESSORY_ID = "ACCESSORY_ID";

    
    public static void LoadSaveDataToGameStorage()
    {
        Mz_StorageManage.Username = PlayerPrefs.GetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_USERNAME);
        Mz_StorageManage.ShopName = PlayerPrefs.GetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_NAME);
        Mz_StorageManage.AvailableMoney = PlayerPrefs.GetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_MONEY);
		Mz_StorageManage.ShopLogo = PlayerPrefs.GetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO);
		Mz_StorageManage.ShopLogoColor = PlayerPrefs.GetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO_COLOR);
		
		int[] array = PlayerPrefsX.GetIntArray(Mz_StorageManage.SaveSlot + "cansellgoodslist");
		foreach (var item in array) {
            BakeryShop.NumberOfCansellItem.Add(item);
		}

        Mz_StorageManage.Roof_id = PlayerPrefs.GetInt(SaveSlot + KEY_ROOF_ID);
        Mz_StorageManage.Awning_id = PlayerPrefs.GetInt(SaveSlot + KEY_AWNING_ID);
        Mz_StorageManage.Table_id = PlayerPrefs.GetInt(SaveSlot + KEY_TABLE_ID);
        Mz_StorageManage.Accessory_id = PlayerPrefs.GetInt(SaveSlot + KEY_ACCESSORY_ID);

        Debug.Log("Load storage data to static variable complete.");
    }

    public static void Save() 
    {
        PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_USERNAME, Mz_StorageManage.Username);
        PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_NAME, Mz_StorageManage.ShopName);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_MONEY, Mz_StorageManage.AvailableMoney);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO, Mz_StorageManage.ShopLogo);
		PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO_COLOR, Mz_StorageManage.ShopLogoColor);
		
		int[] array_temp = BakeryShop.NumberOfCansellItem.ToArray();
		PlayerPrefsX.SetIntArray(Mz_StorageManage.SaveSlot + "cansellgoodslist", array_temp);

		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ROOF_ID, Mz_StorageManage.Roof_id);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_AWNING_ID, Mz_StorageManage.Awning_id);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_TABLE_ID, Mz_StorageManage.Table_id);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ACCESSORY_ID, Mz_StorageManage.Accessory_id);
		
		PlayerPrefs.Save();
    }
}
