using System;
using System.Data.Entity;

namespace XamarinEvolve.Backend.Models
{
    //DropCreateDatabaseIfModelChanges
    public class XamarinEvolveContextInitializer : DropCreateDatabaseIfModelChanges<XamarinEvolveContext>
    {
        protected override void Seed(XamarinEvolveContext context)
        {
            //Seed Data Here
        }
    }
}