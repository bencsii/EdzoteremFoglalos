using System.Collections.Generic;

namespace EdzoteremFoglalos
{
    public class TeamReservation : GymReservation
    {
        private string contactName;
        private List<string> teamMembers;
        private int reservationTime;

        public TeamReservation(string contactName, List<string> teamMembers, int reservationTime)
        {
            this.contactName = contactName;
            this.teamMembers = teamMembers;
            this.reservationTime = reservationTime;
        }

        public string getContactName()
        {
            return contactName;
        }

        public int getReservationTime()
        {
            return reservationTime;
        }

        public int calculatePriceToPay()
        {
            int people = teamMembers.Count + 1;
            return 9000 / people;
        }

        public List<string> getTeamMembers()
        {
            return teamMembers;
        }
    }
}
