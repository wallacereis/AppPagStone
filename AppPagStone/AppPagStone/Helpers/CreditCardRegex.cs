using System;
using System.Text.RegularExpressions;

namespace AppPagStone.Helpers
{
    public class CreditCardRegex
    {
        private const string REGEX_VISA = @"^4[0-9]{12}(?:[0-9]{3})";
        private const string REGEX_MASTER = @"^5[1-5][0-9]{14}";
        private const string REGEX_AMEX = @"^3[47][0-9]{13}";
        private const string REGEX_DINERS = @"^3(?:0[0-5]|[68][0-9])[0-9]{11}";
        private const string REGEX_JCB = @"^(?:2131|1800|35\d{3})\d{11}";
        private const string REGEX_DISCOVER = @"^6(?:011|5[0-9]{2})[0-9]{12}";
        private const string REGEX_ELO = @"/^((((636368)|(438935)|(504175)|(451416)|(636297))\d{0,10})|((5067)|(4576)|(4011))\d{0,12})$/";
        private const string REGEX_HIPERCARD = @"/^(606282\d{10}(\d{3})?)|(3841\d{15})$/";
        private const string REGEX_AURA = @"'/^(5078\d{2})(\d{2})(\d{11})$/'";

        public bool IsVisa(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_VISA, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsMaster(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_MASTER, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsAmex(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_AMEX, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsDiners(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_DINERS, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsJcb(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_JCB, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsDiscover(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_DISCOVER, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsElo(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_ELO, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsHipercard(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_HIPERCARD, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsAura(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, REGEX_AURA, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsNumeric(string strIn)
        {
            try
            {
                return Regex.IsMatch(strIn, @"^[0-9]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
