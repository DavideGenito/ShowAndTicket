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


        public Ticket[] SeatSell(int[] numSeat)
        {
            if(numSeat.Length == 0) throw new ArgumentOutOfRangeException("tu dovele mettele qualcosa");

            Ticket[] tickets = new Ticket[numSeat.Length];

            for (int i = 0;i < numSeat.Length;i++)
            {
                if (numSeat[i] <= 0 || numSeat[i] >= Seats.Length) throw new ArgumentException("non esistele");
                if (Seats[numSeat[i] - 1].Sold) throw new ArgumentException("è già occupato");

                Seats[numSeat[i] - 1].Sold = true;
                tickets[i]=Seats[numSeat[i]-1];
            }
            return tickets;
        }

        public Ticket[] SeatSellFirstEmpty(int howMany)
        {
            if (howMany <= 0) throw new ArgumentOutOfRangeException("ma tu essele scemo? tu non vedele che non possibile");

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

            if (howManySelled < howMany) throw new ArgumentException("Non ci sono abbastanza posti vicini neglo");

            for (int i = 0; i < howManySelled; i++)
            {
                Seats[position[i]].Sold = true;
                tickets[i] = Seats[position[i]];
            }

            return tickets;
        }
    }
}
