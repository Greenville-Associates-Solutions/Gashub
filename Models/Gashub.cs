using System;
using System.Collections.Generic;

namespace Gas.Models;

public partial class Gashub
{
    public int Id { get; set; }

    public string CompanyId { get; set; }
    public string GasTicker { get; set; }

    public string MailAddress1 { get; set; }

    public string MailAddress2 { get; set; }

    public string MailCity { get; set; }

    public string MailState { get; set; }

    public string MailZip { get; set; }

    public string MailCountry { get; set; }

    public string ShipAddress1 { get; set; }

    public string ShipAddress2 { get; set; }

    public string ShipCity { get; set; }

    public string ShipState { get; set; }

    public string ShipZip { get; set; }

    public string ShipCountry { get; set; }

    public string Glaccount { get; set; }

    public string SubAccount { get; set; }

    public string CompanyPhone { get; set; }

    public string CompanyFax { get; set; }

    public string CompanyEmail { get; set; }

    public string CompanyCare { get; set; }

    public string Description { get; set; }
}
