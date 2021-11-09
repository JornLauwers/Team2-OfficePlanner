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
  //  [Authorize(Roles = "Administrator, User")]
    public class SwaggerController : Controller
    {
        private readonly IReservationsRepository reservationsRepository;
        public SwaggerController( IReservationsRepository reservationsRepository)
        {
            this.reservationsRepository = reservationsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("CreateReservation")]
        public IActionResult CreateReservation(ReservationCreateViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                var newReservations = new Reservations<ApplicationUser>()
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
        public ActionResult<Reservations<ApplicationUser>> GetReservationById(int id)
        {
            return this.reservationsRepository.GetById(id);
        }

        [HttpGet("GetReservationByDate")]
        public ActionResult<ReservationListView<ApplicationUser>> GetReservationByDate(DateTime startDate, DateTime endDate)
        {
            return new ReservationListView<ApplicationUser>()
            {
                Reservations = this.reservationsRepository.GetByDate(startDate, endDate)
            };
        }
        [HttpPost("UpdateReservation")]
        public IActionResult UpdateReservation(ReservationUpdateViewModel reservationUpdateViewModel)
        {
            var resercation = this.reservationsRepository.GetById(reservationUpdateViewModel.Id);
            if (resercation != null)
            {
                if (ModelState.IsValid)
                {
                    resercation.StartDate = reservationUpdateViewModel.StartDate;
                    resercation.EndDate = reservationUpdateViewModel.EndDate;
                    resercation.Room = reservationUpdateViewModel.Room;
                    this.reservationsRepository.Update(resercation);
                    return Ok(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("DeleteReservation")]
        public IActionResult DeleteReservation(int id)
        {
            var r = this.reservationsRepository.GetById(id);
            if (r != null)
            {
                this.reservationsRepository.Delete(r);
                return Ok();
            }
            return NoContent();
        }

    }
}
