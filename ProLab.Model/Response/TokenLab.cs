using System;
using System.Collections.Generic;
using System.Text;

namespace ProLab.Model.Response
{
    public class TokenLab
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_in { get; set; }
        public string Refresh_Token { get; set; }
    }
}
