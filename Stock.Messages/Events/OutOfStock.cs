﻿namespace Stock.Messages.Events
{
    public class OutOfStock
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
