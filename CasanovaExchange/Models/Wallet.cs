﻿namespace CasanovaExchange.Models
{
    public class Wallet
    {
        public Wallet()
        {
            Balance = 1000;
        }
        public int Id{ get; set; }
        public double Balance { get; set; }
    }
}
