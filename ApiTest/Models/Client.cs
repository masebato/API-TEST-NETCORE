using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTest.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? Password { get; set; }

    public string? State { get; set; }

    public int? PersonIdFk { get; set; }
    [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual Person? oPerson { get; set; }
}
