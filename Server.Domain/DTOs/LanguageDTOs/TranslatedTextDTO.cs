using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.LanguageDTOs
{
    [DataContract]
    public class TranslatedTextDTO
    {
        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string LanguageCode { get; set; }
    }
}
