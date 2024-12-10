using GustoHub.Data.Models;
using GustoHub.Data.ViewModels.GET;
using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;

namespace GustoHub.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AddAsync(POSTUserDto userDto);
        Task<bool> ExistsByIdAsync(Guid userId);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<GETUserDto> GetByIdAsync(Guid userId);

        Task NotifyAdminForVerification(User user, string adminEmail);

        //Task<IEnumerable<GETUserDto>> AllActiveAsync();
        //Task<IEnumerable<GETUserDto>> AllDeactivatedAsync();
        //Task<string> DeactivateAsync(Guid employeeId);
        //Task<string> ActivateAsync(Guid employeeId);
        Task<string> UpdateAsync(PUTUserDto userDto, Guid userId);
        //Task<bool> IsUserActiveAsync(Guid userId);
    }

}
