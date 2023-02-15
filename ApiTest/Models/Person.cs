using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTest.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public string? Age { get; set; }

    public string? Identification { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
    [JsonIgnore]
    public virtual ICollection<Client> Clients { get; } = new List<Client>();
}
