using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

public partial class Produccione : INotifyPropertyChanged
{
    [JsonIgnore]
    public bool ReadOnly => !Editable;

    public int id_produccion { get; set; }
    public int id_hoja { get; set; }
    public int nro_vuelta { get; set; }

    private string _horaSubidaEntry;
    public string HoraSubidaEntry
    {
        get => _horaSubidaEntry;
        set
        {
            _horaSubidaEntry = FormatearHora(value);
            hora_subida = ObtenerFormatoApi(_horaSubidaEntry);
            OnPropertyChanged();
        }
    }

    private string _horaBajadaEntry;
    public string HoraBajadaEntry
    {
        get => _horaBajadaEntry;
        set
        {
            _horaBajadaEntry = FormatearHora(value);
            hora_bajada = ObtenerFormatoApi(_horaBajadaEntry);
            OnPropertyChanged();
        }
    }

    public string valor_vuelta { get; set; }
    public string hora_subida { get; set; }
    public string hora_bajada { get; set; }

    public bool Editable { get; set; } = true;

    private string FormatearHora(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "";
        var clean = input.Replace(":", "").Trim();
        if (clean.Length > 4) clean = clean.Substring(0, 4);
        if (clean.Length >= 3) return clean.Insert(2, ":");
        return clean;
    }

    private string ObtenerFormatoApi(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return null;
        var parts = input.Split(':');
        if (parts.Length == 2 && int.TryParse(parts[0], out var h) && int.TryParse(parts[1], out var m))
        {
            return $"{h:D2}:{m:D2}:00";
        }
        return null;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}