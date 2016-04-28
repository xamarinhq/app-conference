using System;
using System.Threading.Tasks;
using XamarinEvolve.DataObjects;
using System.Collections.Generic;

namespace XamarinEvolve.DataStore.Abstractions
{
    public interface IFavoriteStore : IBaseStore<Favorite>
    {
        Task<bool> IsFavorite(string sessionId);
        Task DropFavorites();
    }
}

