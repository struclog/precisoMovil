using PrecisoGps.Responses;
using PrecisoGps.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrecisoGps.ViewModels
{
    public class GestionHojaViewModel : BaseViewModel
    {
        public ICommand SeleccionarImagenCommand { get; }
        public ObservableCollection<Produccione> TodasLasProducciones { get; set; } = new ObservableCollection<Produccione>();
        public ObservableCollection<Produccione> ProduccionesVisibles { get; set; } = new ObservableCollection<Produccione>();

        private Produccione _ultimaVueltaGuardada;

        private ObservableCollection<Conductor> _conductores;
        public ObservableCollection<Conductor> Conductores
        {
            get => _conductores;
            set => SetProperty(ref _conductores, value);
        }

        private Conductor _conductorSeleccionado;
        public Conductor ConductorSeleccionado
        {
            get => _conductorSeleccionado;
            set => SetProperty(ref _conductorSeleccionado, value);
        }

        public ObservableCollection<RutaSimple> Rutas { get; set; } = new ObservableCollection<RutaSimple>
        {
            new RutaSimple { id_ruta = 1, nombre = "MONJAS" },
            new RutaSimple { id_ruta = 2, nombre = "SAN ISIDRO" }
        };

        private RutaSimple _rutaSeleccionada;
        public RutaSimple RutaSeleccionada
        {
            get => _rutaSeleccionada;
            set => SetProperty(ref _rutaSeleccionada, value);
        }

        private string _ayudanteNombre;
        public string AyudanteNombre
        {
            get => _ayudanteNombre;
            set => SetProperty(ref _ayudanteNombre, value);
        }

        private ObservableCollection<Gasto> _gastos;
        public ObservableCollection<Gasto> Gastos
        {
            get => _gastos;
            set => SetProperty(ref _gastos, value);
        }

        private string _fecha;
        public string Fecha
        {
            get => _fecha;
            set => SetProperty(ref _fecha, value);
        }

        private string _tipoDia;
        public string TipoDia
        {
            get => _tipoDia;
            set => SetProperty(ref _tipoDia, value);
        }

        private int idUnidad;
        public int IdUnidad
        {
            get => idUnidad;
            set => SetProperty(ref idUnidad, value);
        }

        // Comandos
        public ICommand GuardarCommand { get; }
        public ICommand GuardarVueltaCommand { get; }
        public ICommand AgregarVueltaCommand { get; }

        private int hojaId;

        public GestionHojaViewModel()
        {
            // Comando para guardar todos los cambios generales (sin validar vueltas)
            GuardarCommand = new Command(async () => await GuardarCambiosGeneralesAsync());

            // Comando para guardar solo la vuelta actual
            GuardarVueltaCommand = new Command(async () => await GuardarVueltaActualAsync());

            SeleccionarImagenCommand = new Command<Gasto>(async (gasto) => await SeleccionarImagenAsync(gasto));

            AgregarVueltaCommand = new Command(async () =>
            {
                // Si ya hay vueltas visibles, primero hacemos las validaciones
                if (ProduccionesVisibles.Count > 0)
                {
                    var vueltaActual = ProduccionesVisibles.LastOrDefault();

                    if (vueltaActual != null)
                    {
                        // Verificamos si la vuelta actual tiene los datos completos
                        if (string.IsNullOrWhiteSpace(vueltaActual.HoraSubidaEntry) ||
                            string.IsNullOrWhiteSpace(vueltaActual.HoraBajadaEntry))
                        {
                            Application.Current.MainPage.DisplayAlert("Aviso", "Debes completar la hora de inicio y fin de la vuelta actual.", "OK");
                            return;
                        }

                        // Validamos el formato y lógica de las horas
                        if (!ValidarHorasAsync(vueltaActual).Result)
                        {
                            return; // Si no pasa la validación, cancelamos la adición de nueva vuelta
                        }

                        // Verificamos si la vuelta actual ya ha sido guardada
                        bool vuelaYaGuardada = TodasLasProducciones.Any(v => v.nro_vuelta == vueltaActual.nro_vuelta);
                        if (!vuelaYaGuardada)
                        {
                            Application.Current.MainPage.DisplayAlert("Aviso", "Debes guardar la vuelta actual antes de añadir una nueva. Presiona el botón 'Guardar Vuelta'.", "OK");
                            return;
                        }
                    }
                }

                // Si llegamos aquí, podemos añadir una nueva vuelta
                // Determinamos el siguiente número de vuelta
                int ultimoNumero = TodasLasProducciones.Any()
                    ? TodasLasProducciones.Max(p => p.nro_vuelta)
                    : 0;
                int siguienteNumero = ultimoNumero + 1;

                // Creamos y agregamos la nueva vuelta a ProduccionesVisibles
                var nuevaVuelta = new Produccione
                {
                    nro_vuelta = siguienteNumero,
                    Editable = true,
                    valor_vuelta = "0", // Inicializamos con "0" para evitar nulls
                    HoraSubidaEntry = "",
                    HoraBajadaEntry = ""
                };
                _ultimaVueltaGuardada = nuevaVuelta;

                ProduccionesVisibles.Clear();
                ProduccionesVisibles.Add(nuevaVuelta);
            });

        }

        // Método para guardar solo la vuelta actual
        private async Task GuardarVueltaActualAsync()
        {
            // Obtenemos la vuelta actual
            var vueltaActual = ProduccionesVisibles.LastOrDefault();
            if (vueltaActual == null)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "No hay vuelta para guardar.", "OK");
                return;
            }

            // Verificamos que la vuelta tenga los datos completos y las horas sean válidas
            if (string.IsNullOrWhiteSpace(vueltaActual.HoraSubidaEntry) ||
                string.IsNullOrWhiteSpace(vueltaActual.HoraBajadaEntry))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Debes completar la hora de inicio y fin de la vuelta actual antes de guardar.", "OK");
                return;
            }

            // Validar el formato y lógica de las horas
            if (!await ValidarHorasAsync(vueltaActual))
            {
                return; // Si no pasa la validación, cancelamos el guardado
            }

            // Asegurarse de que el valor_vuelta no sea nulo o vacío
            if (string.IsNullOrEmpty(vueltaActual.valor_vuelta))
            {
                vueltaActual.valor_vuelta = "0";
            }

            // Convertir las horas de entrada a los campos hora_subida y hora_bajada
            vueltaActual.hora_subida = vueltaActual.HoraSubidaEntry + ":00";
            vueltaActual.hora_bajada = vueltaActual.HoraBajadaEntry + ":00";

            // Agregamos o actualizamos la vuelta en TodasLasProducciones
            var existente = TodasLasProducciones.FirstOrDefault(v => v.nro_vuelta == vueltaActual.nro_vuelta);
            if (existente != null)
            {
                // Actualizamos la existente
                existente.hora_subida = vueltaActual.hora_subida;
                existente.hora_bajada = vueltaActual.hora_bajada;
                existente.valor_vuelta = vueltaActual.valor_vuelta;
                existente.HoraSubidaEntry = vueltaActual.HoraSubidaEntry;
                existente.HoraBajadaEntry = vueltaActual.HoraBajadaEntry;
            }
            else
            {
                // Agregamos la nueva vuelta
                TodasLasProducciones.Add(vueltaActual);
            }

            // Validar datos generales básicos
            if (!ValidarDatosGenerales())
            {
                return;
            }

            try
            {
                // Crear una copia temporal de todas las producciones para enviar
                var produccionesParaEnviar = new ObservableCollection<Produccione>(TodasLasProducciones);

                // Guardar la vuelta actual
                var service = new HojasTrabajoService();
                var result = await service.ActualizarProduccionHojaAsync(
                    hojaId,
                    produccionesParaEnviar, // Usamos la copia para evitar problemas de concurrencia
                    new ObservableCollection<Gasto>(), // Enviamos una colección vacía, no null
                    ConductorSeleccionado?.id_personal ?? 0,
                    RutaSeleccionada?.id_ruta ?? 0,
                    AyudanteNombre ?? string.Empty, // Evitar null
                    Fecha ?? DateTime.Now.ToString("dd/MM/yyyy"), // Valor por defecto
                    TipoDia ?? "Normal", // Valor por defecto
                    IdUnidad
                );

                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Vuelta guardada correctamente", "OK");

                    // Marcamos la vuelta actual como guardada
                    vueltaActual.Editable = false;
                    _ultimaVueltaGuardada = vueltaActual;

                    // Crear automáticamente la siguiente vuelta
                    int ultimoNumero = TodasLasProducciones.Any()
                        ? TodasLasProducciones.Max(p => p.nro_vuelta)
                        : 0;
                    int siguienteNumero = ultimoNumero + 1;

                    // Creamos y agregamos la nueva vuelta a ProduccionesVisibles
                    var nuevaVuelta = new Produccione
                    {
                        nro_vuelta = siguienteNumero,
                        Editable = true,
                        valor_vuelta = "0" // Inicializar con "0" para evitar nulls
                    };

                    ProduccionesVisibles.Clear();
                    ProduccionesVisibles.Add(nuevaVuelta);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al guardar la vuelta", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al guardar la vuelta: {ex.Message}", "OK");
            }
        }
        // Método para guardar cambios generales (sin validar vueltas)
        private async Task GuardarCambiosGeneralesAsync()
        {
            // Validamos los datos generales
            if (!ValidarDatosGenerales())
            {
                return;
            }

            try
            {
                // Asegurarse de que todas las colecciones estén inicializadas
                if (Gastos == null)
                {
                    Gastos = new ObservableCollection<Gasto>();
                }

                if (TodasLasProducciones == null)
                {
                    TodasLasProducciones = new ObservableCollection<Produccione>();
                }

                // Guardamos todos los datos generales incluidos los gastos
                var service = new HojasTrabajoService();
                var result = await service.ActualizarProduccionHojaAsync(
                    hojaId,
                    TodasLasProducciones, // Mantenemos las producciones ya guardadas
                    Gastos,
                    ConductorSeleccionado?.id_personal ?? 0,
                    RutaSeleccionada?.id_ruta ?? 0,
                    AyudanteNombre ?? string.Empty, // Evitar null
                    Fecha ?? DateTime.Now.ToString("dd/MM/yyyy"), // Valor por defecto
                    TipoDia ?? "Normal", // Valor por defecto
                    IdUnidad
                );

                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Datos generales guardados correctamente", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al guardar los datos generales", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al guardar datos generales: {ex.Message}", "OK");
            }
        }
        // Método para validar los datos generales (sin las vueltas)
        private bool ValidarDatosGenerales()
        {
            // Validar que se haya seleccionado ruta
            if (RutaSeleccionada == null)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Debe seleccionar una ruta", "OK");
                return false;
            }

            // Validar que se haya seleccionado conductor
            if (ConductorSeleccionado == null)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Debe seleccionar un conductor", "OK");
                return false;
            }

            return true;
        }

        public async void CargarDatos(ResposeHoja hoja)
        {
            hojaId = hoja.id_hoja;
            Fecha = hoja.fecha;
            TipoDia = hoja.tipo_dia;
            AyudanteNombre = hoja.ayudante_nombre;
            RutaSeleccionada = Rutas.FirstOrDefault(r => r.id_ruta == hoja.id_ruta);
            IdUnidad = hoja.id_unidad;

            var conductorService = new ConductorService();
            var lista = await conductorService.ObtenerConductoresAsync();
            Conductores = new ObservableCollection<Conductor>(lista);
            ConductorSeleccionado = Conductores.FirstOrDefault(c => c.id_personal == hoja.id_conductor);

            if (hoja.producciones != null)
            {
                foreach (var prod in hoja.producciones)
                {
                    prod.HoraSubidaEntry = prod.hora_subida?.Substring(0, 5);
                    prod.HoraBajadaEntry = prod.hora_bajada?.Substring(0, 5);
                    prod.Editable = false;
                }
                TodasLasProducciones = new ObservableCollection<Produccione>(hoja.producciones);
            }

            // Inicializamos ProduccionesVisibles como una colección vacía
            // en lugar de agregar una nueva vuelta automáticamente
            ProduccionesVisibles.Clear();
            // NO añadimos una vuelta inicial

            string[] tiposEsperados = { "DIESEL", "CONDUCTOR", "AYUDANTE", "ALIMENTACION", "OTROS" };
            var gastosExistentes = hoja.gastos ?? new List<Gasto>();
            string baseUrl = "http://precisogps.com/back/storage/app/public/";

            foreach (var tipo in tiposEsperados)
            {
                var existente = gastosExistentes.FirstOrDefault(g => g.tipo_gasto == tipo);
                if (existente == null)
                {
                    gastosExistentes.Add(new Gasto { tipo_gasto = tipo, valor = "", Editable = true });
                }
                else
                {
                    existente.Editable = true;
                    if (!string.IsNullOrWhiteSpace(existente.imagen) && existente.imagen.StartsWith("gastos/"))
                    {
                        existente.ImagenPreview = ImageSource.FromUri(new Uri(baseUrl + existente.imagen));
                    }
                }
            }
            Gastos = new ObservableCollection<Gasto>(
                gastosExistentes.OrderBy(g => Array.IndexOf(tiposEsperados, g.tipo_gasto))
            );
        }
        private async Task SeleccionarImagenAsync(Gasto gasto)
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                }

                if (status != PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.DisplayAlert("Permiso denegado", "No se otorgaron permisos para usar la cámara.", "OK");
                    return;
                }

                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Foto del gasto"
                });

                if (photo != null)
                {
                    using (var stream = await photo.OpenReadAsync())
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        var base64 = Convert.ToBase64String(memoryStream.ToArray());

                        gasto.imagen = base64;
                        gasto.ImagenPreview = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                    }
                }
            }
            catch (FeatureNotSupportedException)
            {
                await Application.Current.MainPage.DisplayAlert("No compatible", "La cámara no está disponible en este dispositivo.", "OK");
            }
            catch (PermissionException)
            {
                await Application.Current.MainPage.DisplayAlert("Permisos", "No tienes permisos para usar la cámara.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al tomar la foto: {ex.Message}", "OK");
            }
        }

        private bool EsFormatoHoraValido(string hora)
        {
            if (string.IsNullOrWhiteSpace(hora))
                return false;

            // Verificar el formato HH:MM
            if (!System.Text.RegularExpressions.Regex.IsMatch(hora, @"^([01]?[0-9]|2[0-3]):([0-5][0-9])$"))
                return false;

            // Verificar que los componentes sean valores válidos
            string[] partes = hora.Split(':');
            if (partes.Length != 2)
                return false;

            if (!int.TryParse(partes[0], out int horas) || !int.TryParse(partes[1], out int minutos))
                return false;

            if (horas < 0 || horas > 23 || minutos < 0 || minutos > 59)
                return false;

            return true;
        }

        // Método para convertir string de hora a TimeSpan
        private TimeSpan? ConvertirATimeSpan(string hora)
        {
            if (!EsFormatoHoraValido(hora))
                return null;
            string[] partes = hora.Split(':');
            int horas = int.Parse(partes[0]);
            int minutos = int.Parse(partes[1]);
            return new TimeSpan(horas, minutos, 0);
        }

        // Método completo de validación de horas
        private async Task<bool> ValidarHorasAsync(Produccione vuelta)
        {
            // Verificar que ambos campos tengan valores
            if (string.IsNullOrWhiteSpace(vuelta.HoraSubidaEntry) ||
                string.IsNullOrWhiteSpace(vuelta.HoraBajadaEntry))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error de validación",
                    "Debes ingresar tanto la hora de subida como la hora de bajada.",
                    "OK");
                return false;
            }

            // Verificar formato de hora de subida
            if (!EsFormatoHoraValido(vuelta.HoraSubidaEntry))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error de formato",
                    "La hora de subida debe tener el formato HH:MM (ejemplo: 08:30).",
                    "OK");
                return false;
            }

            // Verificar formato de hora de bajada
            if (!EsFormatoHoraValido(vuelta.HoraBajadaEntry))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error de formato",
                    "La hora de bajada debe tener el formato HH:MM (ejemplo: 09:45).",
                    "OK");
                return false;
            }

            // Convertir a TimeSpan para comparar
            TimeSpan? horaSubida = ConvertirATimeSpan(vuelta.HoraSubidaEntry);
            TimeSpan? horaBajada = ConvertirATimeSpan(vuelta.HoraBajadaEntry);

            if (!horaSubida.HasValue || !horaBajada.HasValue)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error de conversión",
                    "No se pudieron procesar las horas ingresadas.",
                    "OK");
                return false;
            }

            // Validar que la hora de bajada sea posterior a la hora de subida
            if (horaBajada <= horaSubida)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error de lógica",
                    "La hora de bajada debe ser posterior a la hora de subida.",
                    "OK");
                return false;
            }

            // Validar que la duración no sea excesiva (ejemplo: más de 4 horas)
            TimeSpan duracion = horaBajada.Value - horaSubida.Value;
            if (duracion.TotalHours > 4)
            {
                bool continuar = await Application.Current.MainPage.DisplayAlert(
                    "Advertencia",
                    $"La duración de la vuelta es de {duracion.TotalHours:F1} horas. ¿Es correcto?",
                    "Sí, es correcto", "No, corregir");

                if (!continuar)
                    return false;
            }

            // Validar que el valor de la vuelta sea numérico
            if (!string.IsNullOrEmpty(vuelta.valor_vuelta))
            {
                if (!decimal.TryParse(vuelta.valor_vuelta, out _))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error de formato",
                        "El valor de la vuelta debe ser un número.",
                        "OK");
                    return false;
                }
            }

            return true;
        }


    }

    public class RutaSimple
    {
        public int id_ruta { get; set; }
        public string nombre { get; set; }
    }
}