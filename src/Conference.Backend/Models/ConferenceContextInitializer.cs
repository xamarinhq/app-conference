using System;
using System.Data.Entity;

namespace Conference.Backend.Models
{
    //DropCreateDatabaseIfModelChanges
    public class ConferenceContextInitializer : DropCreateDatabaseIfModelChanges<ConferenceContext>
    {
        protected override void Seed(ConferenceContext context)
        {
            //Seed Data Here
        }
    }
}