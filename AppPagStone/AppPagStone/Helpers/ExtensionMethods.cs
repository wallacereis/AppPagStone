using AppPagStone.Stone.EnumTypes;
using System;
using System.Text.RegularExpressions;

namespace AppPagStone.Helpers
{
    public static class ExtensionMethods
    {
        public static string DefaultString(this string value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return string.Empty;
            }
            return Convert.ToString(value).Trim();
        }

        public static DateTime DefaultDateTime(this Object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return new DateTime(1900, 1, 1);
            }
            return Convert.ToDateTime(value);
        }

        public static Int32 DefaultInt(this Object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return 0;
            }
            return Convert.ToInt32(value);
        }

        public static float DefaulFloat(this Object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return 0;
            }
            return (float)Convert.ToDouble(value);
        }

        public static string IdentificarBandeira(this string cartao)
        {
            var regEx = new CreditCardRegex();

            if (regEx.IsVisa(cartao)) return "VISA";
            else if (regEx.IsMaster(cartao)) return "MASTERCARD";
            else if (regEx.IsAmex(cartao)) return "AMEX";
            else if (regEx.IsDiners(cartao)) return "DINERS";
            else if (regEx.IsJcb(cartao)) return "JCB";
            else if (regEx.IsDiscover(cartao)) return "DISCOVER";
            else if (regEx.IsElo(cartao)) return "ELO";
            else if (regEx.IsHipercard(cartao)) return "HIPERCARD";
            else if (regEx.IsAura(cartao)) return "AURA";
            else return "Não Identificada";
        }

        public static string RemoveNonNumbers(this string texto)
        {
            var regex = new Regex(@"[^\d]");
            return regex.Replace(texto, "");
        }

        public static CreditCardBrandEnum ValidarBandeiraCartao(this string numCartao)
        {
            var bandeira = numCartao.IdentificarBandeira().ToLower();

            switch (bandeira)
            {
                case "aura": return CreditCardBrandEnum.Aura;
                case "amex": return CreditCardBrandEnum.Amex;
                case "diners": return CreditCardBrandEnum.Diners;
                case "discover": return CreditCardBrandEnum.Discover;
                case "elo": return CreditCardBrandEnum.Elo;
                case "hipercard": return CreditCardBrandEnum.Hipercard;
                case "mastercard": return CreditCardBrandEnum.Mastercard;
                case "visa": return CreditCardBrandEnum.Visa;
                default: return CreditCardBrandEnum.NaoIdentificada;
            }
        }
    }
}
