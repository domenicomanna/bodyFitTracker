namespace Api.Infrastructure.Security
{
    public interface IUserAccessor
    {
        int GetCurrentUserId();
    }
}