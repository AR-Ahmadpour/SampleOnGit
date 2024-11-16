using System.ComponentModel;

namespace Accreditation.Domain.Common.Enums;

public enum AccreditationInstanceStatusTypesEnum
{
    [Description("ارزیابی داخلی بسته یک")]
    arzyabiDakheliElzami = 13,
    [Description("ارزیابی جامع بدون ثبت نتابج")]
    arzyabi = 4,
    arzyabiDakheliLevelOne = 13,
    arzyabiDakheliLevelThree = 37,
    amadeErjaTeamArzyabi =2, //locked
    revise = 9,
    arzyabiDakheliAsasi = 14,
    khodarzyabiElzami = 17,
    khodarzyabiAsasi = 18,
    khodarzyabiIdeal = 19,
    arzyabiDakheliErsalShodeBeDaneshgah = 20,//locked
    arzyabiDakheliBarrasiShodeTavasoteDaneshgah = 21,//locked
    arzyabiPayanYafte = 22,//locked
    rastiAzmaie = 23,
    arzyabiMojadadPayanYafte = 24,//locked
    rastiAzmaiePayanYafte = 25,//locked
    arzyabiDakheliIdealAghazShode =26,
    arzyabiDakheliIdealErsalShodeBeDaneshgah = 27,//locked
    arzyabiDakheliIdealBarrasiShodeTavasoteDaneshgah = 28,//locked
    amadeErjaTeamArzyabiIdeal = 29,//locked
    arzyabiIdealAghazShode =30,
    arzyabiIdealPayanYafte =31,//locked
    arzyabiAdvari =32,
    arzyabiAdvariPayanYafte =33,//locked
    arzyabiDakheliBarrasiShodeTavasoteVezarat =34,//locked
    arzyabiDakheliIdealBarrasiShodeTavasoteVezarat =35,//locked
    arzyabiMomayeziShode = 36//locked
}
