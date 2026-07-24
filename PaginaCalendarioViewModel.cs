using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MIO.Models;

namespace MIO
{
    public class PaginaCalendarioViewModel
    {
        public DateTime FechaSeleccionada { get; set; } = DateTime.Today;
        public ObservableCollection<ActividadModel> ListaActividades { get; set; }

        public PaginaCalendarioViewModel()
        {
            ListaActividades = new ObservableCollection<ActividadModel>
            {
                new ActividadModel { Hora = "09:00 AM", Titulo = "Reunión de proyecto", Descripcion = "Revisión de avance del sprint", Completada = true },
                new ActividadModel { Hora = "11:30 AM", Titulo = "Consulta de base de datos", Descripcion = "Optimización de scripts SQL", Completada = false },
                new ActividadModel { Hora = "04:00 PM", Titulo = "Pruebas de la API", Descripcion = "Validar endpoints de autenticación", Completada = false }
            };
        }
    }
}
