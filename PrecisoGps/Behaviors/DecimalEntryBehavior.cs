using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PrecisoGps.Behaviors
{
    public class DecimalEntryBehavior : Behavior<Entry>
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

            // Permitir números y un único punto decimal
            string text = e.NewTextValue;
            var entry = (Entry)sender;

            // Verificar si el texto tiene caracteres no permitidos (permitimos números y punto)
            bool isValid = Regex.IsMatch(text, @"^[0-9.]+$");

            if (!isValid)
            {
                // Eliminar caracteres no válidos
                entry.Text = Regex.Replace(text, @"[^0-9.]", "");
                return;
            }

            // Verificar que solo haya un punto decimal
            int decimalPointCount = 0;
            foreach (char c in text)
            {
                if (c == '.')
                    decimalPointCount++;
            }

            if (decimalPointCount > 1)
            {
                // Dejar solo el primer punto decimal
                var parts = text.Split('.');
                if (parts.Length > 1)
                {
                    entry.Text = parts[0] + "." + string.Join("", parts, 1, parts.Length - 1).Replace(".", "");
                }
            }
        }
    }
}