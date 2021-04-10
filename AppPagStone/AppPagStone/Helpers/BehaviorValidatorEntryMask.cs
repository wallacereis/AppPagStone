using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AppPagStone.Helpers
{
    public class BehaviorValidatorEntryMask : Behavior<Entry>
    {
        public int MaxLength { get; set; }
        public bool Formatado { get; set; }
        public string Tipo { get; set; }
        internal const int LENGTH_DATE_CARTAO_CREDITO = 6;
        internal const int LENGTH_DECIMAL = 2;

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            var entryText = e.NewTextValue;
            var entryLength = entryText.Length;

            switch (Tipo)
            {
                case "DataValidadeCartaoCredito":
                    if (entryLength == LENGTH_DATE_CARTAO_CREDITO && !Formatado)
                    {
                        entryText = Convert.ToUInt64(entryText).ToString(@"00/0000");
                        Formatado = true;
                    }
                    else if (entryText.Length > MaxLength)
                    {
                        entryText = entryText.Remove(entryText.Length - 1);
                    }
                    else if (entryText.Length < MaxLength && Formatado)
                    {
                        entryText = entryText.RemoveNonNumbers();
                        Formatado = false;
                    }

                    entry.Text = entryText;
                    entry.TextColor = entry.Text.Length < MaxLength ? Color.Red : Color.Black;

                    break;

                case "Decimal":

                    if (entryText != e.OldTextValue)
                    {
                        string strNumber = entryText.RemoveNonNumbers();

                        if (strNumber.Length > LENGTH_DECIMAL)
                        {
                            var pos = strNumber.Length - LENGTH_DECIMAL;
                            entryText = strNumber.Insert(pos, ",");
                            entry.Text = string.Format("{0:N2}", Convert.ToDecimal(entryText));
                        }
                    }

                    break;

                case "Numeric":

                    if (MaxLength > 0)
                    {
                        if (entryLength > MaxLength)
                        {
                            entryText = entryText.Remove(entryText.Length - 1);
                            entry.Text = entryText;
                        }
                        else
                            entry.Text = entryText.RemoveNonNumbers();
                    }

                    break;

                case "TitleCase":

                    if (MaxLength > 0)
                    {
                        if (entryLength > MaxLength)
                        {
                            entryText = entryText.Remove(entryText.Length - 1);   
                        }
                    }

                    entry.Text = Regex.Replace(entryText.ToLower(), @"(?<=\b(?:mc|mac)?)[a-zA-Z]", m => m.Value.ToUpper());

                    break;

                default:

                    if (MaxLength > 0)
                    {
                        if (entryLength > MaxLength)
                        {
                            entryText = entryText.Remove(entryText.Length - 1);
                            entry.Text = entryText;
                        }

                    }

                    break;
            }
        }
    }
}
