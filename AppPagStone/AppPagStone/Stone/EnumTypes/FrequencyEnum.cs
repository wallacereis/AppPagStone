using System.Runtime.Serialization;

namespace AppPagStone.Stone.EnumTypes
{
    [DataContract]
    public enum FrequencyEnum
    {

        /// <summary>
        /// Semanal
        /// </summary>
        [EnumMember]
        Weekly = 1,

        /// <summary>
        /// Mensal
        /// </summary>
        [EnumMember]
        Monthly = 2,

        /// <summary>
        /// Anual
        /// </summary>
        [EnumMember]
        Yearly = 3,

        /// <summary>
        /// Diário
        /// </summary>
        [EnumMember]
        Daily = 4
    }
}
