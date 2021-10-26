using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OfficePlanner.Server.Data;
using OfficePlanner.Shared;
using OfficePlanner.Server.Models;
using OfficePlanner.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace OfficePlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, User")]
    public class SwaggerController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IReservationsRepository reservationsRepository;
        public SwaggerController(ApplicationDbContext context, IReservationsRepository reservationsRepository)
        {
            this.db = context;
            this.reservationsRepository = reservationsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        //to test the database
        //[HttpPost("Create")]
        //public async Task<Roles> CreateRole()
        //{
        //    Roles role = new Roles { Name = "User" };
        //    var newRole = await db.Roles.AddAsync(role);
        //    await db.SaveChangesAsync();
        //    return newRole.Entity;
        //}
        //[HttpGet("GetAll")]
        //public async Task<IEnumerable<Roles>> GetRoles()
        //{
        //    return db.Roles;
        //}
        [HttpPost("CreateReservation")]
        public IActionResult CreateReservation(ReservationCreateViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                var newReservations = new Reservations()
                {
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    Room = reservation.Room,
                    User = reservation.User
                };
                this.reservationsRepository.Create(newReservations);
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetReservationById/{id}")]
        public ReservationListViewModel GetReservationById(int id)
        {
            var reservations = new ReservationListViewModel();
            reservations.Reservations.Add(this.reservationsRepository.GetById(id));
            return reservations;
        }
        [HttpGet("GetReservationByDate")]
        public ReservationListViewModel GetReservationByDate(DateTime startDate, DateTime endDate)
        {
            var reservations = new ReservationListViewModel();
            reservations.Reservations = (List<Reservations>)this.reservationsRepository.GetByDate(startDate, endDate);
            return reservations;
        }
        [HttpPost("UpdateReservation")]
        public IActionResult UpdateReservation(ReservationUpdateViewModel reservationUpdateViewModel)
        {
            var r = this.reservationsRepository.GetById(reservationUpdateViewModel.Id);
            if (r != null)
            {
                if (ModelState.IsValid)
                {
                    r.StartDate = reservationUpdateViewModel.StartDate;
                    r.EndDate = reservationUpdateViewModel.EndDate;
                    r.Room = reservationUpdateViewModel.Room;
                    this.reservationsRepository.Update(r);
                    return Ok(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("DeleteReservation")]
        public string DeleteReservation(int id)
        {
            var r = this.reservationsRepository.GetById(id);
            if (r != null)
            {
                this.reservationsRepository.Delete(r);
                return "{\"message\" : \"reservation deleted succesfuly\"}";
            }
            return "{\"message\" : \"couldn't delete the reservation\"}";
        }

    }
}
