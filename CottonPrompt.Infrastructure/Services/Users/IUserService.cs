namespace CottonPrompt.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task LoginAsync(Guid id, string name, string email);
    }
}
