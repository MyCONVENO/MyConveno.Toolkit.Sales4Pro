﻿using Microsoft.AspNetCore.Datasync.EFCore;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    public class SyncCustomerFavorite : EntityTableData
    {
        public string UserName { get; set; }
        public string CustomerNumber { get; set; }
    }
}