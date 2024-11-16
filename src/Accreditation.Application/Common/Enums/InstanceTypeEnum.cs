using System.ComponentModel;

namespace Accreditation.Domain.Common.Enums;

public enum InstanceTypes
{
    [Description("ارزیابی داخلی ")]
    ArzyabiDakheli = 1,
    [Description("ارزیابی جامع ")]
    ArzyabiJame = 2,
    [Description("ارزیابی مجدد ")]
    ArzyabiMojadad = 4,
    [Description("ارزیابی راستی آزمایی ")]
    RastiAzmai = 5,
    [Description("ارزیابی داخلی ایده ال ")]
    ArzybiDakheliIdeal = 6,
    [Description("ارزیابی ایده آل ")]
    ArzyabiIdeal = 7,
    [Description("ارزیابی ادواری ")]
    ArzyabiAdvari = 8
}
