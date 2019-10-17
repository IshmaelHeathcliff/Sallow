
using System.Collections.Generic;

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
        Data data = persistable.SaveData();
        string dataTag = persistable.DataTag;
        _persistentData.Add(dataTag, data);
    }

    public void LoadAllData()
    {
        foreach (IDataPersistable persistable in _dataPersistables)
        {
            string dataTag = persistable.DataTag;
            if (_persistentData.ContainsKey(dataTag))
            {
                persistable.LoadData(_persistentData[dataTag]);
            }
        }
    }

    public void ClearData()
    {
        _persistentData.Clear();
    }

    public void ClearPersistables()
    {
        _dataPersistables.Clear();
    }
}