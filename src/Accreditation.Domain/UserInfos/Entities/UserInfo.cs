using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Domain.UserInfos.Entities
{
    public sealed class UserInfo : Entity
    {
        public bool MaritalStatus { get; private set; }
        public int ChildCount { get; private set; }
        public int BirthOstanId { get; private set; }
        public int BirthShahrId { get; private set; }
        public int AddressOstanId { get; private set; }
        public int AddressShahrId { get; private set; }
        public string Address { get; private set; }
        public string? PersonalPicGUID { get; private set; }   
        public string? KartMeliGUID { get; private set; }   
        public string? ShenasnamehGUID { get; private set; }
        public Guid UserGuid { get; private set; }
        public User User { get; private set; }


        private UserInfo() { }



        private UserInfo(bool maritalStatus,int childCount,int birthOstanId,int birthShahrId,
            int addressOstanId,int addressShahrId, string address,string personalGuid,string kartmeliGuid, string shenasnameGuid,
            Guid userGuid):base(Guid.NewGuid())
        {
            MaritalStatus = maritalStatus;
            ChildCount = childCount;
            BirthOstanId = birthOstanId;
            BirthShahrId = birthShahrId;
            AddressOstanId = addressOstanId;
            AddressShahrId = addressShahrId;
            Address = address;
            PersonalPicGUID = personalGuid;
            KartMeliGUID = kartmeliGuid;
            ShenasnamehGUID = shenasnameGuid;
            UserGuid = userGuid;
        }



        public static UserInfo Create(bool MaritalStatus, int ChildCount, int BirthOstanId, int BirthShahrId, int AddressOstanId,
            int AddressShahrId, string Address, string PersonalPicGUID, string KartMeliGUID, string ShenasnamehGUID, Guid UserGuid)
        {
            return new UserInfo(MaritalStatus,ChildCount,BirthOstanId,BirthShahrId,AddressOstanId,AddressShahrId,
                Address,PersonalPicGUID,KartMeliGUID,ShenasnamehGUID,UserGuid);
        }


        public void Edit(bool maritalStatus, int childCount, int birthOstanId, int birthSHahrId, int addressOstanId, int addressShahrId,
            string address, string personalPicGUID, string kartMeliGUID, string shenasnamehGUID)
        {
            MaritalStatus = maritalStatus;
            ChildCount = childCount;
            BirthOstanId = birthOstanId;
            BirthShahrId = birthSHahrId;
            AddressOstanId = addressOstanId;
            AddressShahrId = addressShahrId;
            Address = address;
            PersonalPicGUID = personalPicGUID;
            KartMeliGUID = kartMeliGUID;
            ShenasnamehGUID = shenasnamehGUID;
        }
    }
}
