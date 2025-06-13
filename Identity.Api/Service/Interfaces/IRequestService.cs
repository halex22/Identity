using Identity.Api.Model.DTOs;

namespace Identity.Api.Service.Interfaces
{
    public interface IRequestService
    {
        Task<List<RequestDTO>> GetAllRequests();
        Task<RequestDTO> CreateRequest(RawRequestDTO request);
        Task<RequestDTO> UpdateRequest(RequestDTO request);
        Task<RequestDTO> GetRequestById(int id);
        Task DeleteRequest(int id);
    }
}
