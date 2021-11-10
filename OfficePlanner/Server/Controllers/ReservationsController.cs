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
using AutoMapper;

namespace OfficePlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  //  [Authorize(Roles = "Administrator, User")]
    public class ReservationsController : Controller
    {
        private readonly IReservationsRepository reservationsRepository;
        private IMapper _mapper { get; set; }
        public ReservationsController( IReservationsRepository reservationsRepository, IMapper mapper)
        {
            this.reservationsRepository = reservationsRepository;
            this._mapper = mapper;
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
        public ActionResult<ReservationsDTO> GetReservationById(int id)
        {
            return _mapper.Map<ReservationsDTO>(this.reservationsRepository.GetById(id));
        }

        [HttpGet("GetReservationByDate")]
        public ActionResult<ReservationListView> GetReservationByDate(DateTime startDate, DateTime endDate)
        {
            return new ReservationListView()
            {
                Reservations = this.reservationsRepository.GetByDate(startDate, endDate)
            };
        }
        [HttpPost("UpdateReservation")]
        public IActionResult UpdateReservation(ReservationUpdateViewModel reservationUpdateViewModel)
        {
            var reservation = this.reservationsRepository.GetById(reservationUpdateViewModel.Id);
            if (reservation != null)
            {
                if (ModelState.IsValid)
                {
                    reservation.StartDate = reservationUpdateViewModel.StartDate;
                    reservation.EndDate = reservationUpdateViewModel.EndDate;
                    reservation.Room = reservationUpdateViewModel.Room;
                    this.reservationsRepository.Update(reservation);
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
