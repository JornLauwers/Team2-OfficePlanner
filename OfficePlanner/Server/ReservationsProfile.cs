using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OfficePlanner.Shared;
using OfficePlanner.Server.Models;


namespace OfficePlanner.Server
{
    public class ReservationsProfile : Profile
    {
        public ReservationsProfile()
        {
            CreateMap<Reservations<ApplicationUser>, ReservationsDTO>();
        }
    }
}
