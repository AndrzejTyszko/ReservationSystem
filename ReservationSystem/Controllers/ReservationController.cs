using Microsoft.AspNetCore.Mvc;
using ConcertReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ConcertReservationSystem.Data;

namespace ConcertReservationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult MakeReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllReservations()
        {
            var reservations = _context.Reservations.ToList();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, Reservation updatedReservation)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.UserName = updatedReservation.UserName;
            reservation.SeatNumber = updatedReservation.SeatNumber;
            reservation.ReservationTime = DateTime.Now; // Możesz zaktualizować czas rezerwacji, jeśli chcesz

            _context.SaveChanges();
            return Ok();
        }
    }
}
