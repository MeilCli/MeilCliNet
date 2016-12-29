using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site {
    public static class DateHelper {

        public static DateTime FromFileName(string fileName) {
            string[] ar = fileName.Split('-');
            if(ar.Length < 4) {
                return DateTime.MinValue;
            }
            int year;
            int month;
            int day;
            if(int.TryParse(ar[0],out year) && int.TryParse(ar[1],out month) && int.TryParse(ar[2],out day)) {
                return new DateTime(year,month,day);
            }
            return DateTime.MinValue;
        }
    }
}