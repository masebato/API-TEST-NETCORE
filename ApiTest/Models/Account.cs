using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTest.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Number { get; set; }

    public string? InitialBalance { get; set; }

    public string? State { get; set; }

    public int? ClientIdFk { get; set; }

    public string? Type { get; set; }

    public virtual Client? oClient { get; set; }
    [JsonIgnore]
    public virtual ICollection<Trasnsaction> Trasnsactions { get; } = new List<Trasnsaction>();
}
