using System.Diagnostics.Tracing;

namespace ShowTicketLibrary
{
    public class Show
    {
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public Ticket[] Seats;
        public Show(string title, DateTime date, int numSeats, double ticketCost)
        {
            if (numSeats <= 0) throw new ArgumentOutOfRangeException("bastaldo tu mettele maggiole di zelo numelo di postli se no non andale bene");
            if (ticketCost <= 0) throw new ArgumentOutOfRangeException("bastaldo tu mettele maggiole di zelo costlo biglietlo se no non andale bene");
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("No tloppo belo male male tu mettele titolo valido");

            Title = title;
            Date = date;
            Seats = new Ticket[numSeats];

            for (int i = 0; i < numSeats; i++)
            {
                Seats[i]=new Ticket(ticketCost, i+1);
            }
        }

        public Ticket SingleSeatSell(int numSeat, double money)
        {
            if (money < Seats[0].Cost) throw new ArgumentException("non hai soldi necessari");

            if (numSeat <= 0 || numSeat >= Seats.Length) throw new ArgumentException("non esistele");
            if (Seats[numSeat - 1].Sold) throw new ArgumentException("è già occupato");

            Seats[numSeat - 1].Sold = true;
            return Seats[numSeat - 1];
        }

        public Ticket[] MultipleSeatSell(int[] numSeat, double money)
        {
            if(numSeat.Length == 0) throw new ArgumentOutOfRangeException("tu dovele mettele qualcosa");
            if (money < numSeat.Length * Seats[0].Cost) throw new ArgumentException("non hai soldi necessari");

            Ticket[] tickets = new Ticket[numSeat.Length];

            for (int i = 0;i < numSeat.Length;i++)
            {
                SingleSeatSell(numSeat[i], money);
                tickets[i]=Seats[numSeat[i]-1];
            }

            return tickets;
        }

        public Ticket SingleSeatSellFirstEmpty(double money)
        {
            if (money < Seats[0].Cost) throw new ArgumentException("Non hai soldi necessari");

            for (int i = 0; i < Seats.Length;i++)
            {
                if (!Seats[i].Sold)
                {
                    Seats[i].Sold = true;
                    return Seats[i];
                }
            }
            throw new ArgumentException("Non ci sono più posti");
        }

        public Ticket[] MultipleSeatSellFirstEmpty(int howMany, double money)
        {
            if (howMany <= 0) throw new ArgumentOutOfRangeException("ma tu essele scemo? tu non vedele che non possibile");
            if(money < howMany * Seats[0].Cost) throw new ArgumentException("Non hai soldi necessari");

            Ticket[] tickets = new Ticket[howMany];

            for (int i = 0; i<howMany; i++)
            {
                tickets[i]=SingleSeatSellFirstEmpty(money);
            }
            

            return tickets;
        }
    }
}

/*
 * public Ticket[] MultipleSeatSellFirstEmpty(int howMany, double money)
        {
            if (howMany <= 0) throw new ArgumentOutOfRangeException("ma tu essele scemo? tu non vedele che non possibile");
            if(money < howMany * Seats[0].Cost) throw new ArgumentException("Non hai soldi necessari");

            int count = 0;
            int howManySelled = 0;
            int[] position = new int[howMany];

            Ticket[] tickets = new Ticket[howMany];

            do
            {
                if (Seats[count].Sold)
                {
                    howManySelled = 0;
                }
                else
                {
                    position[howManySelled]=count;
                    howManySelled++;
                }
                count++;
                
            } while (howManySelled<howMany && count<Seats.Length);

            if (howManySelled < howMany) throw new ArgumentException("Non ci sono abbastanza posti vicini");

            for (int i = 0; i < howManySelled; i++)
            {
                Seats[position[i]].Sold = true;
                tickets[i] = Seats[position[i]];
            }

            return tickets;
        }
*/