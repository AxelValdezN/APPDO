using System.Collections.ObjectModel;
using MIO.Models;
using MIO.Services;

namespace MIO;

public partial class MainPage : ContentPage
{
    private readonly DatabaseService _dbService = new();
    private DateTime _fechaActual = DateTime.Today;
    private List<PagoModel> _todosLosPagosDelMes = new();

    public ObservableCollection<DiaModel> DiasMes { get; set; } = new();

    // Colección vinculada directamente al CollectionView inferior
    public ObservableCollection<PagoModel> PagosDelMes { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        _ = CargarMesAsync(_fechaActual);
    }

    private async Task CargarMesAsync(DateTime fecha)
    {
        LblMesAnio.Text = fecha.ToString("MMMM yyyy");
        LblTituloLista.Text = $"Pagos de {fecha:MMMM yyyy}";

        DiasMes.Clear();
        PagosDelMes.Clear();

        try
        {
            _todosLosPagosDelMes = await _dbService.ObtenerPagosDelMesAsync(fecha.Year, fecha.Month);
        }
        catch
        {
            _todosLosPagosDelMes = new List<PagoModel>();
        }

        // 1. Cargar todos los pagos del mes en la lista inferior ordenados por fecha
        var pagosOrdenados = _todosLosPagosDelMes.OrderBy(p => p.Fecha).ToList();
        foreach (var pago in pagosOrdenados)
        {
            PagosDelMes.Add(pago);
        }

        // 2. Construir días del mes y asignar puntos en el calendario
        int diasEnMes = DateTime.DaysInMonth(fecha.Year, fecha.Month);
        DateTime primerDiaMes = new DateTime(fecha.Year, fecha.Month, 1);
        int offsetInicio = ((int)primerDiaMes.DayOfWeek + 6) % 7;

        for (int i = 0; i < offsetInicio; i++)
        {
            DiasMes.Add(new DiaModel());
        }

        for (int dia = 1; dia <= diasEnMes; dia++)
        {
            DateTime fechaDia = new DateTime(fecha.Year, fecha.Month, dia);
            var pagosDia = _todosLosPagosDelMes.Where(p => p.Fecha.Date == fechaDia.Date).ToList();

            var diaModel = new DiaModel { FechaReal = fechaDia };

            foreach (var pago in pagosDia)
            {
                diaModel.PuntosEstado.Add(pago.ColorEstado);
            }

            DiasMes.Add(diaModel);
        }
    }

    private void OnDiaSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is DiaModel diaSeleccionado && diaSeleccionado.FechaReal.HasValue)
        {
            DateTime fecha = diaSeleccionado.FechaReal.Value;
            var pagosDelDia = _todosLosPagosDelMes.Where(p => p.Fecha.Date == fecha.Date).OrderBy(p => p.Fecha).ToList();

            // Si el usuario toca un día específico con pagos, filtramos la lista inferior a ese día; 
            // de lo contrario, si toca un día sin pagos, muestra todos los del mes.
            PagosDelMes.Clear();
            var listaAMostrar = pagosDelDia.Any() ? pagosDelDia : _todosLosPagosDelMes.OrderBy(p => p.Fecha).ToList();

            LblTituloLista.Text = pagosDelDia.Any()
                ? $"Pagos del {fecha:dd/MM/yyyy}"
                : $"Pagos de {_fechaActual:MMMM yyyy}";

            foreach (var p in listaAMostrar)
            {
                PagosDelMes.Add(p);
            }
        }
    }

    private async void OnMesAnteriorClicked(object sender, EventArgs e)
    {
        _fechaActual = _fechaActual.AddMonths(-1);
        await CargarMesAsync(_fechaActual);
    }

    private async void OnMesSiguienteClicked(object sender, EventArgs e)
    {
        _fechaActual = _fechaActual.AddMonths(1);
        await CargarMesAsync(_fechaActual);
    }
}