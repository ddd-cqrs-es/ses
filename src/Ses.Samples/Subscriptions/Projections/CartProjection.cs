﻿using System.Diagnostics;
using System.Threading.Tasks;
using Ses.Abstracts.Subscriptions;
using Ses.Samples.Cart;

namespace Ses.Samples.Subscriptions.Projections
{
    public class CartProjection :
        IHandle<ShoppingCartCreated>,
        IHandle<ItemAddedToShoppingCart>,
        IHandle<ItemRemovedFromShoppingCart>
    {
        public Task Handle(ShoppingCartCreated e, EventEnvelope envelope)
        {
            Debug.WriteLine("Projected " + e.GetType().Name);
            return Task.FromResult(0);
        }

        public Task Handle(ItemAddedToShoppingCart e, EventEnvelope envelope)
        {
            Debug.WriteLine("Projected " + e.GetType().Name);
            return Task.FromResult(0);
        }

        public Task Handle(ItemRemovedFromShoppingCart e, EventEnvelope envelope)
        {
            Debug.WriteLine("Projected " + e.GetType().Name);
            return Task.FromResult(0);
        }
    }
}