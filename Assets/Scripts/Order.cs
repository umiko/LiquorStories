using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Pending = 0,
    Success = 1,
    Failure = 2
}

public class Order 
{
    public DrinkType DrinkTypeOrdered { get; set; }
    public Status Status { get; set; }

    public Order(DrinkType drinkType)
    {
        DrinkTypeOrdered = drinkType;
        Status = Status.Pending;
    }
}