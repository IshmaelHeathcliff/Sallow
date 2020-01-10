
using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager
{
    static PersistentDataManager _instance;

    public static PersistentDataManager Instance => _instance ?? (_instance = new PersistentDataManager());

    PersistentDataManager(){}
 
    List<IDataPersistable> _dataPersistables = new List<IDataPersistable>();

    Dictionary<string, Data> _persistentData = new Dictionary<string, Data>();

    public void Register(IDataPersistable persistable)
    {
        _dataPersistables.Add(persistable);
    }

    public void Unregister(IDataPersistable persistable)
    {
        _dataPersistables.Remove(persistable);
    }

    public void SaveAllData()
    {
        foreach (IDataPersistable persistable in _dataPersistables)
        {
            SaveData(persistable);
        }
    }

    void SaveData(IDataPersistable persistable)
    {
        if (persistable.DataInfo.PersistenceType == DataInfo.DataPersistenceType.DoNotPersist) return;
        Data data = persistable.SaveData();
        string dataTag = persistable.DataInfo.DataTag;
        _persistentData[dataTag] = data;
    }

    public void LoadAllData()
    {
        foreach (IDataPersistable persistable in _dataPersistables)
        {
            if(persistable.DataInfo.PersistenceType == DataInfo.DataPersistenceType.DoNotPersist) continue;
            string dataTag = persistable.DataInfo.DataTag;
            if (_persistentData.ContainsKey(dataTag))
            {
                persistable.LoadData(_persistentData[dataTag]);
            }
        }
    }

    public void ClearPersistables()
    {
        _dataPersistables.Clear();
    }
}