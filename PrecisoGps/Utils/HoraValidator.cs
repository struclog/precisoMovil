using System;
using System.Text.RegularExpressions;

namespace PrecisoGps.Utils
{
    public class HoraValidator
    {
        // Patrón que permite solo horas en formato HH:mm donde HH está entre 00-23 y mm entre 00-59
        private static readonly Regex HoraRegex = new Regex(@"^([0-1][0-9]|2[0-3]):([0-5][0-9])$");

        /// <summary>
        /// Valida si la hora está en formato correcto (HH:mm) y si los valores son válidos
        /// </summary>
        /// <param name="hora">Hora a validar</param>
        /// <returns>True si es válida, false si no</returns>
        public static bool ValidarHora(string hora)
        {
            if (string.IsNullOrEmpty(hora))
                return true; // Permitir valores vacíos, para poder borrar

            return HoraRegex.IsMatch(hora);
        }

        /// <summary>
        /// Formatea la hora para asegurar que sigue el formato correcto
        /// </summary>
        /// <param name="hora">Hora a formatear</param>
        /// <returns>Hora formateada si es posible, o la entrada original si no</returns>
        public static string FormatearHora(string hora)
        {
            if (string.IsNullOrEmpty(hora))
                return hora;

            // Si ya tiene el formato correcto, devolver sin cambios
            if (ValidarHora(hora))
                return hora;

            // Intentar corregir el formato si es posible
            try
            {
                if (hora.Length == 1 && char.IsDigit(hora[0]))
                {
                    // Si solo hay un dígito, asumir que es la hora
                    int h = int.Parse(hora);
                    if (h >= 0 && h <= 9)
                        return $"0{h}:00";
                }
                else if (hora.Length == 2 && int.TryParse(hora, out int h) && h >= 0 && h <= 23)
                {
                    // Si hay dos dígitos, asumir que es la hora completa
                    return $"{hora}:00";
                }
                else if (hora.Contains(":"))
                {
                    // Si ya tiene dos puntos, intentar separar horas y minutos
                    string[] parts = hora.Split(':');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int hours) &&
                        int.TryParse(parts[1], out int minutes))
                    {
                        // Corregir valores fuera de rango
                        hours = Math.Min(23, Math.Max(0, hours));
                        minutes = Math.Min(59, Math.Max(0, minutes));

                        return $"{hours:00}:{minutes:00}";
                    }
                }
            }
            catch
            {
                // Si hay errores al formatear, devolver la entrada original
                return hora;
            }

            // Si no se pudo formatear, devolver la entrada original
            return hora;
        }
    }
}