using System.Collections.Generic;

namespace EdzoteremFoglalos
{
    public class Gym
    {
        private List<GymReservation> reservations;

        public Gym()
        {
            reservations = new List<GymReservation>();
        }

        public List<GymReservation> getReservations()
        {
            return reservations;
        }

        public bool addReservation(GymReservation reservation)
        {
            if (reservation is TeamReservation)
            {
                foreach (GymReservation oldReservation in reservations)
                {
                    if (oldReservation is TeamReservation &&
                        oldReservation.getReservationTime() == reservation.getReservationTime())
                    {
                        return false;
                    }
                }
            }

            reservations.Add(reservation);
            return true;
        }
    }
}
