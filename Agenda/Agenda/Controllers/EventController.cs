using Agenda.DTO;
using Agenda.Entities;
using Agenda.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Controllers
{

    [ApiController]
    [Route("/api/events")]
    public class EventController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public EventController(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventsDto>>> Get()
        {
            var events = await appDbContext.events.ToListAsync();

            return mapper.Map<List<EventsDto>>(events);
        }

        [HttpGet("{id}", Name = "GetEvent")]

        public async Task<ActionResult<EventsDto>> Get(int id)
        {
            var events = await appDbContext.events.FirstOrDefaultAsync(e => e.Id == id);

            if (events == null)
            {
                return NotFound();
            }

            return mapper.Map<EventsDto>(events);
        }

        [HttpPost]
        public async Task<ActionResult> Post(EventCreationDto eventCreationDto)
        {
            var events = mapper.Map<Events>(eventCreationDto);

            appDbContext.Add(events);

            await appDbContext.SaveChangesAsync();

            var dto = mapper.Map<EventsDto>(events);

            return new CreatedAtRouteResult("GetEvent",new {id =events.Id }, dto);

        
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put (int id,EventCreationDto eventCreationDto)
        {
            var events = await appDbContext.events.FirstOrDefaultAsync(e => e.Id == id);
            if (events==null)
            {
                return NotFound();
            }
            mapper.Map(eventCreationDto, events);
            appDbContext.Entry(events).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete( int id)
        {
            var events = await appDbContext.events.FirstOrDefaultAsync(e => e.Id == id);

            if (events == null)
            {
                return NotFound();
            }
            appDbContext.Entry(events).State = EntityState.Deleted;

            await appDbContext.SaveChangesAsync();

            return NoContent();



        }

           
            
        
     



    }
}


