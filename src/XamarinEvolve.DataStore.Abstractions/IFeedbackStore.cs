using System;
using System.Threading.Tasks;
using XamarinEvolve.DataObjects;

namespace XamarinEvolve.DataStore.Abstractions
{
    public interface IFeedbackStore : IBaseStore<Feedback>
    {
        Task<bool> LeftFeedback(Session session);
        Task DropFeedback();
    }
}

