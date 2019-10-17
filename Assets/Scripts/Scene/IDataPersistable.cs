
public interface IDataPersistable
{
    void LoadData(Data data);

    Data SaveData();

    string DataTag { get; set; }
}

public abstract class Data
{
}

public class Data<T> : Data
{
    public T Data1 { get; private set; }

    public Data(T data)
    {
        Data1 = data;
    }
}

public class Data<T1, T2> : Data
{
    public T1 Data1 { get; private set; }
    public T2 Data2 { get; private set; }

    public Data(T1 data1, T2 data2)
    {
        Data1 = data1;
        Data2 = data2;
    }
}

public class Data<T1, T2, T3> : Data
{
    public T1 Data1 { get; private set; }
    public T2 Data2 { get; private set; }
    public T3 Data3 { get; private set; }

    public Data(T1 data1, T2 data2, T3 data3)
    {
        Data1 = data1;
        Data2 = data2;
        Data3 = data3;
    }
}

public class Data<T1, T2, T3, T4> : Data
{
    public T1 Data1 { get; private set; }
    public T2 Data2 { get; private set; }
    public T3 Data3 { get; private set; }
    public T4 Data4 { get; private set; }

    public Data(T1 data1, T2 data2, T3 data3, T4 data4)
    {
        Data1 = data1;
        Data2 = data2;
        Data3 = data3;
        Data4 = data4;
    }
}

public class Data<T1, T2, T3, T4, T5> : Data
{
    public T1 Data1 { get; private set; }
    public T2 Data2 { get; private set; }
    public T3 Data3 { get; private set; }

    public T4 Data4 { get; private set; }
    public T5 Data5 { get; private set; }

    public Data(T1 data1, T2 data2, T3 data3, T4 data4, T5 data5)
    {
        Data1 = data1;
        Data2 = data2;
        Data3 = data3;
        Data4 = data4;
        Data5 = data5;
    }
}

    
