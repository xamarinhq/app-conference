using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinEvolve.Clients.UI
{
    public partial class CodeOfConductPage : ContentPage
    {
        public static string Conduct = "We want Xamarin Evolve 2016 to be the best conference you’ve ever attended. In addition to great content, technical training, networking opportunities, and fun events, we want to make sure the conference is a safe and productive environment for all participants." +
            "\n\nAs such, we are dedicated to providing a harassment-free conference experience for everyone regardless of gender, sexual orientation, disability, physical appearance, body size, race, or religion. We do not tolerate harassment of conference participants in any form. Sexual language and imagery is not appropriate for any conference venue, including talks. Conference participants violating these rules may be asked to leave(without a refund) at the discretion of the conference organizers." +
            "\n\nHarassment includes offensive verbal comments related to gender, sexual orientation, disability, physical appearance, body size, race, religion, sexual images in public spaces, deliberate intimidation, stalking, following, harassing photography or recording, sustained disruption of talks or other events, inappropriate physical contact, and unwelcome sexual attention. Participants asked to stop any harassing behavior are expected to comply immediately." +
            "\n\nExhibitors in the expo hall, sponsor or vendor booths, or similar activities are also subject to this code of conduct. In particular, exhibitors should not use sexualized images, activities, or other material. Booth staff (including volunteers) should not use sexualized clothing/uniforms/costumes, or otherwise create a sexualized environment." +
            "\n\nIf you are being harassed, notice that someone else is being harassed, or have any other concerns, please contact a member of conference staff immediately. Conference staff can be identified by t-shirts and special badges." +
            "\n\nConference staff will be happy to help participants contact hotel/venue security or local law enforcement, provide escorts, or otherwise assist those experiencing harassment to feel safe for the duration of the conference." +
            "\n\nWe thank our attendees, speakers and exhibitors for their help in keeping the event welcoming, respectful, and friendly to all participants, so that we all can enjoy a great conference."; 
        public CodeOfConductPage()
        {
            InitializeComponent();

            CodeOfConductText.Text = Conduct;
        }
    }
}

