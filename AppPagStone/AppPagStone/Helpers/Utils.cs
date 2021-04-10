using AppPagStone.Stone.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPagStone.Helpers
{
    public class Utils
    {
        //-----
        //VENDA
        //-----
        internal const string STONE_BASE_URL = "https://transaction.stone.com.br";
        internal const string STONE_MERCHANT_KEY = "f2a1f485-cfd4-49f5-8862-0ebc438ae923";
        //-----
        //TOKEN
        //-----
        internal const string STONE_TOKEN_URL = "http://split-dev.cloudapp.net:17001/token";
        internal const string STONE_TOKEN_HOM = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlaWQiOjE1LCJldHAiOiJtZXJjaGFudCIsImlhdCI6IjUwOTY1MzUwMDYwMCIsImlzcyI6IlNwbGl0IiwicGlkIjowfQ.ys4kd7Lf3LU7KoA24CJw4O3hRwC_UEQUPc07Q4xsvag";
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlaWQiOjE1LCJldHAiOiJtZXJjaGFudCIsImlhdCI6IjEyNzc5MjAwMzkiLCJpc3MiOiJTcGxpdCIsInBpZCI6MH0._n2lcprGyIP6M34fU7itXfv5E3740nsnALBsVprzE9U
        //-----
        //SPLIT
        //-----
        internal const string STONE_SPLIT_POST_URL = "http://split-dev.cloudapp.net:17002/splits";
        internal const string STONE_SPLIT_GET_URL = "http://split-dev.cloudapp.net:17002/splits/";
        internal const string STONE_SPLIT_DELETE_URL = "http://split-dev.cloudapp.net:17002/splits/";
        internal const string STONE_SPLIT_MERCHANT_KEY = "dba86490-4ae6-4044-ab1f-fce66881a711";
        internal const string STONE_SPLIT_MERCHANT_SECRET_KEY = "MmMxMDYxN2QtNWY1MC00ODI1LTg2MTQtODBjYWUzY2E4OWIx";
        //---------
        //RECIPIENT
        //---------
        internal const string STONE_CREATE_RECIPIENT_URL = "http://split-dev.cloudapp.net:17003/v1/recipient";

        internal const string CONTENT_TYPE = "application/json";
    }
}
