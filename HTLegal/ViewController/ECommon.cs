using HTLegal.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTLegal.ViewController
{
    public class ECommon
    {


        #region enum objects



        #endregion

        #region cac ham ve ngay thang

        /// <summary>
        /// Ham tra ve ngay dau, cuoi thang theo ngay bat ky
        /// </summary>
        /// <param name="currentDate">Ngay bat ky trong thang</param>
        /// <param name="BeginDateOfMonth">Tra ve ngay dau tien cua thang</param>
        /// <param name="EndDateOfMonth">Tra ve ngay cuoi cung cua thang</param>
        public static void GetBeginEndDateOfMonth(DateTime currentDate, out DateTime BeginDateOfMonth, out DateTime EndDateOfMonth)
        {
            DateTime beginDate;
            DateTime endDate;
            string bDate = currentDate.Month.ToString() + "/1/" + currentDate.Year.ToString() + " 0:0:0";
            string eDate = currentDate.Month.ToString() + "/" + DateTime.DaysInMonth(currentDate.Year, currentDate.Month).ToString() + "/" + currentDate.Year.ToString() + " 23:59:59";
            DateTime.TryParse(bDate, out beginDate);
            DateTime.TryParse(eDate, out endDate);
            BeginDateOfMonth = beginDate;
            EndDateOfMonth = endDate;
        }

        /// <summary>
        /// Ham tra ve ngay dau tien(Monday) va ngay cuoi tuan(Sunday)
        /// </summary>
        /// <param name="currentDate">Ngay bat ky trong tuan</param>
        /// <param name="beginWeek">Tra ve ngay dau tien</param>
        /// <param name="endWeek">Tra ve ngay cuoi tuan</param>
        public static void GetBeginEndWeek(DateTime currentDate, out DateTime beginWeek, out DateTime endWeek)
        {

            switch (currentDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    beginWeek = currentDate.AddDays(-4);
                    endWeek = currentDate.AddDays(2);
                    break;
                case DayOfWeek.Monday:
                    beginWeek = currentDate;
                    endWeek = currentDate.AddDays(6);
                    break;
                case DayOfWeek.Saturday:
                    beginWeek = currentDate.AddDays(-5);
                    endWeek = currentDate.AddDays(1);
                    break;
                case DayOfWeek.Sunday:
                    beginWeek = currentDate.AddDays(-6);
                    endWeek = currentDate;
                    break;
                case DayOfWeek.Thursday:
                    beginWeek = currentDate.AddDays(-3);
                    endWeek = currentDate.AddDays(3);
                    break;
                case DayOfWeek.Tuesday:
                    beginWeek = currentDate.AddDays(-1);
                    endWeek = currentDate.AddDays(5);
                    break;
                case DayOfWeek.Wednesday:
                    beginWeek = currentDate.AddDays(-2);
                    endWeek = currentDate.AddDays(4);
                    break;
                default:
                    beginWeek = currentDate;
                    endWeek = currentDate;
                    break;
            }
        }

        /// <summary>
        /// ham convert tu dd/mm/yyyy dang MM/dd/yyyy
        /// </summary>
        /// <param name="dateVi">string dd/MM/yyyy</param>
        /// <returns>MM/dd/yyyy</returns>
        public static DateTime? ConvertDateViToDateEn(string dateVi)
        {
            try
            {
                string[] _dateStrVi = dateVi.Split(new char[] { '/' });
                string _dateStrEn = _dateStrVi[1] + "/" + _dateStrVi[0] + "/" + _dateStrVi[2];
                DateTime _date;
                if (DateTime.TryParse(_dateStrEn, out _date) == true)
                {
                    return _date;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        #endregion


        /// <summary>
        /// remove cac ky tu unicode & dac biet
        /// </summary>
        /// <param name="inputS"></param>
        /// <returns></returns>
        public static string RemoveUnicodeStringAndSymbol(string inputS)
        {
            string output = "";
            if (string.IsNullOrWhiteSpace(inputS) == false)
            {
                //remove unicode
                string outputS = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(inputS));
                output = outputS.Replace("?", "");
                //remove symbol
                output = output.Replace(",", "").Replace(":", "").Replace("'", "").Replace(";", "").Replace("&", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("~", "").Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace("^", "").Replace("~", "").Replace(".","").Replace("-","");

            }
            return output;
        }

        /// <summary>
        /// tinh thong so page va recordsperpage de phan trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rpp"></param>
        /// <param name="totalRecords"></param>
        /// <param name="_page"></param>
        /// <param name="_rpp"></param>
        public static void PagedList(int? page, int? rpp, int totalRecords, out int _page, out int _rpp, out int skip, out int take)
        {
            _page = page??1;
            _rpp = rpp ?? 15;
            //check page
            if (_page > totalRecords/_rpp && totalRecords % _rpp == 0)
            {
                _page = totalRecords/_rpp;
              
            }
            else if (_page > totalRecords / _rpp + 1)
            {
                _page = totalRecords / _rpp + 1;
            }
            
            if (_page < 1)
            {
                _page = 1;
            }

            //check take
            if (_rpp * _page > totalRecords)
            {
                take = totalRecords - (_rpp * (_page - 1));
            }
            else
            {
                take = _rpp;
            }
            skip = (_page - 1) * _rpp;

            return;
        }

   
        /// <summary>
        /// date remain
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateTimeRemain(DateTime dt)
        {
            TimeSpan ts = (DateTime.Now - dt);
            if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes == 0)
            {
                return "a few seconds ago";
            }
            else if (ts.Days == 0 && ts.Hours == 0)
            {
                return ts.Minutes + " minutes ago";
            }
            else if (ts.Days == 0)
            {
                return ts.Hours + " hours " + ts.Minutes + " minutes ago";
            }
            else if (ts.Days > 365)
            {
                return Math.Round((decimal)(ts.Days / 365), 0, MidpointRounding.ToEven).ToString() + " years ago";
            }
            else if (ts.Days > 30)
            {
                return Math.Round((decimal)(ts.Days / 30), 0, MidpointRounding.ToEven).ToString() + " months ago";
            }
            return ts.Days + " days ago";// +ts.Hours + " hours " + ts.Minutes + " minutes ago";

        }


        /// <summary>
        /// auto get username from firstname and lastname:firstname-lastname
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static string GetUserNameByFistLastName(string firstName, string lastName)
        {
            string resultfn = "";
            string resultln = "";
            string[] fn = firstName.Split(new char[] {' '});
            string[] ln = lastName.Split(new char[] {' ' });
            foreach (var item in fn)
            {
                if (string.IsNullOrWhiteSpace(item) == false)
                {
                    resultfn += item;
                }
            }
            foreach (var item in ln)
            {
                if (string.IsNullOrWhiteSpace(item) == false)
                {
                    resultln +=  item;
                }
            }
            if (string.IsNullOrWhiteSpace(resultln) == false)
            {
                return RemoveUnicodeStringAndSymbol(resultfn) + "-" + RemoveUnicodeStringAndSymbol(resultln);
            }
            return  RemoveUnicodeStringAndSymbol(resultfn);
        }

        public static bool IsValidEmail(string emailAddress)
        {
            try {
                return Regex.IsMatch(emailAddress, 
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + 
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,24}))$", 
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
           }  
           catch (RegexMatchTimeoutException) {
              return false;
           }
        }

        public static string GetIDStr(int id)
        {
            string result = "";
            if (id < 10)
            {
                result = "000" + id.ToString();

            }
            else if (id < 100)
            {
                result = "00" + id.ToString();
                
            }
            else if (id < 1000)
            {
                result = "0" + id.ToString();
            }
            else
            {
                result = id.ToString();
            }

            return result;
        }

        public static string GetCardTypeByCardNumber(string cardNumber)
        {
            // 'American Express       34, 37            15
            //'Discover               6011              16
            //'Master Card            51 to 55          16
            //'Visa                   4                 13, 16

            //visa 
            Regex checkVisa = new Regex("^(?:4[0-9]{12}(?:[0-9]{3})?)$");
            if (checkVisa.IsMatch(cardNumber) == true)
            {
                return "visa";
            }
            //amex
             Regex checkAmex = new Regex("^(3[47][0-9]{13})$");
             if (checkAmex.IsMatch(cardNumber) == true)
             {
                 return "amex";
             }
            //discover
             Regex checkDiscover = new Regex("^(6011[0-9]{12})$");
             if (checkDiscover.IsMatch(cardNumber))
             {
                 return "discover";
             }
            //mc
            Regex checkMC = new Regex("^((?:51|55)[0-9]{14})$");
            if (checkMC.IsMatch(cardNumber))
            {
                return "mastercard";
            }

            return "Other";
        }


        /// <summary>
        /// save image from string 64bit
        /// </summary>
        /// <param name="stringImage"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Boolean SaveBinaryImage(string stringImage, string filename)
        {

            var appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string path = Path.Combine(appPath, @"Upload\Contract\MemberSignature\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(stringImage)))
                {
                    using (System.Drawing.Bitmap bm2 = new System.Drawing.Bitmap(ms))
                    {
                        bm2.Save(path + filename);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static E_WebsiteConfiguration GetWebSiteInfo()
        {
            HTLegalContext db = new HTLegalContext();
            var webInfo = (from w in db.E_WebsiteConfiguration
                     select w).FirstOrDefault();

            return webInfo; 
        }



    }
}