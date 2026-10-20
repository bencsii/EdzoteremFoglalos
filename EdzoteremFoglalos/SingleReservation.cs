namespace EdzoteremFoglalos
{
    public class SingleReservation : GymReservation
    {
        private string contactName;
        private int reservationTime;
        private bool hasSeasonTicket;

        public SingleReservation(string contactName, int reservationTime)
        {
            this.contactName = contactName;
            this.reservationTime = reservationTime;
            hasSeasonTicket = false;
        }

        public string getContactName()
        {
            return contactName;
        }

        public int getReservationTime()
        {
            return reservationTime;
        }

        public void buySeasonTicket()
        {
            hasSeasonTicket = true;
        }

        public int calculatePriceToPay()
        {
            if (hasSeasonTicket == true)
            {
                return 2250;
            }

            return 4500;
        }
    }
}
