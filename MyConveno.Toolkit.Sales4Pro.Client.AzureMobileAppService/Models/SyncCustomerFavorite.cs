﻿using Microsoft.Datasync.Client;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncCustomerFavorite : DatasyncClientData, IEquatable<SyncCustomerFavorite>
{
    public string UserID { get; set; }
    public string CustomerNumber { get; set; }

    public bool Equals(SyncCustomerFavorite other)
    => other != null && other.Id == Id && other.UserID == UserID && other.CustomerNumber == CustomerNumber;

}
