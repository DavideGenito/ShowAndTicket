namespace ShowTicketLibrary
{
    public class Ticket
    {
        public double Cost {  get; private set; }
        public int Seat { get; private set; }
        public bool Sold { get; internal set; }

        public Ticket(double cost, int seat)
        {
            Cost = cost;
            Seat = seat;
        }

        public int ciao;
    }
}
