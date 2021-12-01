﻿using System;

namespace BudgetCalculator
{
    /// <summary>
    /// A DTO of economic objects
    /// </summary>
    public abstract class EconomicObject : IEco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime CreationTime { get; set; }
        public int Interval { get; set; }

    }
}