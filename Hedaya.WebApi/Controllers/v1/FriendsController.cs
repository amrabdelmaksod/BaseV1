using API.Errors;
using Hedaya.Application.Friends.Commands;
using Hedaya.Application.Friends.Models;
using Hedaya.Application.Friends.Queries;
using Hedaya.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class FriendsController : BaseController<FriendsController>
    {



        [HttpGet("list")]
        public async Task<ActionResult<apiResponse>> GetFriendList()
        {
            
            var headers = Request.Headers;
            if (!headers.ContainsKey("Authorization"))
            {
                ModelState.AddModelError("Authorization", "Missing tokin");
                if (!ModelState.IsValid)
                {
                    return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                }
            }

         
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");


            var query = new GetFriendListQuery { token = token };
            var friendList = await Mediator.Send(query);
            return Ok(friendList);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<FriendDto>>> SearchForTrainees([FromQuery] string searchTerm)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var query = new SearchForTraineesQuery { TraineeId = userId, SearchTerm = searchTerm };
            var searchResults = await Mediator.Send(query);
            return Ok(searchResults);
        }

        [HttpPost("send-request")]
        public async Task<ActionResult> SendFriendRequest(string FriendId)
        {
            //Get User Id From Claims 
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            // Send the command to the mediator to handle
            var command = new SendFriendRequestCommand { FriendId = FriendId, TraineeId = userId };
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("respond-to-request")]
        public async Task<ActionResult> RespondToFriendRequest(string FriendId, bool Accepted)
        {
           
            // Send the command to the mediator to handle
            await Mediator.Send(new RespondToFriendRequestCommand {Accepted = Accepted, FriendId = FriendId, TraineeId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value });

            return Ok();
        }

        [HttpGet("pending-requests")]
        public async Task<ActionResult<List<FriendRequestDto>>> GetPendingFriendRequests()
        {
            // Get the current user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

            // Create a new query to retrieve the pending friend requests
            var query = new GetPendingFriendRequestsQuery
            {
                TraineeId = userId
            };

            // Send the query to the mediator to handle
            var result = await Mediator.Send(query);

            return Ok(result);
        }


    }
}
