// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License.

using Microsoft.EntityFrameworkCore;

namespace MyConveno.Toolkit.Sales4Pro.Server.SyncDataHost
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        /// <summary>
        /// The dataset .
        /// </summary>
        public DbSet<SyncCustomerFavorite> SyncCustomerFavorites => Set<SyncCustomerFavorite>();
        public DbSet<SyncCustomerNote> SyncCustomerNotes => Set<SyncCustomerNote>();
        public DbSet<SyncShoppingCart> SyncShoppingCarts => Set<SyncShoppingCart>();
        public DbSet<SyncShoppingCartItem> SyncShoppingCartItems => Set<SyncShoppingCartItem>();

        /// <summary>
        /// Do any database initialization required.
        /// </summary>
        /// <returns>A task that completes when the database is initialized</returns>
        public async Task InitializeDatabaseAsync()
        {
            await this.Database.EnsureCreatedAsync().ConfigureAwait(false);
        }
    }
}
