using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ====== ORDER CLASS ======
// PURPOSE : Organizing & interpreting user inputs in relation to the CharacterEntity's order
// AUTHOR : Nao (sacanthias)
// CREATED : 4/14/25
// LAST MODIFIED : 4/17/25

namespace IGME_106_Team_C_Project
{
    internal class Order
    {
        // Fields
        private List<FoodItems> orderList;

        // Properties
        internal int Count
        { 
            get { return orderList.Count; }
        }

        internal List<FoodItems> Plate
        {
            get { return orderList; }
        }

        // Parameterized Constructor
        // loads in a list of FoodItem enums
        public Order(List<FoodItems> inputList)
        {
            orderList = inputList;
        }

        // Methods
  
        /// <summary>
        /// Checks if the user-created userPlate (list of FoodItems) is equal to the orderList
        /// </summary>
        /// <param name="userPlate">The user-created plate that is checked</param>
        /// <returns>A bool based on if the userPlate matches the orderList</returns>
        internal bool Check(List<FoodItems> userPlate)
        {
            // temp local int to keep track of the total FoodItems that are equal at equivalent locations within the list
            int countCorrect = 0;

            // immediately returns false if the counts are unequal
            // (if the user submits an unfinished plate)
            if (userPlate.Count != orderList.Count)
            {
                return false;
            }

            // for loop checks based on the orderList
            for (int i = 0; i < userPlate.Count; i++)
            {
                if (userPlate[i].Equals(orderList[i]))
                {
                    countCorrect++;
                }
            }

            // if the countCorrect is equal to the orderList length, i.e., all values of userPlate are in the same location and equal the ones in orderList, returns true
            if (countCorrect == orderList.Count)
            {
                return true;
            }
            return false;
        }
    }
}
