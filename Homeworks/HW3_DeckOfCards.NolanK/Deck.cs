using System.Linq.Expressions;

namespace HW3_DeckOfCards
{
    // *************************************************************************
    // DO NOT MODIFY THIS FILE EXCEPT WHERE MARKED WITH "TODO"
    // There is no need for any new fields, properties, or methods!
    // *************************************************************************
    class Deck
    {
        // Each Deck is effectively a doubly linked list of Card objects that it manages
        // via references to the head and tail (top and bottom) cards + a field to track
        // the current # of cards

        /// <summary>
        /// A reference to the first Card in the Deck. This will be null when the Deck is empty.
        /// </summary>
        private Card? head = null;

        /// <summary>
        /// A reference to the last Card in the Deck. This will be null when the Deck is empty.
        /// </summary>
        private Card? tail = null;

        /// <summary>
        /// The current number of Cards. This is an auto-property that is publicly
        /// accessible, but only editable within the Deck class.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Implement an indexer property for the list that correctly gets 
        /// data in the node at a specific index. If the index is invalid, throw 
        /// an IndexOutOfRangeException exception. The get should never return null!
        /// 
        /// There is no need for a set!
        /// </summary>
        public Card this[int index]
        {
            get
            {
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // DONE - TODO: IMPLEMENT THIS (but you should really implement Add and test it using the debugger first!)
                //return null; // TMP so starter code compiles
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                if(index >= Count || index < 0)
                {
                    //throw new IndexOutOfRangeException();
                    return null;
                }
                
                Card myCard = head;

                while(index > 0 && myCard != null)
                {
                    myCard = myCard.Next;
                    index--;
                }

                return myCard;
                
            }

        }

        /// <summary>
        /// Add a new Card object (with the specified suit and rank) to the end of the list. 
        /// Increment the Count and update the head and/or tail when you add the card.
        /// </summary>
        public void Add(CardSuit suit, CardRank rank)
        {
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // DONE - TODO: IMPLEMENT THIS (and test it by checking the Deck contents using the debugger!)
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            Card myCard = new Card(suit, rank);

            if(head == null)
            {
                head = myCard;
                tail = myCard;
                Count++;
                return;
            }
            else
            {
                tail.Next = myCard;
                myCard.Previous = tail;

                tail = myCard;
                Count++;
            }


        }

        /// <summary>
        /// This method will remove a number of cards from the end of the list and 
        /// insert them back into the list before a certain index. This method only 
        /// has to worry about moving one group of cards in the deck. The main program 
        /// will repeatedly call this method to shuffle the entire deck.  The first 
        /// parameter is the number of cards to be removed from the end of the list. 
        /// The second parameter is the new index of the first card in the 
        /// group of cards that were moved in the list.
        /// 
        /// Notes:
        ///     - You can assume valid parameters will be give for the size of the deck. 
        ///       (i.e., this won't be called on a Deck with less than cardsToMove+1 cards in it.)
        ///     - If you leverage your this[] get indexer, there's no need for loops here.
        ///     - There's a PDF in this starter project to help you visualize this!
        /// </summary>
        public void Move(int cardsToMove, int targetIndex)
        {
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // DONE - TODO: IMPLEMENT THIS
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            if (Count - cardsToMove == targetIndex)
            {
                SmartConsole.PrintWarning("No Cards moved");
                return;
            }
            if(cardsToMove == 0)
            {
                SmartConsole.PrintWarning("No Cards moved");
                return;
            }
            if(Count <= 0)
            {
                SmartConsole.PrintWarning("No Cards in deck to move");
                return;
            }

            //define what the first of the new inserted cards will be
            Card tempHead = this[Count - cardsToMove];

            //define what the temp tail is (origional tail)
            Card tempTail = this[Count - 1];

            //define what new tail will be
            Card newTail = this[Count - cardsToMove - 1];

            //define card that comes before new card(s) insertion
            Card card_i = this[targetIndex];

            //this will be where to insert temphead
            Card card_i_minus_1 = card_i.Previous;

            ////////////////////////////////////////////////////////////////////

            tempTail.Next = card_i;
            card_i.Previous = tempTail;
            newTail.Next = null;
            tempHead.Previous = card_i_minus_1;
            if(card_i_minus_1 != null)
            {
                card_i_minus_1.Next = tempHead;
            }
            tail = newTail;
            if(targetIndex == 0)
            {
                head = tempHead;
            }

        }

