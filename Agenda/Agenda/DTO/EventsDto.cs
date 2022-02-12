using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.DTO
{
    public class EventsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }
    }
}
