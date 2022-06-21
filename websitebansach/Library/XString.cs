using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace websitebansach.Library
{
    public class XString
    {
        public static string Str_Slug(string s)
        {
            String[][] symbols = {
                                new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]","a"},
                                new String[] { "[đ]","d"},
                                new String[] { "[éèẻẽẹêếềểễệ]","e"},
                                new String[] { "[íìỉĩị]","i"},
                                new String[] { "[óòỏõọôốồổỗộơớờởỡợ]","o"},
                                new String[] { "[úùủũụưứừữựử]","u"},
                                new String[] { "[ýỳỷỹỵ]","y"},
                                new String[] { "[\\s'\";,]", "-"},
                               };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
    }
}