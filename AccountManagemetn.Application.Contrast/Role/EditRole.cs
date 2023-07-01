using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contrast.Role
{
    public class EditRole : CreateRole
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }
    }
}