        /// <summary>
        /// This method returns a deck for each player that represents the cards 
        /// in that player’s hand. All the cards in the deck need to be dealt out 
        /// to the players. All players do not need to have the same number of 
        /// cards.
        /// 
        /// This method should work even if the deck is empty!
        /// 
        /// Hint: Remember that while, after dealing cards in real life, the full deck
        ///         is split among the players into N smaller decks, but the total
        ///         number of cards remains the same and no cards are repeated. This
        ///         is NOT what will happen here. In order to preserve the links in the
        ///         main deck, your deal will COPY the cards as it adds them to each
        ///         player's deck!
        /// </summary>
        public List<Deck> DealPlayerHands(int playerCount)
        {
            List<Deck> result = new List<Deck>();

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // TODO: IMPLEMENT THIS
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            int cardsPerHand = Count / playerCount;
            if (cardsPerHand <= 0)
            {
                return result;
            }
            else
            {
                int currIndex = 0;

                //for each player, create a deck to add to resulting deck
                for (int i = 0; i < playerCount; i++)
                {
                    Deck tempDeck = new Deck();

                    if(cardsPerHand > 2)
                    {
                        Card newHead = new Card (this[currIndex].Suit, this[currIndex].Rank);
                        tempDeck.head = newHead;
                        tempDeck.tail = newHead;
                        currIndex++;

                        for (int j = 1; j < cardsPerHand; j++)
                        {
                            Card currCard = new Card(this[currIndex].Suit, this[currIndex].Rank);

                            tempDeck.tail.Next = currCard;
                            currCard.Previous = tempDeck.tail;
                            currIndex++;
                            tempDeck.tail = currCard;
                        }

                    }
                    else if (cardsPerHand == 1)
                    {
                        Card newHead = this[currIndex];
                        tempDeck.head = newHead;
                        tempDeck.tail = newHead;
                        currIndex++;
                    }
                    else if(cardsPerHand == 2)
                    {
                        Card newHead = this[currIndex];
                        tempDeck.head = newHead;
                        currIndex++;

                        Card newTail = this[currIndex];
                        tempDeck.tail = newTail;
                        currIndex++;
                    }

                    result.Add(tempDeck);
                }
                return result;
            }
        }

        /// <summary>
        /// This method should utilize the “next” field of each node to print 
        /// out all of the cards in order.  This will help to test if all of 
        /// your “arrows” point to the correct “boxes”.
        /// </summary>
        public void Print()
        {
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // DONE - TODO: IMPLEMENT THIS
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            Card tmp = head;
            if (tmp != null)
            {
                do
                {
                    Console.WriteLine(tmp.ToString());
                    tmp = tmp.Next;
                } while (tmp != tail.Next);

                SmartConsole.PrintSuccess("Sucessfully printed cards!");
            }
            else
            {
                SmartConsole.PrintWarning("No cards to print");
            }

        }

        /// <summary>
        /// This method should utilize the “previous” field of each node to 
        /// print out all of the data in reverse order.  This will help to 
        /// test if all of your “arrows” point to the correct “boxes”.
        /// </summary>
        public void PrintReversed()
        {
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // DONE - TODO: IMPLEMENT THIS
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            Card tmp = tail;
            do
            {
                Console.WriteLine(tmp.ToString());
                tmp = tmp.Previous;
            } while (tmp != tail.Next);

            SmartConsole.PrintSuccess("Sucessfully printed cards!");

        }

        /// <summary>
        /// This method will clear the list.  Update the Count attribute, 
        /// as well as the head and tail.
        /// </summary>
        public void Clear()
        {
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // DONE - TODO: IMPLEMENT THIS. There is no need for a loop!
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            head = null;
            tail = null;


        }
    }
}
