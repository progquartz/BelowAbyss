using System.Collections;
using System.Collections.Generic;

public static class QtzTool
{
    public static string TrimEnd(this string str, string trimStr)
    {
        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(trimStr)) return str;

        while (str.EndsWith(trimStr))
        {
            str = str.Remove(str.LastIndexOf(trimStr));
        }
        return str;
    }
}
