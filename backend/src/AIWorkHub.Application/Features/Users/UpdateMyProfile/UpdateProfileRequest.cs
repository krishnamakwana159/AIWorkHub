namespace AIWorkHub.Application.Features.Users.UpdateMyProfile;

public sealed class UpdateProfileRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
