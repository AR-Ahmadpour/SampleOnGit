namespace Accreditation.Application.Users.Roles.GetRoleByIsSetadi
{
    public sealed record GetRoleByIsSetadiDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsDeleted { get; init; }
        //public Guid? UserId { get; init; }
        /// <summary>
        /// نقش ستادی و ارزیاب کشوری
        /// </summary>
        public bool IsSetadi { get; init; }
        //public ICollection<User> Users { get; init; } = new List<User>();
    }
}
