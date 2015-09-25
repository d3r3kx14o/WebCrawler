﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model
{
    class BaiduPolicy : Policy
    {
        string  SearchEngineBase = "http://www.baidu.com/s?";
        Random  rand = new Random();
        int     RecordPerPage = 50;
        string  Policy.SearchEngine { get { return SearchEngineBase; } }
        string  Policy.QueryName { get { return "wd";  } }
        public  int MaxRecordPerQuery { get { return 1000; } }
        int     Policy.MaxPageCount { get { return (int) Math.Ceiling(MaxRecordPerQuery * 1.0 / RecordPerPage); } }
        int     Policy.GetPageRecord() { return RecordPerPage; }
        string  Policy.MiscQueryParas { get {
                return @"ie=utf-8"
                    + "&f=8"
                    + "&tn=baidulocal"
                    + "&rn=" + RecordPerPage
                    + "&rsv_spt=1"
                    + "&inputT=" + (rand.Next() % 200 + 400)
                    + "&cl=3"
                ;
            } }
        string  Policy.RecordSelector { get { return "//td[@class='f']//a"; } }
        string  Policy.ConvertDatetimeRange(DateTime date)
        {
            DateTime endDate = date.AddDays(1);
            return "gpc: stf=" + Util.ConvertToUnixTimestamp(date) + "," + (Util.ConvertToUnixTimestamp(endDate) - 1)
                +"|stftype=2";
        }

        string  Policy.StartFrom(int page)
        {
            return "pn=" + Math.Max(0, page) * RecordPerPage;
        }
    }
}