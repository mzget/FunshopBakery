using UnityEngine;
using System.Collections;

public class StorageManage
{
    #region Standard storage game data.

    //<!-- Save Game Slot.
    private static int saveSlot = 0;
    public static int SaveSlot
    {
        get { return saveSlot; }
        set { saveSlot = value; }
    }

    //<!-- User Name.
    private static string username = string.Empty;
    public static string Username {
        get { return username; }
        set { username = value; }
    }

    #endregion
    
    public static string ShopName;
    public static int Money = 500;
}

public class Mz_StorageData
{
    public static void LoadSaveDataToGameStorage()
    {
        StorageManage.Username = PlayerPrefs.GetString(StorageManage.SaveSlot + "username");
        StorageManage.ShopName = PlayerPrefs.GetString(StorageManage.SaveSlot + "shopname");
        StorageManage.Money = PlayerPrefs.GetInt(StorageManage.SaveSlot + "money");

        Debug.Log("Load storage data to static variable complete.");
    }

    public static void Save() {
        PlayerPrefs.SetString(StorageManage.SaveSlot + "username", StorageManage.Username);
        PlayerPrefs.SetString(StorageManage.SaveSlot + "shopname", StorageManage.ShopName);
        PlayerPrefs.SetInt(StorageManage.SaveSlot + "money", StorageManage.Money);
    }
}
