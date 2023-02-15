using System;
using System.Collections.Generic;

namespace ApiTest.Models;

public partial class Trasnsaction
{
    public int TransactionId { get; set; }

    public string? Type { get; set; }

    public string? Balance { get; set; }

    public string? Value { get; set; }

    public DateTime? DateTransaction { get; set; }

    public int AccountIdFk { get; set; }

    public virtual Account? oAccount { get; set; }
}
