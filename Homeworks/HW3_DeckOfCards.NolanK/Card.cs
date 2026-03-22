// *************************************************************************
// DO NOT MODIFY ANY CODE IN THIS FILE
// *************************************************************************
namespace HW3_DeckOfCards
{
    // Define the possible card suits
    //
    // NOTE: FOR DEBUGGING ONLY
    // To help keep the data structure sizes small while you are testing, I strongly
    // suggest temporarily commenting out all but one CardSuit value in Card.cs
    // so that you get a deck of 13 cards instead of 52.
    // The sample output in the writeup was made with CardSuit limited to Clubs:
    enum CardSuit
    {
        Hearts,
        Spades,
        Diamonds,
        Clubs
    }

    // Define the possible ranks, in order
    enum CardRank
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    /// <summary>
    /// Cards are effectively nodes in a doubly linked list. They store
    /// the card's suit & rank, but also references to the previous and
    /// next card in the Deck.
    /// </summary>
    class Card
    {
        /// <summary>
        /// The Card's suit. It should never be able to be changed once set so
        /// the set is private so it can only be accessed from the constructor.
        /// </summary>
        public CardSuit Suit { get; private set; }

        /// <summary>
        /// The Card's rank. It should never be able to be changed once set so
        /// the set is private so it can only be accessed from the constructor.
        /// </summary>
        public CardRank Rank { get; private set; }

        /// <summary>
        /// A reference to the Card object currently BEFORE this one in the Deck. The
        /// Deck object is responsible to managing this reference. The may be null
        /// if this Card is the only/first one in the Deck.
        /// </summary>
        public Card? Previous { get; set; }

        /// <summary>
        /// A reference to the Card object currently AFTER this one in the Deck. The
        /// Deck object is responsible to managing this reference. This may be null
        /// if this Card if the only/last one in the Deck.
        /// </summary>
        public Card? Next { get; set; }

        /// <summary>
        /// Constructor to create a new Card
        /// </summary>
        /// <param name="suit">The suit for this Card object. After construction, this will never change.</param>
        /// <param name="rank">The rank for this Card object. After construction, this will never change.</param>
        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
            Previous = null;
            Next = null;
        }

        /// <summary>
        /// Return a string representation of the card
        /// </summary>
        /// <returns>A string representation of the Card that includes suit & rank</returns>
        public override string ToString()
        {
            // While the fact that enums auto convert to strings of the same name
            // is awesome, Five of Spades is a little harder to read than "5 of Spades"
            // so do a bit of manual conversion when printing.
            string rankStr = "?";

            switch(Rank)
            {
                case CardRank.Ace:
                case CardRank.Jack:
                case CardRank.Queen:
                case CardRank.King:
                    rankStr = Rank.ToString();
                    break;

                default:
                    rankStr = ((int)Rank + 1).ToString(); // leverage the enum value to find the card value
                    break;
            }

            return rankStr + " of " + Suit;
        }
    }
}
