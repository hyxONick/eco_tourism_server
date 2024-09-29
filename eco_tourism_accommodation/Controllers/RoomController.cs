using eco_tourism_accommodation.Models;
using eco_tourism_accommodation.Services;
using Microsoft.AspNetCore.Mvc;

namespace eco_tourism_accommodation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomInfoController : ControllerBase
    {
        private readonly IRoomInfoService _roomInfoService;

        public RoomInfoController(IRoomInfoService roomInfoService)
        {
            _roomInfoService = roomInfoService;
        }

        // GET: RoomInfo/fetch
        [HttpGet("fetch")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomInfoService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        // GET: RoomInfo/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomInfoService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // POST: RoomInfo/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomInfo room)
        {
            if (room == null)
            {
                return BadRequest("Room cannot be null.");
            }

            var createdRoom = await _roomInfoService.CreateRoomAsync(room);
            return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
        }

        // POST: RoomInfo/update/{id}
        [HttpPost("update/{id:int}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomInfo room)
        {
            if (room == null)
            {
                return BadRequest("Room cannot be null.");
            }

            var updatedRoom = await _roomInfoService.UpdateRoomAsync(id, room);
            if (updatedRoom == null)
            {
                return NotFound();
            }
            return Ok(updatedRoom);
        }

        // POST: api/RoomInfo/delete/{id}
        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomInfoService.DeleteRoomAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
