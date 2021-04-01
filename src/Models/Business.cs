using System;
using System.Collections.Generic;

public class Business
{
    public long Id { get; set; }

    public string Auth0Id { get; set; }

    public string BusinessName { get; set; }

    public string PrimaryEmail { get; set; }

    public string AddrLocation { get; set; }

    public int TelephoneNumber { get; set; }

    public string BusinessLogo { get; set; }

    public bool IsTrading { get; set; }

    public int BusinessId { get; set; }

    public IEnumerable<Product> Products { get; set; }
}
