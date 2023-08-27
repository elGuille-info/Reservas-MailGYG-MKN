// Clase para las reservas que no tienen email.             (27/ago/23 17.48)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiReservasMailGYG;

public class ReservasSinEmail
{
    public ReservasSinEmail()
    {

    }
    public ReservasSinEmail(string nombre, string notas)
    {
        Nombre = nombre;
        Notas = notas;
    }

    public string Nombre { get; set; }
    public string Notas { get; set; }
}
