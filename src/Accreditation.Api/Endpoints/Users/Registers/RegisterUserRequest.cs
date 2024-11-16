namespace Accreditation.Api.Endpoints.Users.Registers;

public sealed record RegisterUserRequest(
      string Password,
      string FirstName,
      string LastName,
      string FatherName,
      string BirthCertNo,
      string BirthCertSerial,
      string BirthPlace,
      string Mobile,
      string Tel,
      string Email,
      byte GenderId,
      DateOnly? DeathDate,
      bool IsAlive,
      bool SabteAhvalApproved,
      bool IsDeleted,
      long? ImageId,
      string NationalCode,
      DateOnly? BirthDate,
      bool AtbaKhareji 
);

