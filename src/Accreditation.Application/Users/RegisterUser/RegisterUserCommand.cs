using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
      string Password ,
      string FirstName , 
      string LastName , 
      string FatherName , 
      string BirthCertNo , 
      string BirthCertSerial , 
      string BirthPlace , 
      string Mobile , 
      string Tel , 
      string Email , 
      byte GenderId , 
      DateOnly? DeathDate , 
      bool IsAlive , 
      bool SabteAhvalApproved ,   
      bool IsDeleted , 
      long? ImageId , 
      string NationalCode , 
      DateOnly? BirthDate , 
      bool AtbaKhareji ,
      byte RoleId,
      string Passwprd
) : ICommand<Guid>;


