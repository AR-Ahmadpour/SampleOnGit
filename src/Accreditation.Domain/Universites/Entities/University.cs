using SharedKernel;

namespace Accreditation.Domain.Universites.Entities
{
    public sealed class University : Entity
    {

        public int Id { get; set; }
        public string Title { get; private set; }
        public string? EnglishCode { get; private set; }
        public int? IntCodeParvaneh { get; private set; }
        public int? IntCodePaygah { get; private set; }
        public string? ShortName { get; private set; }
        public string? LongName { get; private set; }
        public string? EnglishTitle { get; private set; }
        public bool IsDeleted { get; private set; }
        public int OrderId { get; private set; }

        public University()
        {
        }

        private University(Guid id)
        {
            GUID = id;
        }

        public University(Guid gUID, string title, string shortName, string englishCode,
                    string longName, string englishTitle, int? intCodeParvaneh,
                    int? intCodePaygah, bool isDeleted, int orderId)
  : base(gUID)
        {

            Title = title;
            ShortName = shortName;
            EnglishCode = englishCode;
            LongName = longName;
            EnglishTitle = englishTitle;
            IntCodeParvaneh = intCodeParvaneh;
            IntCodePaygah = intCodePaygah;
            IsDeleted = isDeleted;
            OrderId = orderId;
        }
        public static University Create(string title, string shortName, string englishCode,
                          string longName, string englishTitle, int? intCodeParvaneh,
                          int? intCodePaygah, bool isDeleted, int orderId)
       => new University(Guid.NewGuid(), title, shortName, englishCode, longName,
                         englishTitle, intCodeParvaneh, intCodePaygah, isDeleted, orderId);
    }
}
