using System;
using System.Data.Entity;

namespace Conference.Backend.Models
{
    //DropCreateDatabaseIfModelChanges
    public class ConferenceContextInitializer : DropCreateDatabaseIfModelChanges<ConferenceContext>
    {
        protected override void Seed(ConferenceContext context)
        {
            try
            {
                DefaultData.DefaultDataSetup.AddAll(context);
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation(
                              "Class: {0}, Property: {1}, Error: {2}",
                              validationErrors.Entry.Entity.GetType().FullName,
                              validationError.PropertyName,
                              validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message + "\n" + ex.InnerException.Message + "\n" + ex.StackTrace);
            }
        }
    }
}