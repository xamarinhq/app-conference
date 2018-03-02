using System;
using System.Threading.Tasks;
using Conference.DataObjects;

namespace Conference.DataStore.Abstractions
{
    public interface IFeedbackStore : IBaseStore<Feedback>
    {
        Task<bool> LeftFeedback(Session session);
        Task DropFeedback();
    }
}

