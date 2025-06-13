using Identity.Api.Model;
using Identity.Api.Model.DTOs;
using Identity.Api.Service.Interfaces;
using Identity.Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Service
{
    public class RquestService : IRequestService
    {
        private readonly IdentityContext _context;

        public RquestService(IdentityContext context)
        {
            _context = context;
        }
        public async Task<RequestDTO> CreateRequest(RawRequestDTO request)
        {
            var newRequest = new Request
            {
                CreatedAt = DateTime.UtcNow,
                ExecutedAt = request.ExecutedAt ?? null,
                Success = request.Success ?? null,
                ErrorMessage = request.ErrorMessage ?? string.Empty,
                UserId = request.UserId
            };
            _context.Requests.Add(newRequest);
            await _context.SaveChangesAsync();

            return new RequestDTO
            {
                Id = newRequest.Id,
                CreatedAt = newRequest.CreatedAt,
                ExecutedAt = newRequest.ExecutedAt,
                Success = newRequest.Success,
                ErrorMessage = newRequest.ErrorMessage,
                UserId = newRequest.UserId
            };  
        }

        public Task DeleteRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RequestDTO>> GetAllRequests()
        {
            return await _context.Requests.ToListAsync().ContinueWith(t => t.Result.Select(r => new RequestDTO
            {
                Id = r.Id,
                CreatedAt = r.CreatedAt,
                ExecutedAt = r.ExecutedAt,
                Success = r.Success,
                ErrorMessage = r.ErrorMessage,
                UserId = r.UserId
            }).ToList());
        }

        public Task<RequestDTO> GetRequestById(int id)
        {
            //var request = _context.Requests.FindAsync(id);
            //if (request == null) throw new KeyNotFoundException($"Request with ID {id} not found.");
            throw new NotImplementedException("Method not implemented yet.");
        }

        public Task<RequestDTO> UpdateRequest(RequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
