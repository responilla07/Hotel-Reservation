using System;
using System.Data;
using System.Configuration;
using System.Web;


/// <summary>
/// Summary description for Class1
/// </summary>
public class transacNo
{

    public static string newTransacNo(int length)
    {
        //+-!@#$%^&()_+~`-=[];'.,
        char[] chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        string str = string.Empty;
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            int x = random.Next(1, chars.Length);
            //For avoiding Repetation of Characters
            if (!str.Contains(chars.GetValue(x).ToString()))
                str += chars.GetValue(x);
            else
                i = i - 1;
        }
        return str;
    }

}

