﻿using Prodavnica.Api.Models;

namespace Prodavnica.Api.Dto
{
    public class OrederDto
    {
        public Guid OrderId { get; set; }
        public Guid ByerId { get; set; }
        public Guid ShoppingItemId { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public List<ShoppingItemDto> Items { get; set; }
    }
}
