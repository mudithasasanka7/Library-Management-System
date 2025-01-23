using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Linq;
// using System.Web.Security;
// using System.Web.UI;
// using System.Web.UI.WebControls;
// using System.Web.UI.WebControls.WebParts;
// using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Class1
/// </summary>

public class Class1
{
    public static string GetRandomPassword(int length)
    {
        char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        string password = string.Empty;
        Random random = new Random();

        while (password.Length < length)
        {
            int x = random.Next(0, chars.Length);
            char newChar = chars[x];

            // Avoid duplicate characters in the password
            if (!password.Contains(newChar))
            {
                password += newChar;
            }
        }

        return password;
    }

}
