
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace Project.Util
{
    public class Util
    {

        public static string CreateMD5(string input)
        {
            //bam du lieu
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static int DayInDate(int month, int year)
        {

            if (month >= 1 && month <= 12)
            {
                switch (month)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12: return 31; 
                    case 4:
                    case 6:
                    case 9:
                    case 11: return 30; 

                    case 2:
                        if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                            return 29;
                        else
                            return 28;
                        
                }
            }
            return 0;

        }

        
    }
}