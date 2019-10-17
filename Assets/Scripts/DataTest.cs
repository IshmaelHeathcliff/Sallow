
using System.Runtime.CompilerServices;

public class DataTest
{
    static DataTest instance;

    public static DataTest Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataTest();
            }

            return instance;
        }
    }

    public int X = 1;
}