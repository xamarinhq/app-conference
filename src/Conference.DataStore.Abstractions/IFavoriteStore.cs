using System;
using System.Threading.Tasks;
using Conference.DataObjects;
using System.Collections.Generic;

namespace Conference.DataStore.Abstractions
{
    public interface IFavoriteStore : IBaseStore<Favorite>
    {
        Task<bool> IsFavorite(string sessionId);
        Task DropFavorites();
    }
}

