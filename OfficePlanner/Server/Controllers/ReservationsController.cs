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
        private readonly IRoomsRepository roomsRepository;
        private IMapper _mapper { get; set; }
        public ReservationsController( IReservationsRepository reservationsRepository, IMapper mapper, IRoomsRepository roomsRepository)
        {
            this.reservationsRepository = reservationsRepository;
            this._mapper = mapper;
            this.roomsRepository = roomsRepository;
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
                //if (ValidateReservation(reservation))
                //{
                    Reservations<ApplicationUser> newReservations = _mapper.Map<Reservations<ApplicationUser>>(reservation);
                    this.reservationsRepository.Create(newReservations);
                    return Ok(ModelState);
                //}
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
                Reservations = this.reservationsRepository.GetByDate(startDate:startDate, endDate:endDate)
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
                    reservation = _mapper.Map<Reservations<ApplicationUser>>(reservationUpdateViewModel);
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
        [HttpPost("IsReservationValid")]
        public IActionResult IsReservationValid([FromBody]ReservationCreateViewModel reservationCreateViewModel)
        {
            bool valid = ValidateReservation(reservationCreateViewModel);
            if (valid)
            {
                return Ok();
            }
            return BadRequest();
        }

        private bool ValidateReservation(ReservationCreateViewModel reservationCreateViewModel)
        {
            RoomVersions<ApplicationUser> room = roomsRepository.GetRoomVersion(reservationCreateViewModel.Room, reservationCreateViewModel.StartDate);
            List<ReservationsDTO> reservations = reservationsRepository.GetByDate(reservationCreateViewModel.StartDate, reservationCreateViewModel.EndDate);

            IEnumerable<ReservationsDTO> reservationInGivenRoom = reservations.Where(x => x.Room == room.RoomId);

            if (reservationInGivenRoom.Count() < room.AvailableSeats)
            {
                return true;
            }
            return false;
        }
    }
}
