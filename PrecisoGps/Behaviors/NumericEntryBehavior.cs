using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PrecisoGps.Behaviors
{
    public class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                return;
            }

            // Eliminar cualquier carácter que no sea un número
            bool isValid = Regex.IsMatch(e.NewTextValue, @"^[0-9]+$");

            if (!isValid)
            {
                ((Entry)sender).Text = Regex.Replace(e.NewTextValue, @"[^\d]", "");
            }
        }
    }

    public class HoraEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                return;
            }

            var entry = (Entry)sender;
            string text = e.NewTextValue;

            // Permitir sólo números y dos puntos (para formato HH:MM)
            if (!Regex.IsMatch(text, @"^[0-9:]+$"))
            {
                entry.Text = Regex.Replace(text, @"[^0-9:]", "");
                return;
            }

            // Formatear automáticamente como HH:MM
            if (text.Length == 2 && !text.Contains(":") && !e.OldTextValue.Contains(":"))
            {
                entry.Text = text + ":";
                return;
            }

            // Asegurar que solo haya un caracter ":"
            int colonCount = 0;
            foreach (char c in text)
            {
                if (c == ':')
                    colonCount++;
            }

            if (colonCount > 1)
            {
                // Mantener solo el primer ":"
                var parts = text.Split(':');
                if (parts.Length > 1)
                {
                    entry.Text = parts[0] + ":" + string.Join("", parts, 1, parts.Length - 1).Replace(":", "");
                }
            }

            // Limitar a 5 caracteres (formato HH:MM)
            if (text.Length > 5)
            {
                entry.Text = text.Substring(0, 5);
            }
        }
    }
}